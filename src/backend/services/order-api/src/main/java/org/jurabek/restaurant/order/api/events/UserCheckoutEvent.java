package org.jurabek.restaurant.order.api.events;

import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonProperty;


@lombok.Data
public class UserCheckoutEvent {

    @JsonProperty("checkout_info")
    private CheckOutInfo checkOutInfo;
   
    @JsonProperty("customer_basket")
    private CustomerBasket customerBasket;

    @JsonProperty("transaction_id")
    private UUID transactionId;
}