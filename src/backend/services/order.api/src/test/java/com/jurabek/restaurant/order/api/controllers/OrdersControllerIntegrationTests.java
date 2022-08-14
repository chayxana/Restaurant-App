package com.jurabek.restaurant.order.api.controllers;

import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.context.SpringBootTest.WebEnvironment;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import com.jurabek.restaurant.order.api.Application;

/**
 * OrdersControllerIntegrationTests
 */
@Disabled
@ExtendWith(SpringExtension.class)
@SpringBootTest(webEnvironment = WebEnvironment.RANDOM_PORT, classes = Application.class)
@TestPropertySource(locations = "classpath:application-test.properties")
public class OrdersControllerIntegrationTests {
    // @Autowired
    // private TestRestTemplate restTemplate;

    @Value("${local.server.port}")
    private int port;

    @Test
    @Disabled
    public void greetingShouldReturnDefaultMessage() throws Exception {

    }
}