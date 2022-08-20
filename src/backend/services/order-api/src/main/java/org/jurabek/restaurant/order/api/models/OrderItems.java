package org.jurabek.restaurant.order.api.models;

import java.util.UUID;

import javax.persistence.*;

import org.hibernate.annotations.GenericGenerator;

import io.quarkus.hibernate.orm.panache.PanacheEntityBase;

@Entity
@Table(name="OrderItems")
public class OrderItems  extends PanacheEntityBase { 
    @Column(nullable = false)
    @Id
//    @GeneratedValue( generator = "UUID" )
//    @GenericGenerator(
//        name = "UUID",
//        strategy = "org.hibernate.id.UUIDGenerator"
//    )
    private UUID id;
    
    @Column(nullable = false)
    private UUID foodId;

    private float unitPrice;

    private float units;

    private String foodName;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "order_id", foreignKey = @ForeignKey(name = "order_id_fk"))
    private Order order;

    /**
     * @return the foodId
     */
    public UUID getFoodId() {
        return foodId;
    }

    /**
     * @return the order
     */
    public Order getOrder() {
        return order;
    }

    /**
     * @param order the order to set
     */
    public void setOrder(Order order) {
        this.order = order;
    }

    /**
     * @return the id
     */
    public UUID getId() {
        return id;
    }

    /**
     * @param id the id to set
     */
    public void setId(UUID id) {
        this.id = id;
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
     * @return the units
     */
    public float getUnits() {
        return units;
    }

    /**
     * @param units the units to set
     */
    public void setUnits(float units) {
        this.units = units;
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
     * @param foodId the foodId to set
     */
    public void setFoodId(UUID foodId) {
        this.foodId = foodId;
    }
}