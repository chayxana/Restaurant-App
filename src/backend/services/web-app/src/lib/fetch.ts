import { Categories, CategoriesScheme, FoodItems, FoodItemsScheme } from './types/food-item';
import { CustomerOrder, OrderSchema } from './types/order';

export async function fetchFoodItems(): Promise<FoodItems> {
  const apiUrl = process.env.INTERNAL_API_BASE_URL + '/catalog/items/all';
  return await fetchItems(apiUrl);
}

export async function fetchFoodItemsByCategory(category: string): Promise<FoodItems> {
  const apiUrl = process.env.INTERNAL_API_BASE_URL + `/catalog/items/all?category_name=${category}`;
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
      image: process.env.INTERNAL_API_BASE_URL + item.image
    };
  });

  return updatedItems;
}

export async function fetchCategories(): Promise<Categories> {
  const apiUrl = process.env.INTERNAL_API_BASE_URL + '/catalog/categories';
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error('Failed to fetch categories data');
  }

  const categories: Categories = CategoriesScheme.parse(await res.json());
  return categories;
}

export async function getUserInfo(userId: string) {
  const apiUrl = process.env.INTERNAL_API_BASE_URL + `/users/${userId}`;
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error('Failed to fetch user info');
  }

  return await res.json();
}

export const sleep = (ms: number) => new Promise((resolve) => setTimeout(resolve, ms));

async function fetchWithRetry(url: string, retryCount = 5): Promise<any> {
  let lastError: Error | undefined;

  for (let attempt = 0; attempt < retryCount; attempt++) {
    try {
      const response = await fetch(url);
      if (response.ok) {
        return await response.json();
      } else if (response.status === 500) {
        lastError = new Error('Server Error (500)');
      } else {
        // Handle other HTTP errors differently if needed
        throw new Error(`HTTP Error: ${response.status}`);
      }
    } catch (error) {
      lastError = error as Error;
    }
    // Wait before retrying
    await sleep(1000);
  }
  throw lastError;
}

export async function getOrderByTransactionID(transactionId: string): Promise<CustomerOrder> {
  const apiUrl = `${process.env.INTERNAL_API_BASE_URL}/order/api/v1/orders/find?transactionId=${transactionId}`;
  try {
    const data = await fetchWithRetry(apiUrl);
    return OrderSchema.parse(data);
  } catch (error) {
    console.error('Failed to fetch order:', error);
    throw new Error('Failed to fetch user orders');
  }
}
