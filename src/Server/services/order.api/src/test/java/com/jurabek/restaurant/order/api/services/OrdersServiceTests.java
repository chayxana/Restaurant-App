package com.jurabek.restaurant.order.api.services;

import static org.mockito.Mockito.*;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.models.OrderItems;
import com.jurabek.restaurant.order.api.repostitories.OrdersRepository;

import org.hamcrest.core.Is;
import org.hamcrest.core.IsSame;
import org.junit.Assert;
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
        CustomerBasketDto customerBasketDto = new CustomerBasketDto();
        Order order = new Order();
        List<OrderItems> orderItems = new ArrayList<OrderItems>();
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

        Type orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {}.getType();

        when(ordersRepository.findAll()).thenReturn(orders);
        when(modelMapper.map(orders, orderDtoType)).thenReturn(dtos);

        List<CustomerOrderDto> result = ordersService.getAll();

        Assert.assertThat(result.size(), Is.is(1)) ;
        Assert.assertThat(result.get(0), IsSame.sameInstance(dtos.get(0)));
    }

    @Test
    public void GivenCustomerIdReturnOrder() {
        Order order = new Order();
        String customerId = UUID.randomUUID().toString();
        CustomerOrderDto dto = new CustomerOrderDto();

        when(ordersRepository.getByBuyerId(UUID.fromString(customerId)))
            .thenReturn(order);
        
        when(modelMapper.map(order, CustomerOrderDto.class)).thenReturn(dto);

        CustomerOrderDto result = ordersService.getOrderByCustomerId(customerId);

        Assert.assertThat(dto, IsSame.sameInstance(result));
    }
}