package org.jurabek.restaurant.order.api.repositories;


import java.util.UUID;

import jakarta.enterprise.context.ApplicationScoped;

import org.jurabek.restaurant.order.api.models.OrderItems;

import io.quarkus.hibernate.orm.panache.PanacheRepositoryBase;

/**
 * OrderItemsRepository
 */
@ApplicationScoped
public class OrderItemsRepository implements PanacheRepositoryBase<OrderItems, UUID>{   
}