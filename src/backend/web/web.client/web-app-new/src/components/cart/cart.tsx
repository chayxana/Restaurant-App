'use client';

import { CartItem } from './cart-item';
import Link from 'next/link';
import { CustomerCart } from '@/lib/types/cart';
import { deleteCartItem, updateCartItemQuantity } from './actions';
import { useCart } from '@/context/cart-context';

export function Cart({ customerCart, userId }: { customerCart?: CustomerCart; userId?: string }) {
  const { removeItem, items, setQuantity } = useCart();
  if (items.length == 0) {
    return (
      <div className="container mx-auto mt-10 p-6">
        <h1 className="mb-4 text-center text-3xl font-bold">Your cart is empty</h1>
      </div>
    );
  }

  if (userId && customerCart) {
    // await syncClientToBackend(customerCart.items, items, userId);
    // for (const customerCartItem of customerCart?.items || []) {
    //   const clientItem = items.find((item) => item.id === customerCartItem.food_id);
    //   if (!clientItem) {
    //     addItem(mapCustomerCartItemToSessionCartItem(customerCartItem));
    //   } else if (customerCartItem.quantity !== clientItem.quantity) {
    //     // Update this item's quantity in client cart
    //     setQuantity(customerCartItem.food_id, customerCartItem.quantity);
    //   }
    // }
  }

  // Remove items from client that are not in server cart
  const totalPrice = items.reduce((total, item) => total + item.price * item.quantity, 0);
  return (
    <div className="container mx-auto mt-10 p-6">
      <h1 className="mb-4 text-3xl font-bold">Your basket</h1>
      <div className="rounded-lg bg-white p-6 shadow-lg">
        <h2 className="mb-4 text-xl font-bold">Total Price: ${totalPrice.toFixed(2)}</h2>
        {items.map((item) => (
          <CartItem
            onQuantityChange={async (id, quantity) => {
              setQuantity(id, quantity);
              if (userId) {
                await updateCartItemQuantity(userId, id, quantity);
              }
            }}
            onRemoveItem={async (id) => {
              removeItem(id);
              if (userId) {
                await deleteCartItem(userId, id);
              }
            }}
            key={item.id}
            {...item}
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
