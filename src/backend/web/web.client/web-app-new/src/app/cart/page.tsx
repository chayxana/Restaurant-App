'use server';

import { getCart } from '@/components/cart/actions';
import { Cart } from '@/components/cart/cart';
import { cookies } from 'next/headers';

export default async function Page() {
  const cartId = cookies().get('cartId')?.value;
  if (cartId) {
    const cart = await getCart(cartId);
    return <Cart cart={cart} />;
  }
  return <Cart />;
}
