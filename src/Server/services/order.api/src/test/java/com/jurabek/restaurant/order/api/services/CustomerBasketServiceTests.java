package com.jurabek.restaurant.order.api.services;

import org.apache.http.client.HttpClient;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;

@RunWith(MockitoJUnitRunner.class)
public class CustomerBasketServiceTests {
    @Mock
    HttpClient httpClient;

    @InjectMocks
    CustomerBasketServiceIml customerBasketService;

    @Test
    public void clearCustomerBasket_Should_Send_Http_Request_to_Basket_API_and_clean_basket_by_customer_id() {
        customerBasketService.clearCustomerBasket("test");
    }
}
