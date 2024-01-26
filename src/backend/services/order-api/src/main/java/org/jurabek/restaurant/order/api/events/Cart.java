package org.jurabek.restaurant.order.api.events;

import java.util.List;
import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

/**
 * CustomerBasketDto
 */
@Data
public class Cart {

    @JsonProperty("cart_id")
    private UUID cart_id;

    @JsonProperty("items")
    private List<CustomerBasketItem> items;
}