package com.jurabek.restaurant.order.api.controllers;

import java.util.List;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import com.jurabek.restaurant.order.api.services.OrdersService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("api/v1/orders")
public class OrdersController {

	private OrdersService ordersService;

	@Autowired
	public OrdersController(OrdersService ordersService) {
		this.ordersService = ordersService;
	}

	@GetMapping()
	public List<CustomerOrderDto> getData() {
		return this.ordersService.getAll();
	}

	@PostMapping()
	public void create(@RequestBody CustomerBasketDto customerBasketDto) {
		ordersService.Create(customerBasketDto);
	}

	@PutMapping()
	public void update(@RequestBody CustomerBasketDto customerBasketDto) {
		ordersService.Update(customerBasketDto);
	}

	@GetMapping("getOrderByCustomerId")
	public CustomerOrderDto getOrderByCustomerId(String customerId) {
		return ordersService.getOrderByCustomerId(customerId);
	}

	@DeleteMapping()
	public void delete(String orderId){
	    ordersService.Delete(orderId);
    }
}