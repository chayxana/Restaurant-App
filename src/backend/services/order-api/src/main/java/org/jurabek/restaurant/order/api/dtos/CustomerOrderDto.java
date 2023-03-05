package org.jurabek.restaurant.order.api.dtos;

import java.util.Date;
import java.util.List;
import java.util.UUID;

import lombok.Data;

/**
 * OrderDto
 */
@Data
public class CustomerOrderDto {
    private UUID id;
    private Date orderedDate;
    private List<CustomerOrderItemsDto> orderItems;
}