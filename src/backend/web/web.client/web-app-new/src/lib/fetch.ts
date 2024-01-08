import { Categories, CategoriesScheme, FoodItems } from './types/food-item';

export async function fetchFoodItems(): Promise<FoodItems> {
  const apiUrl = process.env.API_BASE_URL + '/catalog/items/all';
  return await fetchItems(apiUrl);
}

export async function fetchFoodItemsByCategory(category: string): Promise<FoodItems> {
  const apiUrl = process.env.API_BASE_URL + `/catalog/items/all?category_name=${category}`;
  return await fetchItems(apiUrl);
}

async function fetchItems(apiUrl: string) {
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error('Failed to fetch catalog items data');
  }

  const items: FoodItems = FoodItems.parse(await res.json());
  const updatedItems = items.map((item) => {
    return {
      ...item,
      image: process.env.API_BASE_URL + item.image
    };
  });

  return updatedItems;
}

export async function fetchCategories(): Promise<Categories> {
  const apiUrl = process.env.API_BASE_URL + '/catalog/categories';
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error('Failed to fetch categories data');
  }

  const categories: Categories = CategoriesScheme.parse(await res.json());
  return categories;
}
