"use client";

import { useCart } from "@/context/CartContext";
import React, { useState } from "react";
import Image from "next/image";
import { CartItemQuantity } from "./cart/CartItemQuantity";

type FoodItemProps = {
  id: number;
  name: string;
  description: string;
  price: number;
  image: string;
};

const FoodItem: React.FC<FoodItemProps> = ({
  id,
  name,
  description,
  price,
  image,
}) => {
  const { addItem } = useCart();
  const [quantity, setQuantity] = useState(1);

  const addToCart = () => {
    addItem({
      id,
      name,
      description,
      quantity,
      price,
      image,
    });
  };

  return (
    <div className="max-w-sm rounded overflow-hidden shadow-lg m-4">
      <div style={{ position: "relative", height: "400px" }}>
        <Image
          src={image}
          alt={name}
          placeholder="empty"
          loading="lazy"
          fill={true}
          sizes="(max-width: 768px) 100vw, 33vw"
          style={{
            objectFit: "cover",
          }}
        />
      </div>

      <div className="px-6 py-4">
        <div className="font-bold text-xl mb-2">{name}</div>
        <p className="text-gray-700 text-base">{description}</p>
        <div className="flex justify-between items-center mt-4">
          <span className="text-lg font-bold">Rs. {price}</span>
          <CartItemQuantity quantity={quantity} onQuantityChange={setQuantity}  />
        </div>
      </div>
      <div className="px-6 pt-4 pb-2">
        <button
          onClick={addToCart}
          className="bg-orange-500 hover:bg-orange-700 text-white font-bold py-2 px-4 rounded w-full"
        >
          Add to Cart
        </button>
      </div>
    </div>
  );
};

export default FoodItem;
