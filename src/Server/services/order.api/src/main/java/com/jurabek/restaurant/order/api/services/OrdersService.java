package com.jurabek.restaurant.order.api.services;

import java.util.List;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;

/**
 * OrdersService
 */
public interface OrdersService {
    List<CustomerOrderDto> getOrderByCustomerId(String customerId);
    List<CustomerOrderDto> getAll();
    void Create(CustomerBasketDto customerBasketDto);
    void Update(CustomerBasketDto customerBasketDto);
    void Delete(String orderId);
}