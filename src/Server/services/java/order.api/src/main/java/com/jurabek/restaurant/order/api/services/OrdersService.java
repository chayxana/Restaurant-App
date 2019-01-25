package com.jurabek.restaurant.order.api.services;

import java.util.List;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.models.Order;

/**
 * OrdersService
 */
public interface OrdersService {
    void Create(CustomerBasketDto customerBasketDto);
    List<Order> getAll();
}