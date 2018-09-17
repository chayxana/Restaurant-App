package com.jurabek.restaurant.order.api.models;

import java.sql.Date;
import java.util.Collection;
import java.util.UUID;

import javax.persistence.*;

import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;

/**
 * Order
 */
@Entity
@Table(name = "Orders")
public class Order extends AuditModel{
    
    private static final long serialVersionUID = 8053559736205578247L;

    @Id
    private UUID id;
    
    private UUID buyerId;
    
    private Date orderDate;

    @OneToMany(fetch = FetchType.LAZY, mappedBy="orders")
    @OnDelete(action = OnDeleteAction.CASCADE)
    private Collection<OrderItem> orderItems;

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
    public Collection<OrderItem> getOrderItems() {
        return orderItems;
    }

    /**
     * @param orderItems the orderItems to set
     */
    public void setOrderItems(Collection<OrderItem> orderItems) {
        this.orderItems = orderItems;
    }

    /**
     * @param id the id to set
     */
    public void setId(UUID id) {
        this.id = id;
    }
}