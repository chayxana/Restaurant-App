package org.jurabek.restaurant.order.api.dtos;

import java.util.List;
import java.util.UUID;

import lombok.Getter;
import lombok.Setter;


/**
 * CustomerBasketDto
 */
@Getter
@Setter
public class CustomerBasketDto {
    private UUID customerId;
    private List<CustomerBasketItemDto> items;
}