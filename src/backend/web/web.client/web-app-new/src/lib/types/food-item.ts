import { z } from 'zod';

export const FoodItem = z.object({
  id: z.number(),
  name: z.string(),
  description: z.string(),
  price: z.number(),
  image: z.string(),
  currency: z.string()
});

export const FoodItems = z.array(FoodItem);

export const CategoriesScheme = z.array(
  z.object({
    id: z.number(),
    name: z.string()
  })
);

export type FoodItem = z.infer<typeof FoodItem>;
export type FoodItems = z.infer<typeof FoodItems>;
export type Categories = z.infer<typeof CategoriesScheme>;
