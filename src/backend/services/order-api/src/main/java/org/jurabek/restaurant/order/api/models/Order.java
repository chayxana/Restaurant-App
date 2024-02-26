package org.jurabek.restaurant.order.api.models;

import java.util.Date;
import java.util.List;
import java.util.UUID;

import jakarta.persistence.CascadeType;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.Id;
import jakarta.persistence.NamedQueries;
import jakarta.persistence.NamedQuery;
import jakarta.persistence.OneToMany;
import jakarta.persistence.Table;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import lombok.Getter;
import lombok.Setter;

/**
 * Order
 */
@Entity
@Table(name = "orders")
@NamedQueries(value = {
        @NamedQuery(name = "Orders.fetchAll", query = "SELECT o FROM Order o LEFT JOIN FETCH o.orderItems")
})
@Getter
@Setter
public class Order extends PanacheEntityBase {
    @Column(nullable = false)
    @Id
    private UUID id;

    @Column(nullable = false)
    private UUID buyerId;

    @Column(nullable = false)
    private UUID cartId;

    @Column(nullable = false)
    private UUID transactionId;

    @Column(nullable = false)
    private UUID checkoutId;

    private Date orderedDate;

    @OneToMany(cascade = CascadeType.ALL, fetch = FetchType.EAGER, mappedBy = "order", orphanRemoval = true)
    private List<OrderItems> orderItems;
}