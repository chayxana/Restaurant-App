package org.jurabek.restaurant.order.api.events;

import com.fasterxml.jackson.annotation.JsonProperty;

@lombok.Data
public class CreditCard {

    @JsonProperty("credit_card_number")
    private String creditCardNumber;

    @JsonProperty("credit_card_cvv")
    private long creditCardCvv;

    @JsonProperty("credit_card_expiration_year")
    private long creditCardExpirationYear;

    @JsonProperty("credit_card_expiration_month")
    private long creditCardExpirationMonth;
}