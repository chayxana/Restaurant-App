package com.jurabek.restaurant.order.api.mappers;

import com.jurabek.restaurant.order.api.dtos.OrderDto;
import com.jurabek.restaurant.order.api.models.Order;

import org.modelmapper.PropertyMap;

/**
 * OrderToOrdetDtoMap
 */
public class OrderToOrdetDtoMap extends PropertyMap<Order, OrderDto> {

    @Override
    protected void configure() {
    }
}