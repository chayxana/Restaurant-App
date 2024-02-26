package org.jurabek.restaurant.order.api.controllers;

import java.util.List;

import jakarta.inject.Inject;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.DELETE;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.NotFoundException;

import org.jboss.resteasy.reactive.RestQuery;
import org.jurabek.restaurant.order.api.dtos.OrderDto;
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

	@GET()
	@Path("/find")
	public OrderDto find(@RestQuery String transactionId) {
		if (transactionId != null && !transactionId.isEmpty()) {
			OrderDto order = ordersService.getOrderByTransactionId(transactionId);
			if (order == null)
				throw new NotFoundException("Order not found");
			return order;
		}
		return null;
	}

	@GET
	public List<OrderDto> getData() {
		return this.ordersService.getAll();
	}

	@GET
	@Path("/{orderId}")
	public OrderDto get(String orderId) {
		return ordersService.getById(orderId);
	}


	@GET
	@Path("/customer/{customerId}")
	public List<OrderDto> getOrderByCustomerId(String customerId) {
		return ordersService.getOrderByCustomerId(customerId);
	}

	@DELETE
	public void delete(String orderId) {
		ordersService.Delete(orderId);
	}
}