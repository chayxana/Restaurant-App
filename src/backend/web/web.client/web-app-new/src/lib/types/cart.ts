import { z } from 'zod';

export const CartItemScheme = z.object({
  food_id: z.number(),
  food_name: z.string(),
  food_description: z.string(),
  old_unit_price: z.number(),
  picture: z.string(),
  quantity: z.number(),
  unit_price: z.number()
});

export const CartScheme = z.object({
  customer_id: z.string(),
  items: z.array(CartItemScheme)
});

export type CustomerCartItem = z.infer<typeof CartItemScheme>;
export type CustomerCart = z.infer<typeof CartScheme>;
