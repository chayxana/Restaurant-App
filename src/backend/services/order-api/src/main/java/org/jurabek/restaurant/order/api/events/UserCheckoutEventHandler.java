package org.jurabek.restaurant.order.api.events;

import java.util.UUID;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

import io.quarkus.grpc.GrpcClient;
import io.smallrye.common.annotation.Blocking;

import org.eclipse.microprofile.reactive.messaging.Incoming;
import org.jboss.logging.Logger;
import org.jurabek.payment.CreditCardInfo;
import org.jurabek.payment.PaymentRequest;
import org.jurabek.payment.PaymentServiceGrpc;
import org.jurabek.restaurant.order.api.services.CheckoutService;

@ApplicationScoped
public class UserCheckoutEventHandler {

    private static final Logger log = Logger.getLogger(UserCheckoutEventHandler.class);

    @GrpcClient(value = "paymentservice")
    PaymentServiceGrpc.PaymentServiceBlockingStub paymentService;

    private final CheckoutService checkout;

    @Inject
    public UserCheckoutEventHandler(CheckoutService checkout) {
        this.checkout = checkout;
    }

    @Incoming("checkout")
    @Blocking
    public void Handle(UserCheckoutEvent in) {
        log.info("received user checkout event: " + in);

        var orderId = UUID.randomUUID().toString();

        var card = in.getCheckOutInfo().getCreditCard();

        CreditCardInfo cardInfo = CreditCardInfo.newBuilder()
                .setCreditCardNumber(card.getCreditCardNumber())
                .setCreditCardCvv((int) card.getCreditCardCvv())
                .setCreditCardExpirationMonth((int) card.getCreditCardExpirationMonth())
                .setCreditCardExpirationYear((int) card.getCreditCardExpirationYear())
                .build();

        float total = 0;
        for (CustomerBasketItem item : in.getCustomerBasket().getItems()) {
            var multiplePrice = item.getUnitPrice() * item.getQuantity();
            total += multiplePrice;
        }

        var request = PaymentRequest.newBuilder()
                .setAmount(total)
                .setUserId(in.getCheckOutInfo().getCustomerId())
                .setOrderId(orderId)
                .setCreditCard(cardInfo)
                .build();

        log.info("payment req:" + request);

        var response = paymentService.payment(request);
        in.setTransactionId(UUID.fromString(response.getTransactionId()));
        checkout.Checkout(in);
    }
}
