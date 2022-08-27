package org.jurabek.restaurant.order.api.models;

import java.util.UUID;

import javax.persistence.*;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name="OrderItems")
@Getter
@Setter
public class OrderItems  extends PanacheEntityBase { 
    @Column(nullable = false)
    @Id
    private UUID id;
    
    @Column(nullable = false)
    private UUID foodId;

    private float unitPrice;

    private float units;

    private String foodName;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "order_id", foreignKey = @ForeignKey(name = "order_id_fk"))
    private Order order;
}