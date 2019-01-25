package com.jurabek.restaurant.order.api.models;

import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import javax.persistence.*;

/**
 * Order
 */
@Entity
@Table(name="orders")
public class Order extends AuditModel {

    private static final long serialVersionUID = -601414249273316205L;

    @Id
    @Column(nullable = false)
    private UUID id;
    
    @Column(nullable = false)
    private UUID buyerId;
    
    private Date orderedDate;

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

    @OneToMany(fetch = FetchType.LAZY, cascade = CascadeType.ALL, mappedBy = "order")
    private List<OrderItems> orderItems;

    /**
     * @return the id
     */
    public UUID getId() {
        return id;
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
    public void setOrderItems(List<OrderItems> orderItems) {
        this.orderItems = orderItems;
    }

    /**
     * @param id the id to set
     */
    public void setId(UUID id) {
        this.id = id;
    }
}