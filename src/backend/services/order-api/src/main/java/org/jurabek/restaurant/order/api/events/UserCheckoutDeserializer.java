package org.jurabek.restaurant.order.api.events;

import io.quarkus.kafka.client.serialization.ObjectMapperDeserializer;

public class UserCheckoutDeserializer extends ObjectMapperDeserializer<UserCheckoutEvent> {
    public UserCheckoutDeserializer() {
        super(UserCheckoutEvent.class);
    }
}