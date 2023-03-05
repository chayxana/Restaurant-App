package org.jurabek.restaurant.order.api.events;

import com.fasterxml.jackson.annotation.JsonProperty;

// Address.java
@lombok.Data
public class Address {
    @JsonProperty("street_address")
    private String streetAddress;

    @JsonProperty("city")
    private String city;

    @JsonProperty("state")
    private String state;

    @JsonProperty("country")
    private String country;

    @JsonProperty("zip_code")
    private long zipCode;
}