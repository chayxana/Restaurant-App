package com.jurabek.restaurant.order.api.config;

import org.apache.http.client.HttpClient;
import org.apache.http.impl.client.HttpClients;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.client.RestTemplate;

@Configuration
public class DefaultConfig {
    @Bean
    HttpClient defaultHttpClient() {
        return HttpClients.createDefault();
    }

    @Bean
    RestTemplate restTemplate() {
        return new RestTemplate();
    }
}
