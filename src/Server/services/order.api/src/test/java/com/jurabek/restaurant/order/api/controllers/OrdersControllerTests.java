package com.jurabek.restaurant.order.api.controllers;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import com.jurabek.restaurant.order.api.services.OrdersService;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;
import static org.junit.Assert.*;

import java.util.ArrayList;
import java.util.List;

import static org.mockito.Mockito.*;

/**
 * OrdersControllerTests
 */
@RunWith(MockitoJUnitRunner.class)
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
        CustomerBasketDto customerBasketDto = new CustomerBasketDto();
        doNothing().when(ordersService).Update(customerBasketDto);

        ordersController.update(customerBasketDto);
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
        assertEquals(mockResults, result);
        verify(ordersService, times(1)).getAll();
    }
}