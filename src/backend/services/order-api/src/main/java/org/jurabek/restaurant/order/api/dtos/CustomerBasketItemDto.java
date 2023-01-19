package org.jurabek.restaurant.order.api.dtos;

import java.util.UUID;

import lombok.Getter;
import lombok.Setter;

/**
 * CustomerBasketItemDto
 */
@Getter
@Setter
public class CustomerBasketItemDto {
    private UUID id;
    private UUID foodId;
    private float unitPrice;
    private float oldUnitPrice;
    private int quantity;
    private String picture;
    private String foodName;
}