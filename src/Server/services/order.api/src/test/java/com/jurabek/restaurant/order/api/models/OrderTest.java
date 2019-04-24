package com.jurabek.restaurant.order.api.models;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import org.hamcrest.core.IsEqual;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.junit.MockitoJUnitRunner;

@RunWith(MockitoJUnitRunner.class)
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
        order.setCreatedAt(orderDate);
        order.setUpdatedAt(orderDate);

        Assert.assertThat(buyerId, IsEqual.equalTo((order.getBuyerId())));
        Assert.assertThat(orderId, IsEqual.equalTo((order.getId())));
        Assert.assertThat(orderDate, IsEqual.equalTo((order.getOrderedDate())));
        Assert.assertThat(orderDate, IsEqual.equalTo((order.getCreatedAt())));
        Assert.assertThat(orderDate, IsEqual.equalTo((order.getUpdatedAt())));
        Assert.assertThat(orderDate, IsEqual.equalTo((order.getUpdatedAt())));
        Assert.assertTrue(order.getOrderItems().contains(orderItem));
    }
}
