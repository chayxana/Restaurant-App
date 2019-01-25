package com.jurabek.restaurant.order.api.dtos;

import java.util.Date;
import java.util.UUID;

/**
 * OrderDto
 */
public class OrderDto {
    private UUID id;
    private Date orderedDate;
    private OrderItemsDto orderItems;
}