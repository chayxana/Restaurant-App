package com.jurabek.restaurant.order.api.dtos;

import java.util.UUID;

/**
 * CustomerBasketItemDto
 */
public class CustomerBasketItemDto {
    private UUID id;
    private UUID foodId;
    private float unitPrice;
    private float oldUnitPrice;
    private int quantity;
    private String picture;
    private String foodName;

    /**
     * @return the id
     */
    public UUID getId() {
        return id;
    }

    /**
     * @return the foodName
     */
    public String getFoodName() {
        return foodName;
    }

    /**
     * @param foodName the foodName to set
     */
    public void setFoodName(String foodName) {
        this.foodName = foodName;
    }

    /**
     * @return the picture
     */
    public String getPicture() {
        return picture;
    }

    /**
     * @param picture the picture to set
     */
    public void setPicture(String picture) {
        this.picture = picture;
    }

    /**
     * @return the quantity
     */
    public int getQuantity() {
        return quantity;
    }

    /**
     * @param quantity the quantity to set
     */
    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

    /**
     * @return the oldUnitPrice
     */
    public float getOldUnitPrice() {
        return oldUnitPrice;
    }

    /**
     * @param oldUnitPrice the oldUnitPrice to set
     */
    public void setOldUnitPrice(float oldUnitPrice) {
        this.oldUnitPrice = oldUnitPrice;
    }

    /**
     * @return the unitPrice
     */
    public float getUnitPrice() {
        return unitPrice;
    }

    /**
     * @param unitPrice the unitPrice to set
     */
    public void setUnitPrice(float unitPrice) {
        this.unitPrice = unitPrice;
    }

    /**
     * @return the foodId
     */
    public UUID getFoodId() {
        return foodId;
    }

    /**
     * @param foodId the foodId to set
     */
    public void setFoodId(UUID foodId) {
        this.foodId = foodId;
    }

    /**
     * @param id the id to set
     */
    public void setId(UUID id) {
        this.id = id;
    }
}