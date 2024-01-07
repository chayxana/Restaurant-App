'use server';

import { SessionCartItem } from '@/context/cart-context';
import { authOptions } from '@/lib/auth';
import { Cart } from '@/lib/types/cart';
import { getServerSession } from 'next-auth';

export async function addItemServer(customerId: string, cartItem: SessionCartItem) {
  const session = await getServerSession(authOptions);

  const cartItemsUrl = process.env.API_BASE_URL + '/basket/api/v1/items';
  const cart: Cart = {
    customer_id: customerId,
    items: [{
      food_id: cartItem.id,
      food_name: cartItem.name,
      picture: cartItem.image,
      old_unit_price: 0,
      unit_price: cartItem.price,
      quantity: cartItem.quantity
    }]
  };

  const requestOptions = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json'
      , Authorization: `Bearer ${session?.user.token}`
    },
    body: JSON.stringify(cart)
  };

  try {
    const res = await fetch(cartItemsUrl, requestOptions);
    if (res.ok) {
      return "successfully added items into cart"
    } else {
      return "failed to add items into cart"
    }
  } catch (error) {
    console.error(error);
    return "failed to add items into cart"
  }
}
