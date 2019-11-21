package com.jurabek.restaurant.order.api.dtos;

import java.util.List;
import java.util.UUID;


/**
 * CustomerBasketDto
 */

public class CustomerBasketDto {

    private UUID customerId;
    
    private List<CustomerBasketItemDto> items;

    /**
     * @return the items
     */
    public List<CustomerBasketItemDto> getItems() {
        return items;
    }

    /**
     * @return the customerId
     */
    public UUID getCustomerId() {
        return customerId;
    }

    /**
     * @param customerId the customerId to set
     */
    public void setCustomerId(UUID customerId) {
        this.customerId = customerId;
    }

    /**
     * @param items the items to set
     */
    public void setItems(List<CustomerBasketItemDto> items) {
        this.items = items;
    }
}