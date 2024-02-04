package org.jurabek.restaurant.order.api.services;

import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.jurabek.restaurant.order.api.dtos.OrderDto;
import org.jurabek.restaurant.order.api.events.Cart;
import org.jurabek.restaurant.order.api.mappers.OrdersMapper;
import org.jurabek.restaurant.order.api.models.Order;
import org.jurabek.restaurant.order.api.models.OrderItems;
import org.jurabek.restaurant.order.api.repositories.OrdersRepository;


import io.quarkus.test.junit.QuarkusTest;
import io.quarkus.test.junit.mockito.InjectMock;
import jakarta.inject.Inject;

@QuarkusTest
public class OrdersServiceTests {

    @InjectMock
    private OrdersRepository ordersRepository;

    @InjectMock
    private OrdersMapper mapper;

    @Inject
    private OrdersServicesIml ordersService;

    @BeforeEach
    public void setup() {
        ordersService = new OrdersServicesIml(ordersRepository, mapper);
    }

    // @Test
    // public void CreateShouldCreateWhenCustomerBasketDto() {
    //     var customerBasketDto = new CustomerBasket();
    //     var order = new Order();
    //     var orderItems = new ArrayList<OrderItems>();
    //     orderItems.add(new OrderItems());
    //     order.setOrderItems(orderItems);

    //     when(mapper.mapDtoToOrder(customerBasketDto)).thenReturn(order);

    //     ordersService.Create(customerBasketDto);

    //     verify(ordersRepository, times(1)).persist(order);
    // }

    @Test
    public void GetAllShouldReturnAllOrderDtos() {
        var order = new Order();
        var orders = List.of(order);

        var dto = new OrderDto();

        when(ordersRepository.fetchAll()).thenReturn(orders);
        when(mapper.mapOrderToDto(order)).thenReturn(dto);

        List<OrderDto> result = ordersService.getAll();

        Assertions.assertEquals(1, result.size());
        Assertions.assertEquals(result.get(0), dto);
    }

    @Test
    public void GivenCustomerIdReturnOrder() {
        var order = new Order();
        var orders = List.of(order);
        String customerId = UUID.randomUUID().toString();
        var dto = new OrderDto();

        when(ordersRepository.getByBuyerId(UUID.fromString(customerId))).thenReturn(orders);

        when(mapper.mapOrderToDto(order)).thenReturn(dto);

        List<OrderDto> result = ordersService.getOrderByCustomerId(customerId);

        Assertions.assertEquals(result.size(), 1);
        Assertions.assertEquals(result.get(0), dto);
    }

    @Test
    public void delete_test() {
        // arrange
        String id = UUID.randomUUID().toString();
        UUID idUUID = UUID.fromString(id);
        when(ordersRepository.deleteById(idUUID)).thenReturn(true);

        // act
        ordersService.Delete(id);

        // assert
        verify(ordersRepository, times(1)).deleteById(idUUID);
    }

    @Test
    public void getById_test() {
        // arrange
        var orderId = UUID.randomUUID().toString();
        var order = new Order();
        var orderDto = new OrderDto();
        when(ordersRepository.findById(UUID.fromString(orderId))).thenReturn(order);
        when(mapper.mapOrderToDto(order)).thenReturn(orderDto);

        // act
        var result = ordersService.getById(orderId);

        // assert
        Assertions.assertEquals(result, orderDto);
    }
}