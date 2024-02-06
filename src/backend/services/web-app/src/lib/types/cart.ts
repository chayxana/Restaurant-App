import { z } from 'zod';

export const CartItemScheme = z.object({
  item_id: z.number(),
  product_name: z.string(),
  product_description: z.string(),
  img: z.string(),
  quantity: z.number(),
  unit_price: z.number()
});

export const CartScheme = z.object({
  id: z.string().uuid(),
  user_id: z.string(),
  total: z.number(),
  items: z.array(CartItemScheme)
});

export type CustomerCartItem = z.infer<typeof CartItemScheme>;
export type CustomerCart = z.infer<typeof CartScheme>;
