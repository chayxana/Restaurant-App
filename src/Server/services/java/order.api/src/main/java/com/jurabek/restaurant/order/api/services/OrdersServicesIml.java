package com.jurabek.restaurant.order.api.services;

import java.util.Collection;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.models.OrderItems;
import com.jurabek.restaurant.order.api.repostitories.OrdersRepository;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

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
    public Collection<CustomerBasketDto> getAll() {
		return null;
	}
}