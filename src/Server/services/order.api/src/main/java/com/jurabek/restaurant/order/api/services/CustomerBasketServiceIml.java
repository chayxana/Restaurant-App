package com.jurabek.restaurant.order.api.services;

import com.jurabek.restaurant.order.api.config.EndpointsConfiguration;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;

@Service
public class CustomerBasketServiceIml implements CustomerBasketService {

    private EndpointsConfiguration endpointsConfiguration;
    private RestTemplate restTemplate;

    @Autowired
    public CustomerBasketServiceIml(RestTemplate restTemplate, EndpointsConfiguration endpointsConfiguration) {
        this.restTemplate = restTemplate;
        this.endpointsConfiguration = endpointsConfiguration;
    }

    @Override
    public void clearCustomerBasket(String customerId) {
        var basketUrl = endpointsConfiguration.getBasketUrl();
        this.restTemplate
            .delete(basketUrl + "/api/v1/items/{customerId}", customerId);
    }
}
