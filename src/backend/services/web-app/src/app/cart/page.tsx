'use server';

import { getCart } from '@/components/cart/actions';
import { CartDetail } from '@/components/cart/cart-detail';
import { cookies } from 'next/headers';

export default async function Page() {
  const cartId = cookies().get('cartId')?.value;
  if (cartId) {
    const cart = await getCart(cartId);
    return <CartDetail cart={cart} />;
  }
  return <CartDetail />;
}
