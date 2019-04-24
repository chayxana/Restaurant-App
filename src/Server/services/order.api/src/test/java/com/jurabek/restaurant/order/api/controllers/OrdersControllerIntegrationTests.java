package com.jurabek.restaurant.order.api.controllers;

import com.jurabek.restaurant.order.api.Application;

import org.junit.Ignore;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.context.SpringBootTest.WebEnvironment;
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
    // @Autowired
    // private TestRestTemplate restTemplate;
    
    @LocalServerPort
    private int port;

    @Test
    @Ignore
    public void greetingShouldReturnDefaultMessage() throws Exception {
        
    }
}