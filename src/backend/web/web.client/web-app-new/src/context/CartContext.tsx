'use client'

import React, { PropsWithChildren, createContext, useContext, useState } from "react";

// Define the shape of the context state
interface CartItem {
  id: string;
  title: string;
  quantity: number;
  price: number;
}

interface CartContextType {
  items: CartItem[];
  addItem: (item: CartItem) => void;
  removeItem: (itemId: string) => void;
  clearCart: () => void;
}

// Create the context
const CartContext = createContext<CartContextType | undefined>(undefined);

// Define the provider component
export const CartProvider = ({ children }: PropsWithChildren<{}>) => {
  const [items, setItems] = useState<CartItem[]>([]);

  const addItem = (newItem: CartItem) => {
    setItems((prevItems) => {
      // Check if item is already in the cart
      const itemIndex = prevItems.findIndex((item) => item.id === newItem.id);
      if (itemIndex > -1) {
        // Update quantity if item exists
        const newItems = [...prevItems];
        newItems[itemIndex].quantity += newItem.quantity;
        return newItems;
      } else {
        // Add new item to cart
        return [...prevItems, newItem];
      }
    });
  };

  const removeItem = (itemId: string) => {
    setItems((prevItems) => prevItems.filter((item) => item.id !== itemId));
  };

  const clearCart = () => {
    setItems([]);
  };

  return (
    <CartContext.Provider value={{ items, addItem, removeItem, clearCart }}>
      {children}
    </CartContext.Provider>
  );
};

// Hook to use the cart context
export const useCart = () => {
  const context = useContext(CartContext);
  if (context === undefined) {
    throw new Error("useCart must be used within a CartProvider");
  }
  return context;
};
