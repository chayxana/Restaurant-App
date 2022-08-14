package org.jurabek.restaurant.order.api.configs;

import javax.enterprise.context.ApplicationScoped;
import javax.enterprise.inject.Produces;

import org.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import org.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import org.jurabek.restaurant.order.api.dtos.CustomerOrderItemsDto;
import org.jurabek.restaurant.order.api.mappers.CustomerBasketItemDtoToOrderItemsMap;
import org.jurabek.restaurant.order.api.mappers.CustomerBasketToOrderMap;
import org.jurabek.restaurant.order.api.mappers.OrderToCustomerOrderDtoMap;
import org.jurabek.restaurant.order.api.models.Order;
import org.jurabek.restaurant.order.api.models.OrderItems;
import org.modelmapper.ModelMapper;
import org.modelmapper.convention.MatchingStrategies;

/**
 * ModelMapperConfig
 */
@ApplicationScoped
public class ModelMapperConfig {

    @Produces
    public ModelMapper modelMapper() {
        var modelMapper = new ModelMapper();
        modelMapper.getConfiguration().setMatchingStrategy(MatchingStrategies.STRICT);
       
        var customerBasketItemDtoToOrderItemsMap = new CustomerBasketItemDtoToOrderItemsMap();
        var customerBasketToOrderMap = new CustomerBasketToOrderMap(modelMapper);
        var orderToCustomerOrderDto = new OrderToCustomerOrderDtoMap(modelMapper);
        
        modelMapper.addMappings(customerBasketItemDtoToOrderItemsMap);
        modelMapper.createTypeMap(CustomerBasketDto.class, Order.class).addMappings(customerBasketToOrderMap);

        modelMapper.createTypeMap(OrderItems.class, CustomerOrderItemsDto.class);
        modelMapper.createTypeMap(Order.class, CustomerOrderDto.class).addMappings(orderToCustomerOrderDto);

        return modelMapper;
    }
}