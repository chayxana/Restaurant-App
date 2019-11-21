package com.jurabek.restaurant.order.api.services;

import org.apache.http.client.HttpClient;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

@Service
public class CustomerBasketServiceIml implements CustomerBasketService {
    private final HttpClient httpClient;

    @Value("${BASKET_URL:http://localhost:5200}")
    private String basketUrl;

    @Autowired
    public CustomerBasketServiceIml(HttpClient httpClient) {
        this.httpClient = httpClient;
    }

    @Override
    public void clearCustomerBasket(String customerId) {

    }
}
