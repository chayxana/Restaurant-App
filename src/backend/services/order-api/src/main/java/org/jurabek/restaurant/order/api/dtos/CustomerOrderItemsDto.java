package org.jurabek.restaurant.order.api.dtos;

import java.util.UUID;

import lombok.Getter;
import lombok.Setter;

/**
 * OrderItemsDto
 */
@Getter
@Setter
public class CustomerOrderItemsDto {
    private UUID id;
    private UUID foodId;
    private float unitPrice;
    private float units;
    private String foodName;
}