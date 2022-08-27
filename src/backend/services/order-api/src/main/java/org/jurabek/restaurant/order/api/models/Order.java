package org.jurabek.restaurant.order.api.models;

import java.util.Date;
import java.util.List;
import java.util.UUID;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;

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

    private Date orderedDate;

    @OneToMany(cascade = CascadeType.ALL, fetch = FetchType.EAGER, mappedBy = "order", orphanRemoval = true)
    private List<OrderItems> orderItems;
}