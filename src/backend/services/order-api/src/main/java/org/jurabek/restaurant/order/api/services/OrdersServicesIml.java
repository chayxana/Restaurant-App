package org.jurabek.restaurant.order.api.services;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

import javax.enterprise.context.ApplicationScoped;
import javax.inject.Inject;

import org.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import org.jurabek.restaurant.order.api.mappers.OrdersMapper;
import org.jurabek.restaurant.order.api.repositories.OrdersRepository;

/**
 * OrdersServicesIml
 */
@ApplicationScoped
public class OrdersServicesIml implements OrdersService {

    private final OrdersRepository ordersRepository;
    private final OrdersMapper mapper;

    @Inject
    public OrdersServicesIml(OrdersRepository ordersRepository, OrdersMapper mapper) {
        this.ordersRepository = ordersRepository;
        this.mapper = mapper;
    }

    @Override
    public List<CustomerOrderDto> getAll() {
        return this.ordersRepository.fetchAll()
                .stream()
                .map(o -> mapper.mapOrderToDto(o))
                .collect(Collectors.toList());
    }

    @Override
    public List<CustomerOrderDto> getOrderByCustomerId(String customerId) {
        return ordersRepository.getByBuyerId(UUID.fromString(customerId))
                .stream()
                .map(o -> mapper.mapOrderToDto(o))
                .collect(Collectors.toList());
    }

    @Override
    public void Delete(String orderId) {
        ordersRepository.deleteById(UUID.fromString(orderId));
    }

    @Override
    public CustomerOrderDto getById(String orderId) {
        var order = ordersRepository.findById(UUID.fromString(orderId));
        return mapper.mapOrderToDto(order);
    }
}