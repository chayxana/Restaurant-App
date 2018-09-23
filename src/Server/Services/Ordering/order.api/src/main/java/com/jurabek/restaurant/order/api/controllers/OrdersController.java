package com.jurabek.restaurant.order.api.controllers;

import java.util.List;

import com.jurabek.restaurant.order.api.models.Order;
import com.jurabek.restaurant.order.api.repostitories.OrdersRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/v1/orders")
public class OrdersController {

  private OrdersRepository ordersRepository;

  @Autowired
  public OrdersController(OrdersRepository ordersRepository) {
    this.ordersRepository = ordersRepository;
  }

  @GetMapping()
  public List<Order> getData() {
    return ordersRepository.findAll();
  }

  @PostMapping()
  public void create() {
    
  }
}