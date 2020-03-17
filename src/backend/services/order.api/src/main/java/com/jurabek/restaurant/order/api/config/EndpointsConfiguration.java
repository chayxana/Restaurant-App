package com.jurabek.restaurant.order.api.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

/**
 * EndpointsConfiguration
 */
@Configuration
public class EndpointsConfiguration {
    @Value("${microservices.basket-api.url:http://localhost:5200}")
    private String basketUrl;

    public String getBasketUrl() {
        return basketUrl;
    }
}