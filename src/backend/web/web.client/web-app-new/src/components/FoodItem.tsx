"use client";

import { useCart } from "@/context/CartContext";
import React, { useState } from "react";
import Image from "next/image";

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
  const [quantity, setQuantity] = useState(1);
  const { addItem } = useCart();

  const handleQuantityChange = (newQuantity: number) => {
    if (newQuantity > 0) {
      setQuantity(newQuantity);
    }
  };

  const addToCart = () => {
    addItem({
      id,
      name,
      quantity,
      price,
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
          <div className="flex items-center">
            <button
              onClick={() => handleQuantityChange(quantity - 1)}
              className="text-md bg-gray-200 text-gray-600 px-2 py-1 rounded-l"
            >
              -
            </button>
            <input
              type="text"
              className="w-12 text-center border-t border-b"
              value={quantity}
              onChange={(e) =>
                handleQuantityChange(parseInt(e.target.value) || 0)
              }
            />
            <button
              onClick={() => handleQuantityChange(quantity + 1)}
              className="text-md bg-gray-200 text-gray-600 px-2 py-1 rounded-r"
            >
              +
            </button>
          </div>
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
