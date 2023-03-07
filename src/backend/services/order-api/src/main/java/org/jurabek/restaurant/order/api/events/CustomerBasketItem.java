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

    @JsonProperty("food_id")
    private int foodId;
   
    @JsonProperty("unit_price")
    private float unitPrice;

    @JsonProperty("old_unit_price")
    private float oldUnitPrice;

    private int quantity;

    private String picture;
   
    @JsonProperty("food_name")
    private String foodName;
}