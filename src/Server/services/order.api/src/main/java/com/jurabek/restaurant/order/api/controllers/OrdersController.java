package com.jurabek.restaurant.order.api.controllers;

import java.util.List;

import com.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import com.jurabek.restaurant.order.api.services.OrdersService;

import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.Authorization;
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

	@GetMapping("getAllOrders")
	@ApiOperation(value = "Returns all orders", authorizations= { @Authorization(value="oauth2") })
	public List<CustomerOrderDto> getData() {
		return this.ordersService.getAll();
	}

	@PostMapping()
	@ApiOperation(value = "Creates new order", authorizations= { @Authorization(value="oauth2") })
	public void create(@RequestBody CustomerBasketDto customerBasketDto) {
		ordersService.Create(customerBasketDto);
	}

	@PutMapping()
	@ApiOperation(value = "Updates order", authorizations= { @Authorization(value="oauth2") })
	public void update(@RequestBody CustomerBasketDto customerBasketDto) {
		ordersService.Update(customerBasketDto);
	}

	@GetMapping("getOrdersByCustomerId")
	@ApiOperation(value = "Returns all orders for specific customer", authorizations= { @Authorization(value="oauth2") })
	public List<CustomerOrderDto> getOrderByCustomerId(String customerId) {
		return ordersService.getOrderByCustomerId(customerId);
	}

	@DeleteMapping()
	@ApiOperation(value = "Deletes order", authorizations= { @Authorization(value="oauth2") })
	public void delete(String orderId){
	    ordersService.Delete(orderId);
    }
}