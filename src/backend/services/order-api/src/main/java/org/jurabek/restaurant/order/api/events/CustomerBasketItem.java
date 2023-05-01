package org.jurabek.restaurant.order.api.events;

import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonProperty;

import lombok.Data;

/**
 * CustomerBasketItemDto
 */
@Data
public class CustomerBasketItem {
    private UUID id;

    @JsonProperty("item_id")
    private String itemId;

    @JsonProperty("price")
    private float price;

    @JsonProperty("quantity")
    private int quantity;
}