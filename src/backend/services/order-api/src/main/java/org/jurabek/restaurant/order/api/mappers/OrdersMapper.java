package org.jurabek.restaurant.order.api.mappers;

import java.math.BigDecimal;
import java.util.Date;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import jakarta.enterprise.context.ApplicationScoped;

import org.jurabek.restaurant.order.api.dtos.OrderDto;
import org.jurabek.restaurant.order.api.dtos.OrderItemDto;
import org.jurabek.restaurant.order.api.events.Cart;
import org.jurabek.restaurant.order.api.events.CustomerBasketItem;
import org.jurabek.restaurant.order.api.models.Order;
import org.jurabek.restaurant.order.api.models.OrderItems;

@ApplicationScoped
public class OrdersMapper {

    public Order mapDtoToOrder(Cart source) {
        var order = new Order();
        var items = source.getItems()
                .stream()
                .map(this::mapDtoToOrderItems)
                .collect(Collectors.toList());
        order.setOrderItems(items);
        order.setId(UUID.randomUUID());
        order.setOrderedDate(new Date());
        order.setCartId(source.getCart_id());
        return order;
    }

    public OrderItems mapDtoToOrderItems(CustomerBasketItem source) {
        var orderItems = new OrderItems();
        orderItems.setId(UUID.randomUUID());
        orderItems.setItemId(Integer.valueOf(source.getItemId()));
        orderItems.setUnitPrice(source.getPrice());
        orderItems.setUnits(source.getQuantity());
        return orderItems;
    }

    public OrderDto mapOrderToDto(Order order) {
        var dto = new OrderDto();
        dto.setId(order.getId());

        var orderItems = order.getOrderItems()
                .stream()
                .map(this::mapOrderItemsToDto)
                .collect(Collectors.toList());

        dto.setOrderItems(orderItems);
        dto.setOrderedDate(order.getOrderedDate());
        return dto;
    }

    public List<OrderDto> mapOrdersToDtos(List<Order> orders) {
        return orders.stream().map(this::mapOrderToDto).collect(Collectors.toList());
    }

    public OrderItemDto mapOrderItemsToDto(OrderItems source) {
        var dto = new OrderItemDto();
        dto.setProductId(source.getItemId());
        dto.setProductName(source.getItemName());
        dto.setId(source.getId());
        dto.setUnitPrice(new BigDecimal((source.getUnitPrice())));
        dto.setUnits(source.getUnits());
        return dto;
    }
}
