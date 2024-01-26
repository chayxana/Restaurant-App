package org.jurabek.restaurant.order.api.events;

import com.fasterxml.jackson.annotation.JsonProperty;

// CheckOutInfo.java
@lombok.Data
public class CheckOutInfo {
    @JsonProperty("user_id")
    private String userId;

    @JsonProperty("cart_id")
    private String cartId;

    @JsonProperty("user_currency")
    private String userCurrency;

    @JsonProperty("address")
    private Address address;

    @JsonProperty("email")
    private String email;
}