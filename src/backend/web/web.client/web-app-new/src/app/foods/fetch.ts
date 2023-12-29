import { z } from "zod";

const FoodItems = z.array(
  z.object({
    id: z.number(),
    name: z.string(),
    description: z.string(),
    price: z.number(),
    image: z.string(),
    currency: z.string(),
  })
);

export type FoodItems = z.infer<typeof FoodItems>;

export async function fetchFoodItems(): Promise<FoodItems> {
  const apiUrl = process.env.BASE_URL + "/catalog/items/all";
  return await fetchItems(apiUrl);
}


export async function fetchFoodItemsByCategory(category: string): Promise<FoodItems> {
  const apiUrl = process.env.BASE_URL + `/catalog/items/all?category_name=${category}`;
  return await fetchItems(apiUrl);
}


async function fetchItems(apiUrl: string) {
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error("Failed to fetch catalog items data");
  }

  const items: FoodItems = FoodItems.parse(await res.json());
  const updatedItems = items.map((item) => {
    return {
      ...item,
      image: process.env.BASE_URL + item.image,
    };
  });

  return updatedItems;
}