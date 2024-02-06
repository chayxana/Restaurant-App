import { ShoppingCartIcon } from '@heroicons/react/24/solid';
import { cookies } from 'next/headers';
import { getCart } from './actions';

export default async function OpenCart() {
  const cartId = cookies().get('cartId')?.value;
  let cart;

  if (cartId) {
    cart = await getCart(cartId);
  }
  return (
    <div className="relative flex h-11 w-11 items-center justify-center rounded-md border border-neutral-200 text-black transition-colors">
      <ShoppingCartIcon className="h-6 w-6" />
      {cart?.items.length ? (
        <div className="absolute right-0 top-0 -mr-2 -mt-2 h-4 w-4 rounded bg-orange-500 text-center text-[11px] font-medium text-white">
          {cart?.items.length}
        </div>
      ) : null}
    </div>
  );
}
