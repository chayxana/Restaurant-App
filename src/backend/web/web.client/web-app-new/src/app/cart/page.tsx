import React from "react";
import Image from "next/image";
import { TrashIcon } from "@heroicons/react/24/solid";
import Link from "next/link";

// Type for the cart item
type CartItemProps = {
  id: string;
  title: string;
  description: string;
  quantity: number;
  price: number;
  imageUrl: string;
  // Add more props as needed
};

// A single cart item component
const CartItem: React.FC<CartItemProps> = ({
  id,
  title,
  description,
  quantity,
  price,
  imageUrl,
}) => {
  // Functions to handle item quantity changes and removal will be added here
  return (
    <div className="flex items-center justify-between border-b border-gray-200 py-4">
      <div className="flex items-center">
        <Image
          src={imageUrl}
          alt={title}
          width={60}
          height={60}
          className="rounded-full"
        />
        <div className="ml-4">
          <h3 className="text-lg font-bold">{title}</h3>
          <p className="text-sm text-gray-600">{description}</p>
          <p className="text-sm font-bold">Total: ${price.toFixed(2)}</p>
        </div>
      </div>
      <div className="flex items-center">
        <button className="text-gray-500 hover:text-red-500">
          <TrashIcon className="h-6 w-6" />
        </button>
        <div className="ml-4">
          <span className="text-lg font-bold">{quantity}</span>
        </div>
      </div>
    </div>
  );
};

// The main cart page component
const CartPage: React.FC = () => {
  // Sample cart items data
  const cartItems = [
    {
      id: "123",
      price: 50,
      quantity: 10,
      title: "Hamburger",
      description:
        "A delicious hamburger with cheese, lettuce, tomato, bacon, onion, pickles, and chili.",
      imageUrl:
        "https://img.buzzfeed.com/thumbnailer-prod-us-east-1/video-api/assets/165384.jpg", // Replace with your image path
    },
  ];

  // Calculate the total price
  const totalPrice = cartItems.reduce(
    (total, item) => total + item.price * item.quantity,
    0
  );

  return (
    <div className="container mx-auto mt-10">
      <h1 className="text-3xl font-bold mb-4">Your basket</h1>
      <div className="bg-white shadow-lg rounded-lg p-6">
        <h2 className="text-xl font-bold mb-4">
          Total Price: ${totalPrice.toFixed(2)}
        </h2>
        {cartItems.map((item) => (
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
