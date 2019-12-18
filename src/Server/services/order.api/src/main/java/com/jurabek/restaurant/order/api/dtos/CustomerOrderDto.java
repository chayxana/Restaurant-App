package com.jurabek.restaurant.order.api.dtos;

import java.util.Date;
import java.util.List;
import java.util.UUID;

/**
 * OrderDto
 */
public class CustomerOrderDto {
    private UUID id;
    private Date orderedDate;
    private List<CustomerOrderItemsDto> orderItems;

    /**
     * @return the id
     */
    public UUID getId() {
        return id;
    }

    /**
	 * @return the orderItems
	 */
	public List<CustomerOrderItemsDto> getOrderItems() {
		return orderItems;
	}

	/**
	 * @param orderItems the orderItems to set
	 */
	public void setOrderItems(List<CustomerOrderItemsDto> orderItems) {
		this.orderItems = orderItems;
	}

	/**
     * @return the orderedDate
     */
    public Date getOrderedDate() {
        return orderedDate;
    }

    /**
     * @param orderedDate the orderedDate to set
     */
    public void setOrderedDate(Date orderedDate) {
        this.orderedDate = orderedDate;
    }

    /**
     * @param id the id to set
     */
    public void setId(UUID id) {
        this.id = id;
    }
}