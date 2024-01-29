package org.jurabek.restaurant.order.api.events;

import java.util.Date;
import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class OrderCompleted {
    private UUID orderId;
    private UUID cartId;
    private UUID userId;
    private UUID transactionId;
    private Date orderedDate;
}
