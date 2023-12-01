// components/FoodItem.tsx

import React from 'react';

type FoodItemProps = {
  title: string;
  description: string;
  price: string;
  imageUrl: string;
};

const FoodItem: React.FC<FoodItemProps> = ({ title, description, price, imageUrl }) => {
  return (
    <div className="max-w-sm rounded overflow-hidden shadow-lg m-4">
      <img className="w-full" src={imageUrl} alt={title} />
      <div className="px-6 py-4">
        <div className="font-bold text-xl mb-2">{title}</div>
        <p className="text-gray-700 text-base">{description}</p>
      </div>
      <div className="px-6 pt-4 pb-2">
        <span className="inline-block bg-orange-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">Rs. {price}</span>
      </div>
    </div>
  );
};

export default FoodItem;
