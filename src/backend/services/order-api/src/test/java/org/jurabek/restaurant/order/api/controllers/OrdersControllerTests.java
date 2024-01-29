package org.jurabek.restaurant.order.api.controllers;

import static org.mockito.ArgumentMatchers.isA;
import static org.mockito.Mockito.doNothing;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.jurabek.restaurant.order.api.dtos.OrderDto;
import org.jurabek.restaurant.order.api.events.Cart;
import org.jurabek.restaurant.order.api.services.OrdersService;

import io.quarkus.test.junit.QuarkusTest;
import io.quarkus.test.junit.mockito.InjectMock;

/**
 * OrdersControllerTests
 */
@QuarkusTest
public class OrdersControllerTests {

    @InjectMock
    private OrdersService ordersService;

    private OrdersController ordersController;

    @BeforeEach
    public void setup() {
        ordersController = new OrdersController(ordersService);
    }

    // @Test
    // public void CreateShouldSave(){
    //     CustomerBasket customerBasketDto = new CustomerBasket();
    //     doNothing().when(ordersService).Create(isA(CustomerBasket.class));

    //     ordersController.create(customerBasketDto);
    //     verify(ordersService, times(1)).Create(customerBasketDto);
    // }

    // @Test
    // public void UpdateShouldUpdate() {
        
    //     // Arrange
    //     CustomerBasketDto customerBasketDto = new CustomerBasketDto();
    //     doNothing().when(ordersService).Update(customerBasketDto);

    //     // Act
    //     ordersController.update(customerBasketDto);

    //     // Assert
    //     verify(ordersService, times(1)).Update(customerBasketDto);
    // }

    @Test
    public void DeleteShouldRemove() {
        String orderId = "orderID";
        doNothing().when(ordersService).Delete(orderId);

        ordersController.delete(orderId);
        verify(ordersService, times(1)).Delete(orderId);
    }

    @Test
    public void getShouldReturnData(){
        List<OrderDto> mockResults = new ArrayList<>();
        mockResults.add(new OrderDto());
        mockResults.add(new OrderDto());
        mockResults.add(new OrderDto());

        when(ordersService.getAll()).thenReturn(mockResults);
        List<OrderDto> result = ordersController.getData();
        Assertions.assertEquals(mockResults, result);
        verify(ordersService, times(1)).getAll();
    }

    @Test
    public void getOrderByCustomerIdTest() {
        List<OrderDto> customerOrderDtos = Arrays.asList(new OrderDto());
        String customerId = "123";

        when(ordersService.getOrderByCustomerId(customerId))
                .thenReturn(customerOrderDtos);

        List<OrderDto> result = ordersController.getOrderByCustomerId(customerId);

        Assertions.assertEquals(customerOrderDtos.get(0), result.get(0));
    }
}