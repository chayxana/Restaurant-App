package com.jurabek.restaurant.order.api.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

/**
 * EndpointsConfiguration
 */
@Configuration
public class EndpointsConfiguration {
    @Value("${service-registry.basket-api.url")
    private String basketUrl;


    public String getBasketUrl() {
        return basketUrl;
    }
}