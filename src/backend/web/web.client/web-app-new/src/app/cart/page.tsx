'use client'

import React from "react";
import Link from "next/link";
import { useCart } from "@/context/CartContext";
import { CartItem } from "@/components/cart/CartItem";

const CartPage: React.FC = () => {
  const { items } = useCart();

  // Calculate the total price
  const totalPrice = items.reduce(
    (total, item) => total + item.price * item.quantity,
    0
  );

  return (
    <div className="container mx-auto mt-10 p-6">
      <h1 className="text-3xl font-bold mb-4">Your basket</h1>
      <div className="bg-white shadow-lg rounded-lg p-6">
        <h2 className="text-xl font-bold mb-4">
          Total Price: ${totalPrice.toFixed(2)}
        </h2>
        {items.map((item) => (
          <CartItem key={item.id} {...item} />
        ))}
        <Link href="/checkout">
          <div className="flex justify-end mt-6">
            <button className="bg-orange-500 hover:bg-orange-600 text-white font-bold py-2 px-4 rounded-full">
              Checkout
            </button>
          </div>
        </Link>

      </div>
    </div>
  );
};

export default CartPage;
