'use server';

import { getServerSession } from 'next-auth';
import { redirect } from 'next/navigation';
import { authOptions } from '@/lib/auth';
import { headers } from 'next/headers';
import { CheckoutForm } from '@/components/checkout/checkout-form';
import { createUrl, mapCustomerCartItemToSessionCartItem } from '@/lib/utils';
import { getCart } from '@/components/cart/actions';
import { CartItemInfo } from '@/components/cart/cart-item';

const Page = async () => {
  const session = await getServerSession(authOptions);
  if (!session?.user) {
    const host = headers().get('x-forwarded-host');
    const protocol = headers().get('x-forwarded-proto') || 'http';
    const callbackUrl = `${protocol}://${host}/checkout`;
    return redirect(createUrl('api/auth/signin/web-app', new URLSearchParams({ callbackUrl })));
  }

  const cart = await getCart(session.user.user_id);
  const items = cart.items.map(mapCustomerCartItemToSessionCartItem);

  return (
    <div className="container mx-auto p-6">
      <h1 className="mb-6 text-3xl font-bold">Checkout</h1>
      <div className="rounded-lg bg-white p-6 shadow-lg">
        <div className="flex">
          <div className="mx-auto w-3/5">
            <CheckoutForm />
          </div>
          <div className="mx-auto w-3/4 p-5">
            {items.map((item) => (
              <CartItemInfo key={item.id} {...item} />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Page;
