package com.jurabek.restaurant.order.api.controllers;

import java.util.ArrayList;
import java.util.List;

import com.jurabek.restaurant.order.api.Application;
import com.jurabek.restaurant.order.api.dtos.CustomerOrderDto;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.context.SpringBootTest.WebEnvironment;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.boot.web.server.LocalServerPort;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.junit4.SpringRunner;

/**
 * OrdersControllerIntegrationTests
 */
@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = WebEnvironment.RANDOM_PORT, classes = Application.class)
@TestPropertySource(locations = "classpath:application-test.properties")
public class OrdersControllerIntegrationTests {
    @Autowired
    private TestRestTemplate restTemplate;
    
    @LocalServerPort
    private int port;

    @Test
    public void greetingShouldReturnDefaultMessage() throws Exception {

        List<CustomerOrderDto> result = this.restTemplate
            .getForObject("http://localhost:" + port + "/api/v1/orders", new ArrayList<CustomerOrderDto>()
            .getClass());
        
    }
}