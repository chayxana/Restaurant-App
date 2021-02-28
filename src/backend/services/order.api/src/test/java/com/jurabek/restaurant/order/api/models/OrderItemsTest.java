package com.jurabek.restaurant.order.api.models;

import static com.google.code.beanmatchers.BeanMatchers.*;
import static org.hamcrest.CoreMatchers.allOf;
import static org.hamcrest.MatcherAssert.assertThat;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.JUnit4;

@RunWith(JUnit4.class)
public class OrderItemsTest {
  @Test
  public void testGetterAndSetters() {
    assertThat(OrderItems.class, allOf(hasValidGettersAndSetters()));
  }
}
