import { z } from 'zod';

export const OrderItemSchema = z.object({
  id: z.string().uuid(),
  productId: z.number(),
  unitPrice: z.number(),
  units: z.number()
});

export const OrderSchema = z.object({
  id: z.string().uuid(),
  orderedDate: z.string(),
  orderItems: z.array(OrderItemSchema)
});

export type CustomerOrder = z.infer<typeof OrderSchema>;
export type CustomerOrderItem = z.infer<typeof OrderItemSchema>;
