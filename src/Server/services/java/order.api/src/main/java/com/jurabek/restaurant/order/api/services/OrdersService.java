package com.jurabek.restaurant.order.api.services;

import java.util.Collection;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;

/**
 * OrdersService
 */
public interface OrdersService {
    void Create(CustomerBasketDto customerBasketDto);
    Collection<CustomerBasketDto> getAll();
}