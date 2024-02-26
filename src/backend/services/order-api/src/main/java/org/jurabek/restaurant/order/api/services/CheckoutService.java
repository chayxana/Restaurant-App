package org.jurabek.restaurant.order.api.services;

import java.util.UUID;

import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.transaction.Transactional;

import org.eclipse.microprofile.reactive.messaging.Channel;
import org.eclipse.microprofile.reactive.messaging.Emitter;
import org.jurabek.restaurant.order.api.events.OrderCompleted;
import org.jurabek.restaurant.order.api.events.UserCheckoutEvent;
import org.jurabek.restaurant.order.api.mappers.OrdersMapper;
import org.jurabek.restaurant.order.api.repositories.OrdersRepository;


@ApplicationScoped
public class CheckoutService {
   
    private final OrdersRepository ordersRepository;
    private final OrdersMapper mapper;

    @Inject
    @Channel("order-completed")
    private Emitter<OrderCompleted> orderCompletedEventEmitter;

    @Inject
    public CheckoutService(OrdersRepository ordersRepository, OrdersMapper mapper) {
        this.ordersRepository = ordersRepository;
        this.mapper = mapper;
    }

    @Transactional
    public void Checkout(UserCheckoutEvent checkoutInfo) {
        var order = mapper.mapDtoToOrder(checkoutInfo.getCustomerBasket());
        order.setTransactionId(checkoutInfo.getTransactionId());
        order.setBuyerId(UUID.fromString(checkoutInfo.getCheckOutInfo().getUserId()));
        order.setCheckoutId(checkoutInfo.getCheckoutId());
        
        for (var orderItems : order.getOrderItems()) {
            orderItems.setOrder(order);
        }
        ordersRepository.persist(order);

        var event = new OrderCompleted(order.getId(), order.getCartId(), order.getBuyerId(), order.getTransactionId(),
                order.getOrderedDate());

        orderCompletedEventEmitter.send(event);
    }

}
