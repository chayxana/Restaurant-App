package org.jurabek.restaurant.order.api.services;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

import org.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import org.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import org.jurabek.restaurant.order.api.models.Order;
import org.jurabek.restaurant.order.api.models.OrderItems;
import org.jurabek.restaurant.order.api.repositories.OrdersRepository;
import org.modelmapper.ModelMapper;
import org.modelmapper.TypeToken;

/**
 * OrdersServicesIml
 */
@ApplicationScoped
public class OrdersServicesIml implements OrdersService {

    private final OrdersRepository ordersRepository;
    private final ModelMapper modelMapper;

    @Inject
    public OrdersServicesIml(OrdersRepository ordersRepository, ModelMapper modelMapper) {
        this.ordersRepository = ordersRepository;
        this.modelMapper = modelMapper;
    }

    @Override
    public void Create(CustomerBasketDto customerBasketDto) {
        var order = modelMapper.map(customerBasketDto, Order.class);
        for (OrderItems orderItems : order.getOrderItems()) {
            orderItems.setOrder(order);
        }
        ordersRepository.persist(order);
    }

    @Override
    public List<CustomerOrderDto> getAll() {
        var orders = this.ordersRepository.fetchAll();
        var orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {}.getType();
        return modelMapper.map(orders, orderDtoType);
	}

    @Override
    public void Update(CustomerBasketDto customerBasketDto) {
    }

    @Override
    public List<CustomerOrderDto> getOrderByCustomerId(String customerId) {
        var order = ordersRepository.getByBuyerId(UUID.fromString(customerId));
        var orderDtoType = new TypeToken<ArrayList<CustomerOrderDto>>() {}.getType();
        return modelMapper.map(order, orderDtoType);
    }

	@Override
	public void Delete(String orderId) {
        ordersRepository.deleteById(UUID.fromString(orderId));
	}

    @Override
    public CustomerOrderDto getById(String orderId) {
        var order = ordersRepository.findById(UUID.fromString(orderId));
        return modelMapper.map(order, CustomerOrderDto.class);
    }
}