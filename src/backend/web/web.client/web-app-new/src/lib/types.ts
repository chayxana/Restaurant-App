import { z } from 'zod';


export const FoodItem = z.object({
  id: z.number(),
  name: z.string(),
  description: z.string(),
  price: z.number(),
  image: z.string(),
  currency: z.string()
})

export type FoodItem = z.infer<typeof FoodItem>;

export const FoodItems = z.array(FoodItem);

export type FoodItems = z.infer<typeof FoodItems>;

export const Categories = z.array(
  z.object({
    id: z.number(),
    name: z.string()
  })
);

export type Categories = z.infer<typeof Categories>;

export const CartItem = z.object({
  food_id: z.number(),
  food_name: z.string(),
  old_unit_price: z.number(),
  picture: z.string(),
  quantity: z.number(),
  unit_price: z.number()
});
export type CartItem = z.infer<typeof CartItem>;

export const Cart = z.object({
  customer_id: z.string(),
  items: z.array(CartItem)
});
export type Cart = z.infer<typeof Cart>;
