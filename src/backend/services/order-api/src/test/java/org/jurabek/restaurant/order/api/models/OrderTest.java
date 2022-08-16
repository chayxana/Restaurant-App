package org.jurabek.restaurant.order.api.models;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class OrderTest {
    @Test
    public void TestGetAndSetOrderEntity() {
        Order order = new Order();

        UUID buyerId = UUID.randomUUID();
        UUID orderId = UUID.randomUUID();
        Date orderDate = new Date();

        OrderItems orderItem = new OrderItems();
        List<OrderItems> orderItems = new ArrayList<>();
        orderItems.add(orderItem);

        order.setBuyerId(buyerId);
        order.setId(orderId);
        order.setOrderedDate(orderDate);
        order.setOrderItems(orderItems);

        Assertions.assertEquals(buyerId, order.getBuyerId());
        Assertions.assertEquals(orderId, order.getId());
        Assertions.assertEquals(orderDate, order.getOrderedDate());
        Assertions.assertTrue(order.getOrderItems().contains(orderItem));
    }
}
