package com.jurabek.restaurant.order.api.services;

import java.util.ArrayList;
import java.util.List;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.models.OrderItems;
import com.jurabek.restaurant.order.api.repostitories.OrdersRepository;

import org.modelmapper.ModelMapper;
import org.modelmapper.TypeToken;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.lang.reflect.Type;
import java.util.UUID;

/**
 * OrdersServicesIml
 */
@Service
public class OrdersServicesIml implements OrdersService {

    private OrdersRepository ordersRepository;
    private ModelMapper modelMapper;

    @Autowired
    public OrdersServicesIml(OrdersRepository ordersRepository, ModelMapper modelMapper) {
        this.ordersRepository = ordersRepository;
        this.modelMapper = modelMapper;
    }

    @Override
    public void Create(CustomerBasketDto customerBasketDto) {
        Order order = modelMapper.map(customerBasketDto, Order.class);
        for (OrderItems orderItems : order.getOrderItems()) {
            orderItems.setOrder(order);
        }
        ordersRepository.save(order);
    }

    @Override
    public List<CustomerOrderDto> getAll() {
        List<Order> orders = this.ordersRepository.findAll();
        Type orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {}.getType();
        return modelMapper.map(orders, orderDtoType);
	}

    @Override
    public void Update(CustomerBasketDto customerBasketDto) {
    }

    @Override
    public CustomerOrderDto getOrderByCustomerId(String customerId) {
        Order order= ordersRepository.getByBuyerId(UUID.fromString(customerId));
        return modelMapper.map(order, CustomerOrderDto.class);
    }

	@Override
	public void Delete(String orderId) {
	}
}