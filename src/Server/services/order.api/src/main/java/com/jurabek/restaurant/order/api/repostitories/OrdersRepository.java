package com.jurabek.restaurant.order.api.repostitories;

import java.util.UUID;

import com.jurabek.restaurant.order.api.models.Order;

import org.springframework.data.jpa.repository.JpaRepository;
/**
 * OrdersRepository
 */
public interface OrdersRepository extends JpaRepository<Order, UUID> {
    Order getByBuyerId(UUID buyerId);
}