package org.jurabek.restaurant.order.api.dtos;


import static com.google.code.beanmatchers.BeanMatchers.hasValidGettersAndSetters;
import static org.hamcrest.CoreMatchers.allOf;
import static org.hamcrest.MatcherAssert.assertThat;

import org.junit.jupiter.api.Test;

public class CustomerBasketDtoTest {
    @Test
    public void testGetterAndSetters() {
        assertThat(CustomerBasketDto.class, allOf(
                hasValidGettersAndSetters()
        ));
    }
}
