import { Categories, CategoriesScheme, FoodItems, FoodItemsScheme } from './types/food-item';
import { CustomerOrder, OrderSchema } from './types/order';

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

  const items: FoodItems = FoodItemsScheme.parse(await res.json());
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

export async function getUserInfo(userId: string) {
  const apiUrl = process.env.API_BASE_URL + `/users/${userId}`;
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error('Failed to fetch user info');
  }

  return await res.json();
}

export const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));
const retryCount = 5;

export async function getOrderByTransactionID(transactioId: string): Promise<CustomerOrder> {
  const apiUrl = process.env.API_BASE_URL + `/order/api/v1/orders/find?transactionId=${transactioId}`;
  const res = await fetch(apiUrl);
  if (res.ok) {
    return OrderSchema.parse(await res.json());
  }

  if (res.status == 500) {
    for (let i = 0; i < retryCount; i++) {
      await sleep(1000);
      const res = await fetch(apiUrl);
      if (res.ok) {
        return OrderSchema.parse(await res.json());
      }
    }
  }
  throw new Error('Failed to fetch user orders');
}
