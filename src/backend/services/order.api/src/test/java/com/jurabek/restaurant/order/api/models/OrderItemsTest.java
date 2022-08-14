package com.jurabek.restaurant.order.api.models;

import static com.google.code.beanmatchers.BeanMatchers.*;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import static org.hamcrest.CoreMatchers.allOf;
import static org.hamcrest.MatcherAssert.assertThat;

@ExtendWith(SpringExtension.class)
public class OrderItemsTest {
    @Test
    public void testGetterAndSetters() {
        assertThat(OrderItems.class, allOf(
                hasValidGettersAndSetters()
        ));
    }
}