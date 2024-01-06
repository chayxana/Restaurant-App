import { z } from 'zod';

export const FoodItems = z.array(
  z.object({
    id: z.number(),
    name: z.string(),
    description: z.string(),
    price: z.number(),
    image: z.string(),
    currency: z.string()
  })
);

export type FoodItems = z.infer<typeof FoodItems>;

export const Categories = z.array(
  z.object({
    id: z.number(),
    name: z.string()
  })
);

export type Categories = z.infer<typeof Categories>;
