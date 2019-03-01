package com.jurabek.restaurant.gateway.restaurantgateway;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.zuul.EnableZuulProxy;

@SpringBootApplication
@EnableZuulProxy
public class RestaurantGatewayApplication {

	public static void main(String[] args) {
		SpringApplication.run(RestaurantGatewayApplication.class, args);
	}

}
