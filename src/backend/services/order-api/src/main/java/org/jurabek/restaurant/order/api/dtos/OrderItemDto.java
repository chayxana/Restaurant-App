package org.jurabek.restaurant.order.api.dtos;

import java.math.BigDecimal;
import java.util.UUID;

import lombok.Data;

/**
 * OrderItemsDto
 */
@Data
public class OrderItemDto {
    private UUID id;
    private BigDecimal unitPrice;
    private float units;

    private int productId;
    private String productName;
}