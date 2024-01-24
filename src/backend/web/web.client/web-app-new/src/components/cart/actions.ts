'use server';

import { SessionCartItem } from '@/context/cart-context';
import { authOptions } from '@/lib/auth';
import { CustomerCart, CustomerCartItem, CartScheme } from '@/lib/types/cart';
import { getServerSession } from 'next-auth';
import { revalidateTag } from 'next/cache';
import { cookies } from 'next/headers';

const cartUrl = `${process.env.API_BASE_URL}/shoppingcart/api/v1/cart`;

export async function getCart(cartID: string): Promise<CustomerCart | undefined> {
  const session = await getServerSession(authOptions);
  const requestOptions: RequestInit = {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${session?.user.token}`
    },
    cache: 'no-store',
    next: { tags: ['cart'] }
  };

  const getCartUrl = `${cartUrl}/${cartID}`;
  const res = await fetch(getCartUrl, requestOptions);
  if (res.status === 404) {
    return undefined
  }

  if (!res.ok) {
    throw new Error('Failed to fetch cart data');
  }

  const cart: CustomerCart = CartScheme.parse(await res.json());
  return cart;
}

export async function addCartItem(cartItem: SessionCartItem) {
  const session = await getServerSession(authOptions);
  let cartId = cookies().get('cartId')?.value;

  let cart: CustomerCart | undefined;
  if (cartId) {
    cart = await getCart(cartId);
  }

  if (!cartId || !cart) {
    cart = await createCart();
    cartId = cart.id;
    cookies().set('cartId', cartId);
  }
  await addToCart(cartId, mapSessionCartItemToCartItem(cartItem), session?.user.token);
}

export async function updateCartItemQuantity(
  customerId: string,
  itemId: number,
  newQuantity: number
) {
  const session = await getServerSession(authOptions);
  const existingCart = await getCart(customerId);

  // Check if the cart has the item
  const item = existingCart?.items.find((item) => item.item_id === itemId);
  if (!item) {
    throw new Error('Item not found in cart');
  }
  item.quantity = newQuantity;

  await addToCart(existingCart!.id, item, session?.user.token);
  revalidateTag('cart');
}

export async function deleteCartItem(cartId: string, itemId: number) {
  const session = await getServerSession(authOptions);
  const requestOptions: RequestInit = {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${session?.user.token}`
    },
    cache: 'no-store',
    next: { tags: ['cart'] }
  };

  const cartItemUrl = `${cartUrl}/${cartId}/item/${itemId}`
  try {
    const res = await fetch(cartItemUrl, requestOptions);
    if (!res.ok) {
      return 'failed to add items into cart';
    }
  } catch (error) {
    console.error(error);
    return 'failed to add items into cart';
  }
  revalidateTag('cart');
}

async function addToCart(cartId: string, lineItem: CustomerCartItem, token?: string) {
  const requestOptions: RequestInit = {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${token}`
    },
    body: JSON.stringify(lineItem),
    cache: 'no-store',
    next: { tags: ['cart'] }
  };

  const cartItemUrl = `${cartUrl}/${cartId}/item`
  try {
    const res = await fetch(cartItemUrl, requestOptions);
    if (!res.ok) {
      return 'failed to add items into cart';
    }
  } catch (error) {
    console.error(error);
    return 'failed to add items into cart';
  }
  revalidateTag('cart');
}

export async function deleteCart(cartID: string, token?: string) {
  const requestOptions: RequestInit = {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${token}`
    },
    cache: 'no-store',
    next: { tags: ['cart'] }
  };

  const deleteCartUrl = `${cartUrl}/${cartID}`;
  try {
    const res = await fetch(deleteCartUrl, requestOptions);
    if (!res.ok) {
      return 'failed to delete cart';
    }
  } catch (error) {
    console.error(error);
    return 'failed to delete cart';
  }
  revalidateTag('cart');
}

function mapSessionCartItemToCartItem(cartItem: SessionCartItem): CustomerCartItem {
  return {
    item_id: cartItem.id,
    product_name: cartItem.name,
    product_description: cartItem.description,
    img: cartItem.image,
    unit_price: cartItem.price,
    quantity: cartItem.quantity
  };
}

export async function createCart(token?: string): Promise<CustomerCart> {
  const requestOptions: RequestInit = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${token}`
    },
    body: JSON.stringify({
      "items": [],
      "user_id": ""
    }),
    cache: 'no-store',
    next: { tags: ['cart'] }
  };
  const res = await fetch(cartUrl, requestOptions);
  if (!res.ok) {
    throw new Error('Failed to fetch cart data');
  }
  const cart: CustomerCart = CartScheme.parse(await res.json());
  return cart;
}

