package com.jurabek.restaurant.order.api;

import com.jurabek.restaurant.order.api.controllers.OrdersControllerTests;

import org.junit.internal.TextListener;
import org.junit.runner.JUnitCore;
import org.junit.runner.Result;

/**
 * ApplicationTests
 */
public class ApplicationTests {

    public static void main(String[] args) {
        JUnitCore junit = new JUnitCore();
        junit.addListener(new TextListener(System.out));

        // Reflections reflections = new Reflections("my.project.prefix");
        // Set<Class<? extends Object>> allClasses = reflections.getSubTypesOf(Object.class);

        Result result = junit.run(OrdersControllerTests.class);
        printResult(result);
    }

    private static void printResult(Result result) {
        System.out.printf("Test ran: %s, Failed: %s%n",
                result.getRunCount(), result.getFailureCount());
    }
}