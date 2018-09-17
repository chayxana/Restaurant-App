package com.jurabek.restaurant.order.api.controllers;

import java.util.ArrayList;
import java.util.List;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("api/v1/orders")
public class OrdersController {

  @GetMapping("orders")
  public List<String> getData() {
    return new ArrayList<String>();
  }
}