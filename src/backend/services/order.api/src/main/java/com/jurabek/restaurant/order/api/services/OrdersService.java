package com.jurabek.restaurant.order.api.services;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import java.util.List;

/** OrdersService */
public interface OrdersService {
  List<CustomerOrderDto> getOrderByCustomerId(String customerId);

  List<CustomerOrderDto> getAll();

  CustomerOrderDto getById(String orderId);

  void Create(CustomerBasketDto customerBasketDto);

  void Update(CustomerBasketDto customerBasketDto);

  void Delete(String orderId);
}
