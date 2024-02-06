'use client';

import { CartItem } from './cart-item';
import Link from 'next/link';
import { CustomerCart } from '@/lib/types/cart';
import { deleteCartItem, updateItem } from './actions';

export function CartDetail({ cart }: { cart?: CustomerCart }) {
  const items = cart?.items;

  if (!items) {
    return (
      <div className="container mx-auto mt-10 p-6">
        <h1 className="mb-4 text-center text-3xl font-bold">Your cart is empty</h1>
      </div>
    );
  }

  const totalPrice = cart.total;
  return (
    <div className="container mx-auto mt-10 p-6">
      <h1 className="mb-4 text-3xl font-bold">Your basket</h1>
      <div className="rounded-lg bg-white p-6 shadow-lg">
        <h2 className="mb-4 text-xl font-bold">Total Price: ${totalPrice.toFixed(2)}</h2>
        {items.map((item) => (
          <CartItem
            onQuantityChange={async (id, quantity) => {
              const item = cart.items.find((i) => i.item_id === id);
              if (!item) {
                return;
              }
              item.quantity = quantity;
              await updateItem(cart.id, item);
            }}
            onRemoveItem={async (id) => {
              await deleteCartItem(cart.id, id);
            }}
            key={item.item_id}
            id={item.item_id}
            name={item.product_name}
            description={item.product_description}
            price={item.unit_price}
            image={item.img}
            quantity={item.quantity}
          />
        ))}
        <Link href="/checkout">
          <div className="mt-6 flex justify-end">
            <button
              disabled={items.length == 0}
              className="w-1/3 rounded-full bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-600"
            >
              Checkout
            </button>
          </div>
        </Link>
      </div>
    </div>
  );
}
