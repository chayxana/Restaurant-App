package org.jurabek.restaurant.order.api.mappers;

import java.util.Date;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import javax.enterprise.context.ApplicationScoped;

import org.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import org.jurabek.restaurant.order.api.dtos.CustomerBasketItemDto;
import org.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import org.jurabek.restaurant.order.api.dtos.CustomerOrderItemsDto;
import org.jurabek.restaurant.order.api.models.Order;
import org.jurabek.restaurant.order.api.models.OrderItems;

@ApplicationScoped
public class OrdersMapper {

    public Order mapDtoToOrder(CustomerBasketDto source) {
        var order = new Order();
        var items = source.getItems()
                .stream()
                .map(i -> mapDtoToOrderItems(i))
                .collect(Collectors.toList());
        
                order.setOrderItems(items);
        order.setId(UUID.randomUUID());
        order.setOrderedDate(new Date());
        order.setBuyerId(source.getCustomerId());
        return order;
    }

    public OrderItems mapDtoToOrderItems(CustomerBasketItemDto source) {
        var orderItems = new OrderItems();
        orderItems.setId(source.getId());
        orderItems.setFoodId(source.getFoodId());
        orderItems.setUnitPrice(source.getUnitPrice());
        orderItems.setUnits(source.getQuantity());
        orderItems.setFoodName(source.getFoodName());
        return orderItems;
    }

    public CustomerOrderDto mapOrderToDto(Order order) {
        var dto = new CustomerOrderDto();
        dto.setId(order.getId());

        var orderItems = order.getOrderItems()
                .stream()
                .map(items -> mapOrderItemsToDto(items))
                .collect(Collectors.toList());

        dto.setOrderItems(orderItems);
        dto.setOrderedDate(order.getOrderedDate());
        return dto;
    }

    public List<CustomerOrderDto> mapOrdersToDtos(List<Order> orders) {
        return orders.stream().map(o -> mapOrderToDto(o)).collect(Collectors.toList());
    }

    public CustomerOrderItemsDto mapOrderItemsToDto(OrderItems source) {
        var dto = new CustomerOrderItemsDto();
        dto.setFoodId(source.getFoodId());
        dto.setFoodName(source.getFoodName());
        dto.setId(source.getId());
        dto.setUnitPrice(source.getUnitPrice());
        dto.setUnits(source.getUnits());
        return dto;
    }
}
