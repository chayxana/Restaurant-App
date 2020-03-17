package com.jurabek.restaurant.order.api.mappers;

import java.util.Date;
import java.util.UUID;
import java.util.stream.Collectors;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.models.OrderItems;

import org.modelmapper.ModelMapper;
import org.modelmapper.PropertyMap;

/**
 * CustomerBasketToOrderMap
 */
public class CustomerBasketToOrderMap extends PropertyMap<CustomerBasketDto, Order> {

    private final ModelMapper modelMapper;

    public CustomerBasketToOrderMap(ModelMapper modelMapper) {
        this.modelMapper = modelMapper;
    }

    @Override
    protected void configure() {

        using(ctx -> {
            var customerBasketDto = (CustomerBasketDto) ctx.getSource();

            return customerBasketDto.getItems().stream()
                    .map(basketItem -> modelMapper.map(basketItem, OrderItems.class)).collect(Collectors.toList());
        }).map(source, destination.getOrderItems());

        map().setId(UUID.randomUUID());
        map().setOrderedDate(new Date());
        map().setBuyerId(source.getCustomerId());
    }
}