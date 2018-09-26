package com.jurabek.restaurant.order.api.models;

import java.sql.Date;
import java.util.Collection;
import java.util.Set;
import java.util.UUID;

import javax.persistence.*;

/**
 * Order
 */
@Entity
@Table(name="orders")
public class Order extends AuditModel {
    @Id
    private UUID id;
    
    private UUID buyerId;
    
    private Date orderDate;

    @OneToMany(fetch = FetchType.EAGER, cascade = CascadeType.ALL, mappedBy = "order")
    private Set<OrderItems> orderItems;

    /**
     * @return the id
     */
    public UUID getId() {
        return id;
    }

    /**
     * @return the orderDate
     */
    public Date getOrderDate() {
        return orderDate;
    }

    /**
     * @param orderDate the orderDate to set
     */
    public void setOrderDate(Date orderDate) {
        this.orderDate = orderDate;
    }

    /**
     * @return the buyerId
     */
    public UUID getBuyerId() {
        return buyerId;
    }

    /**
     * @param buyerId the buyerId to set
     */
    public void setBuyerId(UUID buyerId) {
        this.buyerId = buyerId;
    }

    /**
     * @return the orderItems
     */
    public Collection<OrderItems> getOrderItems() {
        return orderItems;
    }

    /**
     * @param orderItems the orderItems to set
     */
    public void setOrderItems(Set<OrderItems> orderItems) {
        this.orderItems = orderItems;
    }

    /**
     * @param id the id to set
     */
    public void setId(UUID id) {
        this.id = id;
    }
}