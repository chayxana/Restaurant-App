import { z } from 'zod';

export const CartItemScheme = z.object({
  food_id: z.number(),
  food_name: z.string(),
  old_unit_price: z.number(),
  picture: z.string(),
  quantity: z.number(),
  unit_price: z.number()
});
export type CartItem = z.infer<typeof CartItemScheme>;

export const CartScheme = z.object({
  customer_id: z.string(),
  items: z.array(CartItemScheme)
});
export type Cart = z.infer<typeof CartScheme>;
