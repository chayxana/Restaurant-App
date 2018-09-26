package com.jurabek.restaurant.order.api.services;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.repostitories.OrdersRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

/**
 * OrdersServicesIml
 */
@Service
public class OrdersServicesIml implements OrdersService {

    private OrdersRepository ordersRepository;

    @Autowired
    public OrdersServicesIml(OrdersRepository ordersRepository) {
        this.ordersRepository = ordersRepository;
    }

    @Override
    public void Create(CustomerBasketDto customerBasketDto) {
        Order order = new Order();

        order.setBuyerId(customerBasketDto.getCustomerId());

        ordersRepository.save(order);
    }
}