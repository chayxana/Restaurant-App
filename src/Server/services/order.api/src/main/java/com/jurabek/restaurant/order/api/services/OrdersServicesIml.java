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
    public boolean Create(CustomerBasketDto customerBasketDto) {
        Order order = modelMapper.map(customerBasketDto, Order.class);
        for (OrderItems orderItems : order.getOrderItems()) {
            orderItems.setOrder(order);
        }
        ordersRepository.save(order);
        return true;
    }

    @Override
    public List<CustomerOrderDto> getAll() {
        List<Order> orders = this.ordersRepository.findAll();
        Type orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {}.getType();
        List<CustomerOrderDto> result = modelMapper.map(orders, orderDtoType);
        return result;
	}
}