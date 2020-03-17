package com.jurabek.restaurant.order.api.models;

import static com.google.code.beanmatchers.BeanMatchers.*;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;
import static org.hamcrest.CoreMatchers.allOf;
import static org.hamcrest.MatcherAssert.assertThat;

@RunWith(JUnit4.class)
public class OrderItemsTest {
    @Test
    public void testGetterAndSetters() {
        assertThat(OrderItems.class, allOf(
                hasValidGettersAndSetters()
        ));
    }
}