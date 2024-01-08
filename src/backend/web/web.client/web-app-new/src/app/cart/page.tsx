'use client';

import React from 'react';
import Link from 'next/link';
import { useCart } from '@/context/cart-context';
import { CartItem } from '@/components/cart/cart-item';

const CartPage: React.FC = () => {
  const { items } = useCart();

  // Calculate the total price
  const totalPrice = items.reduce((total, item) => total + item.price * item.quantity, 0);

  return (
    <div className="container mx-auto mt-10 p-6">
      <h1 className="mb-4 text-3xl font-bold">Your basket</h1>
      <div className="rounded-lg bg-white p-6 shadow-lg">
        <h2 className="mb-4 text-xl font-bold">Total Price: ${totalPrice.toFixed(2)}</h2>
        {items.map((item) => (
          <CartItem key={item.id} {...item} />
        ))}
        <Link href="/checkout">
          <div className="mt-6 flex justify-end">
            <button className="w-1/3 rounded-full bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-600">
              Checkout
            </button>
          </div>
        </Link>
      </div>
    </div>
  );
};

export default CartPage;
