import { getServerSession } from 'next-auth';
import { redirect } from 'next/navigation';
import { authOptions } from '@/lib/auth';
import { cookies, headers } from 'next/headers';
import { CheckoutForm } from '@/components/checkout/checkout-form';
import { createUrl } from '@/lib/utils';
import { getCart } from '@/components/cart/actions';
import { CartItemInfo } from '@/components/cart/cart-item';

export default async function Page() {
  const session = await getServerSession(authOptions);
  if (!session?.user) {
    const host = headers().get('x-forwarded-host');
    const protocol = headers().get('x-forwarded-proto') || 'http';
    const callbackUrl = `${protocol}://${host}/checkout`;
    return redirect(createUrl('api/auth/signin/web-app', new URLSearchParams({ callbackUrl })));
  }

  const cartId = cookies().get('cartId')?.value;
  if (!cartId) {
    return <div>Empty cart</div>;
  }

  const cart = await getCart(cartId);
  if (!cart) {
    return <div>Empty cart</div>;
  }

  return (
    <div className="container mx-auto p-6">
      <h1 className="mb-6 text-3xl font-bold">Checkout</h1>
      <div className="rounded-lg bg-white p-6 shadow-lg">
        <div className="flex">
          <div className="mx-auto w-3/5">
            <CheckoutForm />
          </div>
          <div className="mx-auto w-3/4 p-5">
            {cart.items.map((item) => (
              <CartItemInfo
                key={item.item_id}
                name={item.product_name}
                description={item.product_description}
                price={item.unit_price}
                image={item.img}
              />
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
