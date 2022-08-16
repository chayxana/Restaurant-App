package org.jurabek.restaurant.order.api.controllers;

import java.util.List;

import javax.inject.Inject;
import javax.transaction.Transactional;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

import org.jurabek.restaurant.order.api.dtos.CustomerBasketDto;
import org.jurabek.restaurant.order.api.dtos.CustomerOrderDto;
import org.jurabek.restaurant.order.api.services.OrdersService;

@Path("api/v1/orders")
@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
public class OrdersController {

	private final OrdersService ordersService;

	@Inject
	public OrdersController(OrdersService ordersService) {
		this.ordersService = ordersService;
	}

	@GET
	public List<CustomerOrderDto> getData() {
		return this.ordersService.getAll();
	}

	@GET
	@Path("/{orderId}")
	public CustomerOrderDto get(String orderId) {
		return ordersService.getById(orderId);
	}

	@POST
	@Transactional
	public void create(CustomerBasketDto customerBasketDto) {
		ordersService.Create(customerBasketDto);
	}

	@PUT
	public void update(CustomerBasketDto customerBasketDto) {
		ordersService.Update(customerBasketDto);
	}

	@GET
	@Path("/customer/{customerId}")
	public List<CustomerOrderDto> getOrderByCustomerId(String customerId) {
		return ordersService.getOrderByCustomerId(customerId);
	}

	@DELETE
	public void delete(String orderId){
	    ordersService.Delete(orderId);
    }
}