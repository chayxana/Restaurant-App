package com.jurabek.restaurant.order.api.repostitories;

import com.jurabek.restaurant.order.api.models.OrderItems;
import java.util.UUID;
import org.springframework.data.jpa.repository.JpaRepository;

/** OrderItemsRepository */
public interface OrderItemsRepository extends JpaRepository<OrderItems, UUID> {}
