package org.jurabek.restaurant.order.api.events;

import java.util.List;
import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

/**
 * CustomerBasketDto
 */
@Data
public class CustomerBasket {

    @JsonProperty("customer_id")
    private UUID customerId;
    
    @JsonProperty("items")
    private List<CustomerBasketItem> items;
}