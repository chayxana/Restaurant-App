package com.jurabek.restaurant.order.api.config;

import org.apache.http.client.HttpClient;
import org.apache.http.impl.client.HttpClients;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class DefaultConfig {
    @Bean
    HttpClient defaultHttpClient() {
        return HttpClients.createDefault();
    }
}
