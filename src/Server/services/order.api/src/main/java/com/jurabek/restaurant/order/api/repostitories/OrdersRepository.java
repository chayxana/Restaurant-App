package com.jurabek.restaurant.order.api.repostitories;

import java.util.List;
import java.util.UUID;

import com.jurabek.restaurant.order.api.models.Order;

import org.springframework.data.jpa.repository.JpaRepository;
/**
 * OrdersRepository
 */
public interface OrdersRepository extends JpaRepository<Order, UUID> {
    List<Order> getByBuyerId(UUID buyerId);
}