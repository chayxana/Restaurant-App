'use server';

import { SessionCartItem } from '@/context/cart-context';
import { authOptions } from '@/lib/auth';
import { CustomerCart, CustomerCartItem, CartScheme } from '@/lib/types/cart';
import { getServerSession } from 'next-auth';
import { revalidateTag } from 'next/cache';

const cartUrl = `${process.env.API_BASE_URL}/basket/api/v1/items`

export async function getCart(customerId: string): Promise<CustomerCart> {
  const session = await getServerSession(authOptions);
  console.log(session?.user.token);

  const requestOptions: RequestInit = {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${session?.user.token}`
    },
    cache: 'no-store',
    next: { tags: ['cart'] },
  };

  const getCartUrl = `${cartUrl}/${customerId}`;
  const res = await fetch(getCartUrl, requestOptions);
  if (res.status === 404) {
    return {
      customer_id: customerId,
      items: []
    }
  }
  if (!res.ok) {
    throw new Error('Failed to fetch cart data');
  }

  const cart: CustomerCart = CartScheme.parse(await res.json());
  return cart;
}

export async function addCartItem(customerId: string, cartItem: SessionCartItem) {
  const session = await getServerSession(authOptions);
  const existingCart = await getCart(customerId);

  const newCartItem = mapSessionCartItemToCartItem(cartItem);
  const newCart = {
    customer_id: customerId,
    items: mergeCartItems(existingCart.items, newCartItem)
  };

  await updateCart(newCart, session?.user.token)
  revalidateTag('cart');
}

export async function updateCartItemQuantity(customerId: string, itemId: number, newQuantity: number) {
  const session = await getServerSession(authOptions);
  const existingCart = await getCart(customerId);

  // Check if the cart has the item
  const itemIndex = existingCart.items.findIndex((item) => item.food_id === itemId);
  if (itemIndex === -1) {
    throw new Error('Item not found in cart');
  }
  existingCart.items[itemIndex].quantity = newQuantity;

  await updateCart(existingCart, session?.user.token)
  revalidateTag('cart');
}

export async function deleteCartItem(customerId: string, itemId: number) {
  const session = await getServerSession(authOptions);
  const existingCart = await getCart(customerId);

  // Filter out the item to be deleted
  const updatedCartItems = existingCart.items.filter(item => item.food_id !== itemId);
  if (updatedCartItems.length === 0) {
    await deleteCart(customerId, session?.user.token)
    revalidateTag('cart');
    return;
  }

  await updateCart({ ...existingCart, items: updatedCartItems }, session?.user.token)
  revalidateTag('cart');
}

async function updateCart(cart: CustomerCart, token?: string) {
  const requestOptions: RequestInit = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${token}`
    },
    body: JSON.stringify(cart),
    cache: 'no-store',
    next: { tags: ['cart'] },
  };

  try {
    const res = await fetch(cartUrl, requestOptions);
    if (!res.ok) {
      return 'failed to add items into cart';
    }
  } catch (error) {
    console.error(error);
    return 'failed to add items into cart';
  }
}

async function deleteCart(customerId: string, token?: string) {
  const requestOptions: RequestInit = {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${token}`
    },
    cache: 'no-store',
    next: { tags: ['cart'] },
  };

  const deleteCartUrl = `${cartUrl}/${customerId}`;
  try {
    const res = await fetch(deleteCartUrl, requestOptions);
    if (!res.ok) {
      return 'failed to delete cart';
    }
  } catch (error) {
    console.error(error);
    return 'failed to delete cart';
  }
}

export async function syncClientToBackend(
  customerCartItems: CustomerCartItem[],
  sessionCartItems: SessionCartItem[],
  customerId: string
) {
  // Update server with items only in client cart
  for (const clientItem of sessionCartItems) {
    const serverItem = customerCartItems.find((item) => item.food_id === clientItem.id);
    if (!serverItem) {
      // Add this item to the server
      await addCartItem(customerId, clientItem);
    } else if (clientItem.quantity !== serverItem.quantity) {
      // Update this item's quantity on the server
      await updateCartItemQuantity(customerId, clientItem.id, clientItem.quantity);
    }
  }
}

function mergeCartItems(exitingItems: CustomerCartItem[], newCartItem: CustomerCartItem): CustomerCartItem[] {
  const itemIndex = exitingItems.findIndex((item) => item.food_id === newCartItem.food_id);
  if (itemIndex > -1) {
    const newItems = [...exitingItems];
    newItems[itemIndex].quantity += newCartItem.quantity;
    return newItems;
  } else {
    return [...exitingItems, newCartItem];
  }
}

function mapSessionCartItemToCartItem(cartItem: SessionCartItem): CustomerCartItem {
  return {
    food_id: cartItem.id,
    food_name: cartItem.name,
    food_description: cartItem.description,
    picture: cartItem.image,
    old_unit_price: 0,
    unit_price: cartItem.price,
    quantity: cartItem.quantity
  };
}
