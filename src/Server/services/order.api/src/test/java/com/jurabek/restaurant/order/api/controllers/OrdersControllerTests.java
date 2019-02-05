package com.jurabek.restaurant.order.api.controllers;

import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.services.OrdersService;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;

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
        when(ordersService.Create(customerBasketDto)).thenReturn(true);
        ordersController.create(customerBasketDto);
        verify(ordersService, times(1)).Create(customerBasketDto);
    }
}