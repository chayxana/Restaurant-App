package org.jurabek.restaurant.order.api.models;

import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import javax.persistence.*;

import org.hibernate.annotations.GenericGenerator;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;

/**
 * Order
 */
@Entity
@Table(name = "orders")
@NamedQueries(value = {
        @NamedQuery(name = "Orders.fetchAll", query = "SELECT o FROM Order o LEFT JOIN FETCH o.orderItems")
})
public class Order extends PanacheEntityBase {
    @Column(nullable = false)
    @Id
//    @GeneratedValue( generator = "UUID" )
//    @GenericGenerator(
//        name = "UUID",
//        strategy = "org.hibernate.id.UUIDGenerator"
//    )
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

    @OneToMany(cascade = CascadeType.ALL, fetch = FetchType.EAGER, mappedBy = "order", orphanRemoval = true)
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