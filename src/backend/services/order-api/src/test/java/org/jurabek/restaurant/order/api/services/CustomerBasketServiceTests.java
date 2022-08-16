// package com.jurabek.restaurant.order.api.services;

// import static org.mockito.Mockito.doNothing;
// import static org.mockito.Mockito.times;
// import static org.mockito.Mockito.verify;
// import static org.mockito.Mockito.when;

// import com.jurabek.restaurant.order.api.config.EndpointsConfiguration;

// import org.junit.jupiter.api.Disabled;
// import org.junit.jupiter.api.Test;
// import org.junit.jupiter.api.extension.ExtendWith;
// import org.mockito.InjectMocks;
// import org.mockito.Mock;
// import org.springframework.test.context.junit.jupiter.SpringExtension;
// import org.springframework.web.client.RestTemplate;


// public class CustomerBasketServiceTests {
//     @Mock
//     EndpointsConfiguration configuration;

//     @Mock
//     RestTemplate restTemplate;

//     @InjectMocks
//     CustomerBasketServiceIml customerBasketService;

//     @Test
//     @Disabled
//     public void clearCustomerBasket_Should_Send_Http_Request_to_Basket_API_and_clean_basket_by_customer_id() {
//         // Arrange
//         when(configuration.getBasketUrl()).thenReturn("http://basket_url");
//         doNothing().when(restTemplate).delete("http://basket_url//api/v1/items/{customerId}", "test");

//         // Act
//         customerBasketService.clearCustomerBasket("test");

//         // Assert
//         verify(restTemplate, times(1)).delete("http://basket_url//api/v1/items/{customerId}", "test");
//     }
// }
