package org.jurabek.restaurant.order.api.events;

import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonProperty;


@lombok.Data
public class UserCheckoutEvent {

    @JsonProperty("user_checkout")
    private CheckOutInfo checkOutInfo;
   
    @JsonProperty("customer_cart")
    private Cart customerBasket;

    @JsonProperty("transaction_id")
    private UUID transactionId;

    @JsonProperty("checkout_id")
    private UUID checkoutId;
}