import { FoodItems } from "./types";

export async function fetchFoodItems(): Promise<FoodItems> {
  const apiUrl = process.env.API_BASE_URL + "/catalog/items/all";
  return await fetchItems(apiUrl);
}

export async function fetchFoodItemsByCategory(category: string): Promise<FoodItems> {
  const apiUrl = process.env.API_BASE_URL + `/catalog/items/all?category_name=${category}`;
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
      image: process.env.API_BASE_URL + item.image,
    };
  });

  return updatedItems;
}