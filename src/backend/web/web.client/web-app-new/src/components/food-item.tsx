'use client';

import React, { useState } from 'react';
import Image from 'next/image';
import { CartItemQuantity } from './cart/cart-item-quantity';
import { AddToCart } from './cart/add-to-cart';
import { FoodItem } from '@/lib/types/food-item';

export const Item = ({ foodItem }: { foodItem: FoodItem }) => {
  const [quantity, setQuantity] = useState(1);
  return (
    <div className="m-4 max-w-sm overflow-hidden rounded shadow-lg">
      <div style={{ position: 'relative', height: '400px' }}>
        <Image
          src={foodItem.image}
          alt={foodItem.name}
          placeholder="empty"
          loading="lazy"
          fill={true}
          sizes="(max-width: 768px) 100vw, 33vw"
          style={{
            objectFit: 'cover'
          }}
        />
      </div>

      <div className="px-6 py-2">
        <div className="mb-2 text-xl font-bold">{foodItem.name}</div>
        <p className="text-base text-gray-700">{foodItem.description}</p>
        <div className="mt-4 flex items-center justify-between">
          <span className="text-lg font-bold">Rs. {foodItem.price}</span>
          <CartItemQuantity quantity={quantity} onQuantityChange={setQuantity} />
        </div>
      </div>
      <AddToCart quantity={quantity} foodItem={foodItem} />
    </div>
  );
};
