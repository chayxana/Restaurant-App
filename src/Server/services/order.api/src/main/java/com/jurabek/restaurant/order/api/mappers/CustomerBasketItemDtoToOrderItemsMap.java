
package com.jurabek.restaurant.order.api.mappers;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketItemDto;
import com.jurabek.restaurant.order.api.models.OrderItems;

import org.modelmapper.PropertyMap;

/**
 * CustomerBasketItemDtoToOrderItemsMap
 */
public class CustomerBasketItemDtoToOrderItemsMap extends PropertyMap<CustomerBasketItemDto, OrderItems> {
    
    @Override
    protected void configure() {
        map().setId(source.getId());
        map().setFoodId(source.getFoodId());
        map().setUnitPrice(source.getUnitPrice());
        map().setUnits(source.getQuantity());
        map().setFoodName(source.getFoodName());
    }
}   