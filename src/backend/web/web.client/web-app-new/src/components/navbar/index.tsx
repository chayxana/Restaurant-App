"use client";
import React from "react";
import Link from "next/link";
import { UserCircleIcon } from "@heroicons/react/24/solid";
import { useCart } from "@/context/CartContext";
import OpenCart from "../cart/OpenCart";
import OpenUserProfile from "./user-profile";
import Search from "./search";
import { useSession } from "next-auth/react";

const Navbar: React.FC = () => {
  const { items } = useCart();
  const { status } = useSession();

  return (
    <nav className="bg-white shadow-md fixed w-full z-30 top-0 left-0">
      <div className="max-w-6xl mx-auto px-4 flex justify-between items-center h-16">
        <div className="flex w-full md:w-1/3">
          <Link href="/">
            <div className="flex items-center space-x-2">
              <UserCircleIcon className="h-8 w-8" />
              <span className="font-semibold text-gray-500 text-lg">
                Restaurant App
              </span>
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
          <OpenUserProfile authStatus={status} />
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
