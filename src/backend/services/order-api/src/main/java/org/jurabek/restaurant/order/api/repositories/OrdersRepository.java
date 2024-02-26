package org.jurabek.restaurant.order.api.repositories;

import java.util.List;
import java.util.UUID;

import jakarta.enterprise.context.ApplicationScoped;

import org.jurabek.restaurant.order.api.models.Order;

import io.quarkus.hibernate.orm.panache.PanacheRepositoryBase;

/**
 * OrdersRepository
 */
@ApplicationScoped
public class OrdersRepository implements PanacheRepositoryBase<Order, UUID> {
    public List<Order> getByBuyerId(UUID buyerId) {
      return find("buyerId", buyerId).firstResult();
    }

    public Order getByTransactionId(UUID transactionId) {
      return find("transactionId", transactionId).firstResult();
    }

    public List<Order> fetchAll() {
      return find("#Orders.fetchAll").list();
    }
}