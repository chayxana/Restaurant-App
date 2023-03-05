package org.jurabek.restaurant.order.api.dtos;

import java.util.UUID;

import lombok.Data;

/**
 * OrderItemsDto
 */
@Data
public class CustomerOrderItemsDto {
    private UUID id;
    private UUID foodId;
    private float unitPrice;
    private float units;
    private String foodName;
}