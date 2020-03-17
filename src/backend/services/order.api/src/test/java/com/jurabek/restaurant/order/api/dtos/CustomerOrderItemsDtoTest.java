package com.jurabek.restaurant.order.api.dtos;

import org.junit.Test;

import static com.google.code.beanmatchers.BeanMatchers.hasValidGettersAndSetters;
import static org.hamcrest.CoreMatchers.allOf;
import static org.hamcrest.MatcherAssert.assertThat;

public class CustomerOrderItemsDtoTest {
    @Test
    public void testGetterAndSetters() {
        assertThat(CustomerOrderItemsDto.class, allOf(
                hasValidGettersAndSetters()
        ));
    }
}
