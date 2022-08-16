package org.jurabek.restaurant.order.api.services;

import static org.mockito.Mockito.doNothing;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

// import java.lang.reflect.Type;
// import java.util.ArrayList;
// import java.util.List;
import java.util.UUID;

import javax.inject.Inject;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import org.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import org.jurabek.restaurant.order.api.models.Order;
import org.jurabek.restaurant.order.api.models.OrderItems;
import org.jurabek.restaurant.order.api.repositories.OrdersRepository;
import org.mockito.Mockito;
import org.modelmapper.ModelMapper;
// import org.modelmapper.TypeToken;

import io.quarkus.test.junit.QuarkusMock;
import io.quarkus.test.junit.QuarkusTest;
import io.quarkus.test.junit.mockito.InjectMock;

@QuarkusTest
public class OrdersServiceTests {

    @InjectMock
    private OrdersRepository ordersRepository;

    @InjectMock
    private ModelMapper modelMapper;

    private OrdersServicesIml ordersService;

    @BeforeEach
    public void setup() {
        ordersService = new OrdersServicesIml(ordersRepository, modelMapper);
    }

    // @Test
    // public void CreateShouldCreateWhenCustomerBasketDto() {
    //     var customerBasketDto = new CustomerBasketDto();
    //     var order = new Order();
    //     var orderItems = new ArrayList<OrderItems>();
    //     orderItems.add(new OrderItems());
    //     order.setOrderItems(orderItems);

    //     Mockito.when(modelMapper.map(customerBasketDto, Order.class)).thenReturn(order);

    //     ordersService.Create(customerBasketDto);

    //     Mockito.verify(ordersRepository, times(1)).persist(order);
    // }

    // @Test
    // public void GetAllShouldReturnAllOrderDtos() {
    //     List<Order> orders = new ArrayList<>();
    //     orders.add(new Order());

    //     List<CustomerOrderDto> dtos = new ArrayList<>();
    //     dtos.add(new CustomerOrderDto());

    //     Type orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {
    //     }.getType();

    //     when(ordersRepository.findAll().list()).thenReturn(orders);
    //     when(modelMapper.map(orders, orderDtoType)).thenReturn(dtos);

    //     List<CustomerOrderDto> result = ordersService.getAll();

    //     Assertions.assertEquals(1, result.size());
    //     Assertions.assertEquals(result.get(0), dtos.get(0));
    // }

    // @Test
    // public void GivenCustomerIdReturnOrder() {
    //     List<Order> orders = new ArrayList<>();
    //     orders.add(new Order());
    //     String customerId = UUID.randomUUID().toString();
    //     List<CustomerOrderDto> dtos = new ArrayList<>();
    //     dtos.add(new CustomerOrderDto());

    //     Type orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {
    //     }.getType();

    //     when(ordersRepository.getByBuyerId(UUID.fromString(customerId))).thenReturn(orders);

    //     when(modelMapper.map(orders, orderDtoType)).thenReturn(dtos);

    //     List<CustomerOrderDto> result = ordersService.getOrderByCustomerId(customerId);

    //     Assertions.assertEquals(result.size(), 1);
    //     Assertions.assertEquals(result.get(0), dtos.get(0));
    // }

    // @Test
    // public void delete_test() {
    //     // arrange
    //     String id = UUID.randomUUID().toString();
    //     UUID idUUID = UUID.fromString(id);
    //     doNothing().when(ordersRepository).deleteById(idUUID);

    //     // act
    //     ordersService.Delete(id);

    //     // assert
    //     verify(ordersRepository, times(1)).deleteById(idUUID);
    // }

    @Test
    public void getById_test() {
        // arrange
        var orderId = UUID.randomUUID().toString();
        var order = new Order();
        var orderDto = new CustomerOrderDto();
        when(ordersRepository.findById(UUID.fromString(orderId))).thenReturn(order);
        when(modelMapper.map(order, CustomerOrderDto.class)).thenReturn(orderDto);

        // act
        var result = ordersService.getById(orderId);

        // assert
        Assertions.assertEquals(result, orderDto);
    }
}