'use server';

import { getCart } from '@/components/cart/actions';
import { Cart } from '@/components/cart/cart';
import { authOptions } from '@/lib/auth';
import { getServerSession } from 'next-auth';

export default async function Page() {
  const session = await getServerSession(authOptions);
  if (session?.user) {
    const cart = await getCart(session.user.user_id);
    return <Cart customerCart={cart} userId={session.user.user_id} />;
  }
  return <Cart />;
}
