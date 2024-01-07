'use client';
import React from 'react';
import Link from 'next/link';
import { UserCircleIcon } from '@heroicons/react/24/solid';
import { useCart } from '@/context/cart-context';
import OpenCart from '../cart/open-cart';
import OpenUserProfile from './user-profile';
import Search from './search';

const Navbar: React.FC = () => {
  const { items } = useCart();

  return (
    <nav className="fixed left-0 top-0 z-30 w-full bg-white shadow-md">
      <div className="mx-auto flex h-16 max-w-6xl items-center justify-between px-4">
        <div className="flex w-full md:w-1/3">
          <Link href="/">
            <div className="flex items-center space-x-2">
              <UserCircleIcon className="h-8 w-8" />
              <span className="text-lg font-semibold text-gray-500">Restaurant App</span>
            </div>
          </Link>
        </div>

        <div className="hidden justify-center md:flex md:w-1/3">
          <Search />
        </div>

        <div className="flex justify-end space-x-2 md:w-1/3">
          <Link href="/cart">
            <OpenCart quantity={items.length} className="h-6 w-6" />
          </Link>
          <OpenUserProfile />
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
