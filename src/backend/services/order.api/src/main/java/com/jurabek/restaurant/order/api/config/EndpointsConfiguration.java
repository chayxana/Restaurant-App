package com.jurabek.restaurant.order.api.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

/**
 * EndpointsConfiguration
 */
@Configuration
public class EndpointsConfiguration {
    @Value("${microservices.basket-api.url")
    private String basketUrl;

    @Value("${microservices.identity-api-public.url}")
    private String identityPublicUrl;

    public String getBasketUrl() {
        return basketUrl;
    }
    
    public String getIdentityPublicUrl() {
        return identityPublicUrl;
    }
}