package com.jurabek.restaurant.order.api.dtos;

import java.util.UUID;

/**
 * OrderItemsDto
 */
public class CustomerOrderItemsDto {
    private UUID id;
    private UUID foodId;
    private float unitPrice;
    private float units;
    private String foodName;

    public UUID getId() {
        return this.id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public UUID getFoodId() {
        return this.foodId;
    }

    public void setFoodId(UUID foodId) {
        this.foodId = foodId;
    }

    public float getUnitPrice() {
        return this.unitPrice;
    }

    public void setUnitPrice(float unitPrice) {
        this.unitPrice = unitPrice;
    }

    public float getUnits() {
        return this.units;
    }

    public void setUnits(float units) {
        this.units = units;
    }

    public String getFoodName() {
        return this.foodName;
    }

    public void setFoodName(String foodName) {
        this.foodName = foodName;
    }
}