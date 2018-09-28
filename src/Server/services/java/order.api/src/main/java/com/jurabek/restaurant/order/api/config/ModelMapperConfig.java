package com.jurabek.restaurant.order.api.config;

import java.util.Date;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerBasketItemDto;
import com.jurabek.restaurant.order.api.mappers.CustomerBasketItemDtoToOrderItemsMap;
import com.jurabek.restaurant.order.api.mappers.CustomerBasketToOrderMap;
import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.models.OrderItems;

import org.modelmapper.ModelMapper;
import org.modelmapper.PropertyMap;
import org.modelmapper.convention.MatchingStrategies;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

/**
 * ModelMapperConfig
 */
@Configuration
public class ModelMapperConfig {

    @Bean
    public ModelMapper modelMapper() {
        ModelMapper modelMapper = new ModelMapper();
        modelMapper.getConfiguration().setMatchingStrategy(MatchingStrategies.STRICT);
        PropertyMap<CustomerBasketItemDto, OrderItems> customerBasketItemDtoToOrderItemsMap = new CustomerBasketItemDtoToOrderItemsMap();
        PropertyMap<CustomerBasketDto, Order> customerBasketToOrderMap = new CustomerBasketToOrderMap(modelMapper);

        modelMapper.addMappings(customerBasketItemDtoToOrderItemsMap);
        modelMapper.createTypeMap(CustomerBasketDto.class, Order.class).addMappings(customerBasketToOrderMap);

        return modelMapper;
    }
}