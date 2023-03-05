package org.jurabek.restaurant.order.api.events;

import com.fasterxml.jackson.annotation.JsonProperty;

// CheckOutInfo.java
@lombok.Data
public class CheckOutInfo {
    @JsonProperty("customer_id")
    private String customerId;

    @JsonProperty("user_currency")
    private String userCurrency;

    @JsonProperty("address")
    private Address address;

    @JsonProperty("email")
    private String email;

    @JsonProperty("credit_card")
    private CreditCard creditCard;
}