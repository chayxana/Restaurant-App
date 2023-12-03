// components/Navbar.tsx
'use client'
import React, { useState } from 'react';
import Link from 'next/link';
import { MagnifyingGlassIcon, ShoppingCartIcon } from '@heroicons/react/24/solid'
import { useCart } from '@/context/CartContext';

const Navbar: React.FC = () => {
  const [searchQuery, setSearchQuery] = useState('');
  const { items } = useCart();
  
  const handleSearch = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    // Implement the search functionality here
    console.log(`Searching for: ${searchQuery}`);
  };

  return (
    <nav className="bg-white shadow-lg">
      <div className="max-w-6xl mx-auto px-4">
        <div className="flex justify-between">
          <div className="flex space-x-7">
            <div>
              {/* Website Logo */}
              <Link href="/">
                <div className="flex items-center py-4 px-2">
                  {/* Insert your logo image or text here */}
                  <span className="font-semibold text-gray-500 text-lg">Food & Drinks App</span>
                </div>
              </Link>
            </div>
            {/* Primary Navbar items */}
            <div className="hidden md:flex items-center space-x-1">
              <Link href="/foods">
                <div className="py-4 px-2 text-orange-500 border-b-4 border-orange-500 font-semibold ">Foods</div>
              </Link>
              <Link href="/drinks">
                <div className="py-4 px-2 text-gray-500 font-semibold hover:text-orange-500 transition duration-300">Drinks</div>
              </Link>
              {/* Add other navigation items here */}
            </div>
          </div>
          {/* Secondary Navbar items */}
          <div className="hidden md:flex items-center space-x-3 ">
            <form onSubmit={handleSearch} className="flex items-center">
              <input
                className="py-2 pl-4 pr-3 rounded-l-lg focus:outline-none focus:ring-2 focus:ring-orange-500 focus:border-transparent"
                type="text"
                placeholder="Search..."
                onChange={(e) => setSearchQuery(e.target.value)}
              />
              <button
                type="submit"
                className="p-2 bg-orange-500 text-white rounded-r-lg"
              >
                <MagnifyingGlassIcon className="h-5 w-5" />
              </button>
            </form>
            <Link href="/cart">
              <div className="py-2 px-2 flex items-center">
                <ShoppingCartIcon className="h-6 w-6 text-gray-500 hover:text-orange-500" />
                <span className="text-gray-500 text-sm ml-1">({items.length})</span>
              </div>
            </Link>
          </div>
          {/* Mobile menu button */}
          <div className="md:hidden flex items-center">
            <button className="outline-none mobile-menu-button">
              <svg
                className="w-6 h-6 text-gray-500 hover:text-orange-500"
                x-show="!showMenu"
                fill="none"
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path d="M4 6h16M4 12h16m-7 6h7"></path>
              </svg>
            </button>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
