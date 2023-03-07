package org.jurabek.restaurant.order.api.services;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;
import javax.transaction.Transactional;

import org.jurabek.restaurant.order.api.events.UserCheckoutEvent;
import org.jurabek.restaurant.order.api.mappers.OrdersMapper;
import org.jurabek.restaurant.order.api.repositories.OrdersRepository;


@ApplicationScoped
public class CheckoutService {
   
    private final OrdersRepository ordersRepository;
    private final OrdersMapper mapper;
    
    @Inject
    public CheckoutService(OrdersRepository ordersRepository, OrdersMapper mapper) {
        this.ordersRepository = ordersRepository;
        this.mapper = mapper;
    }

    @Transactional
    public void Checkout(UserCheckoutEvent checkoutInfo) {
        var order = mapper.mapDtoToOrder(checkoutInfo.getCustomerBasket());
        order.setTransactionID(checkoutInfo.getTransactionId());
        
        for (var orderItems : order.getOrderItems()) {
            orderItems.setOrder(order);
        }
        ordersRepository.persist(order);
    }

}
