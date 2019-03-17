package com.jurabek.restaurant.service.discovery;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;

@SpringBootApplication
@EnableEurekaServer
public class RestaurantServiceDiscoveryApplication {

	public static void main(String[] args) {
		SpringApplication.run(RestaurantServiceDiscoveryApplication.class, args);
	}

}
