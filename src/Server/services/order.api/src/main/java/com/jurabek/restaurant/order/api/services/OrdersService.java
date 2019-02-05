package com.jurabek.restaurant.order.api.services;

import java.util.List;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;

/**
 * OrdersService
 */
public interface OrdersService {
    boolean Create(CustomerBasketDto customerBasketDto);
    List<CustomerOrderDto> getAll();
}