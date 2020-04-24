package com.jurabek.restaurant.order.api.services;

import static org.junit.Assert.assertThat;
import static org.mockito.Mockito.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.UUID;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.models.OrderItems;
import com.jurabek.restaurant.order.api.repostitories.OrdersRepository;

import org.hamcrest.core.Is;
import org.hamcrest.core.IsSame;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;
import org.modelmapper.ModelMapper;
import org.modelmapper.TypeToken;

import java.lang.reflect.Type;

@RunWith(MockitoJUnitRunner.class)
public class OrdersServiceTests {

    @Mock
    private OrdersRepository ordersRepository;

    @Mock
    private ModelMapper modelMapper;

    @InjectMocks
    private OrdersServicesIml ordersService;

    @Test
    public void CreateShouldCreateWhenCustomerBasketDto() {
        var customerBasketDto = new CustomerBasketDto();
        var order = new Order();
        var orderItems = new ArrayList<OrderItems>();
        orderItems.add(new OrderItems());
        order.setOrderItems(orderItems);

        when(modelMapper.map(customerBasketDto, Order.class)).thenReturn(order);

        ordersService.Create(customerBasketDto);

        verify(ordersRepository, times(1)).save(order);
    }

    @Test
    public void GetAllShouldReturnAllOrderDtos() {
        List<Order> orders = new ArrayList<>();
        orders.add(new Order());

        List<CustomerOrderDto> dtos = new ArrayList<>();
        dtos.add(new CustomerOrderDto());

        Type orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {
        }.getType();

        when(ordersRepository.findAll()).thenReturn(orders);
        when(modelMapper.map(orders, orderDtoType)).thenReturn(dtos);

        List<CustomerOrderDto> result = ordersService.getAll();

        assertThat(result.size(), Is.is(1));
        assertThat(result.get(0), IsSame.sameInstance(dtos.get(0)));
    }

    @Test
    public void GivenCustomerIdReturnOrder() {
        List<Order> orders = new ArrayList<>();
        orders.add(new Order());
        String customerId = UUID.randomUUID().toString();
        List<CustomerOrderDto> dtos = new ArrayList<>();
        dtos.add(new CustomerOrderDto());

        Type orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {
        }.getType();

        when(ordersRepository.getByBuyerId(UUID.fromString(customerId))).thenReturn(orders);

        when(modelMapper.map(orders, orderDtoType)).thenReturn(dtos);

        List<CustomerOrderDto> result = ordersService.getOrderByCustomerId(customerId);

        assertThat(result.size(), Is.is(1));
        assertThat(result.get(0), IsSame.sameInstance(dtos.get(0)));
    }

    @Test
    public void delete_test() {
        // arrange
        String id = UUID.randomUUID().toString();
        UUID idUUID = UUID.fromString(id);
        doNothing().when(ordersRepository).deleteById(idUUID);

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
        var orderDto = new CustomerOrderDto();
        when(ordersRepository.findById(UUID.fromString(orderId))).thenReturn(Optional.of(order));
        when(modelMapper.map(order, CustomerOrderDto.class)).thenReturn(orderDto);

        // act
        var result = ordersService.getById(orderId);

        // assert
        assertThat(result, IsSame.sameInstance(orderDto));
    }
}