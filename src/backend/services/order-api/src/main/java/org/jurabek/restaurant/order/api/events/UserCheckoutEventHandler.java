package org.jurabek.restaurant.order.api.events;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

import io.quarkus.grpc.GrpcClient;
import io.smallrye.common.annotation.Blocking;
import org.eclipse.microprofile.reactive.messaging.Incoming;
import org.jboss.logging.Logger;
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
        checkout.Checkout(in);
    }
}
