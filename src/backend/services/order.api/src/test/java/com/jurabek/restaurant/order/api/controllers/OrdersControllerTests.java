package com.jurabek.restaurant.order.api.controllers;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import com.jurabek.restaurant.order.api.services.OrdersService;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static org.mockito.Mockito.*;

/**
 * OrdersControllerTests
 */
@ExtendWith(SpringExtension.class)
public class OrdersControllerTests {

    @Mock
    private OrdersService ordersService;

    @InjectMocks
    private OrdersController ordersController;

    @Test
    public void CreateShouldSave(){
        CustomerBasketDto customerBasketDto = new CustomerBasketDto();
        doNothing().when(ordersService).Create(isA(CustomerBasketDto.class));

        ordersController.create(customerBasketDto);
        verify(ordersService, times(1)).Create(customerBasketDto);
    }

    @Test
    public void UpdateShouldUpdate() {
        
        // Arrange
        CustomerBasketDto customerBasketDto = new CustomerBasketDto();
        doNothing().when(ordersService).Update(customerBasketDto);

        // Act
        ordersController.update(customerBasketDto);

        // Assert
        verify(ordersService, times(1)).Update(customerBasketDto);
    }

    @Test
    public void DeleteShouldRemove() {
        String orderId = "orderID";
        doNothing().when(ordersService).Delete(orderId);

        ordersController.delete(orderId);
        verify(ordersService, times(1)).Delete(orderId);
    }

    @Test
    public void getShouldReturnData(){
        List<CustomerOrderDto> mockResults = new ArrayList<>();
        mockResults.add(new CustomerOrderDto());
        mockResults.add(new CustomerOrderDto());
        mockResults.add(new CustomerOrderDto());

        when(ordersService.getAll()).thenReturn(mockResults);
        List<CustomerOrderDto> result = ordersController.getData();
        Assertions.assertEquals(mockResults, result);
        verify(ordersService, times(1)).getAll();
    }

    @Test
    public void getOrderByCustomerIdTest() {
        List<CustomerOrderDto> customerOrderDtos = Arrays.asList(new CustomerOrderDto());
        String customerId = "123";

        when(ordersService.getOrderByCustomerId(customerId))
                .thenReturn(customerOrderDtos);

        List<CustomerOrderDto> result = ordersController.getOrderByCustomerId(customerId);

        Assertions.assertEquals(customerOrderDtos.get(0), result.get(0));
    }
}