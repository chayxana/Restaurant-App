package com.jurabek.restaurant.order.api.services;

import java.util.List;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;

/**
 * OrdersService
 */
public interface OrdersService {
    
    CustomerOrderDto getOrderByCustomerId(String customerId);
    List<CustomerOrderDto> getAll();

    boolean Create(CustomerBasketDto customerBasketDto);
    boolean Update(CustomerBasketDto customerBasketDto);
    boolean Delete(String orderId);
}