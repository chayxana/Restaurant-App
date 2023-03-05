package org.jurabek.restaurant.order.api.dtos;


import static com.google.code.beanmatchers.BeanMatchers.hasValidGettersAndSetters;
import static org.hamcrest.CoreMatchers.allOf;
import static org.hamcrest.MatcherAssert.assertThat;

import org.junit.jupiter.api.Test;
import org.jurabek.restaurant.order.api.events.CustomerBasketItem;

public class CustomerBasketItemDtoTest {
    @Test
    public void testGetterAndSetters() {
        assertThat(CustomerBasketItem.class, allOf(
                hasValidGettersAndSetters()
        ));
    }
}
