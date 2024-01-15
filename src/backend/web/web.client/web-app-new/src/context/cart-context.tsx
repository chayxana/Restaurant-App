'use client';

// import { useSession } from 'next-auth/react';
import React, { PropsWithChildren, createContext, useContext, useEffect, useState } from 'react';

export interface SessionCartItem {
  id: number;
  name: string;
  description: string;
  quantity: number;
  price: number;
  image: string;
}

interface CartContextType {
  items: SessionCartItem[];
  customerId?: string;
  addItem: (_item: SessionCartItem) => void;
  removeItem: (_itemId: number) => void;
  clearCart: () => void;
  setQuantity: (_id: number, _quantity: number) => void;
}

// Create the context
const CartContext = createContext<CartContextType | undefined>(undefined);

// Define the provider component
export const CartProvider = ({ children }: PropsWithChildren<{}>) => {
  const [items, setItems] = useState<SessionCartItem[]>([]);

  useEffect(() => {
    const storedItems = localStorage.getItem('items');
    if (storedItems) {
      setItems(JSON.parse(storedItems));
    }
  }, []);

  useEffect(() => {
    if (items.length === 0) {
      localStorage.removeItem('items');
    } else {
      localStorage.setItem('items', JSON.stringify(items));
    }
  }, [items]);

  const addOrUpdateQuantity = (
    prevItems: SessionCartItem[],
    newItem: SessionCartItem
  ): SessionCartItem[] => {
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
  };

  const addItem = (newItem: SessionCartItem) => {
    setItems((prevItems) => {
      return addOrUpdateQuantity(prevItems, newItem);
    });
  };

  const removeItem = (itemId: number) => {
    setItems((prevItems) => prevItems.filter((item) => item.id !== itemId));
  };

  const clearCart = () => {
    setItems([]);
  };

  const setQuantity = (id: number, quantity: number) => {
    setItems((prevItems) =>
      prevItems.map((item) => (item.id === id ? { ...item, quantity } : item))
    );
  };

  return (
    <CartContext.Provider
      value={{
        items,
        addItem,
        removeItem,
        clearCart,
        setQuantity
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
