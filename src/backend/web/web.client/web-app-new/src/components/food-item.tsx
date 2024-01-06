'use client';

import { useCart } from '@/context/cart-context';
import React, { useState } from 'react';
import Image from 'next/image';
import { CartItemQuantity } from './cart/cart-item-quantity';

type FoodItemProps = {
  id: number;
  name: string;
  description: string;
  price: number;
  image: string;
};

const FoodItem: React.FC<FoodItemProps> = ({ id, name, description, price, image }) => {
  const { addItem } = useCart();
  const [quantity, setQuantity] = useState(1);

  const addToCart = () => {
    addItem({
      id,
      name,
      description,
      quantity,
      price,
      image
    });
  };

  return (
    <div className="m-4 max-w-sm overflow-hidden rounded shadow-lg">
      <div style={{ position: 'relative', height: '400px' }}>
        <Image
          src={image}
          alt={name}
          placeholder="empty"
          loading="lazy"
          fill={true}
          sizes="(max-width: 768px) 100vw, 33vw"
          style={{
            objectFit: 'cover'
          }}
        />
      </div>

      <div className="px-6 py-4">
        <div className="mb-2 text-xl font-bold">{name}</div>
        <p className="text-base text-gray-700">{description}</p>
        <div className="mt-4 flex items-center justify-between">
          <span className="text-lg font-bold">Rs. {price}</span>
          <CartItemQuantity quantity={quantity} onQuantityChange={setQuantity} />
        </div>
      </div>
      <div className="px-6 pb-2 pt-4">
        <button
          onClick={addToCart}
          className="w-full rounded bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-700"
        >
          Add to Cart
        </button>
      </div>
    </div>
  );
};

export default FoodItem;
