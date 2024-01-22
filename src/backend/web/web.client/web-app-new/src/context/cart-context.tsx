'use client';

// import { useSession } from 'next-auth/react';
import React, { PropsWithChildren, createContext, useContext, useState } from 'react';

export interface SessionCartItem {
  id: number;
  name: string;
  description: string;
  quantity: number;
  price: number;
  image: string;
}

interface CartContextType {
  count: number;
  increment: () => void;
  decrement: () => void;
}

// Create the context
const CartContext = createContext<CartContextType | undefined>(undefined);

// Define the provider component
export const CartProvider = ({ children }: PropsWithChildren<{}>) => {
  const [count, setCount] = useState(0);

  //Increase counter
  const increment = () => {
    return setCount(count + 1);
  };

  //Decrease counter
  const decrement = () => {
    return setCount(count - 1);
  };

  return (
    <CartContext.Provider
      value={{
        count,
        increment,
        decrement
      }}
    >
      {children}
    </CartContext.Provider>
  );
};

export const useCart = () => {
  const context = useContext(CartContext);
  if (context === undefined) {
    throw new Error('useCart must be used within a CartProvider');
  }
  return context;
};
