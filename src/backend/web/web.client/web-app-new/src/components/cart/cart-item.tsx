import React from 'react';
import Image from 'next/image';
import { TrashIcon } from '@heroicons/react/24/solid';
import { CartItemQuantity } from './cart-item-quantity';

// Type for the cart item
type CartItemProps = {
  id: number;
  name: string;
  description: string;
  quantity: number;
  price: number;
  image: string;
  onQuantityChange: (_itemId: number, _newQuantity: number) => void;
  onRemoveItem: (_itemId: number) => void;
};

export const CartItemInfo: React.FC<{
  name: string;
  description: string;
  price: number;
  image: string;
}> = ({ name, description, price, image }) => {
  return (
    <div className="flex items-center">
      <Image src={image} alt={name} quality={75} width={60} height={60} className="rounded-full" />
      <div className="ml-4">
        <h3 className="text-lg font-bold">{name}</h3>
        <p className="text-sm text-gray-600">{description}</p>
        <p className="text-sm font-bold">Total: ${price.toFixed(2)}</p>
      </div>
    </div>
  );
};

export const CartItemActionButton: React.FC<{
  onQuantityChange: (_itemId: number, _newQuantity: number) => void;
  onRemoveItem: (_itemId: number) => void;
  id: number;
  quantity: number;
}> = ({ id, quantity, onQuantityChange, onRemoveItem }) => {
  return (
    <div className="flex items-center">
      <CartItemQuantity
        className="relative px-4"
        quantity={quantity}
        onQuantityChange={(newQuantity) => onQuantityChange(id, newQuantity)}
      />
      <button className="text-gray-500 hover:text-red-500" onClick={() => onRemoveItem(id)}>
        <TrashIcon className="h-6 w-6" />
      </button>
    </div>
  );
};

// A single cart item component
export const CartItem: React.FC<CartItemProps> = ({
  id,
  name,
  description,
  quantity,
  image,
  price,
  onQuantityChange,
  onRemoveItem
}) => {
  return (
    <div className="flex items-center justify-between border-b border-gray-200 py-4">
      <CartItemInfo name={name} description={description} price={price} image={image} />
      <CartItemActionButton
        id={id}
        quantity={quantity}
        onQuantityChange={onQuantityChange}
        onRemoveItem={onRemoveItem}
      />
    </div>
  );
};
