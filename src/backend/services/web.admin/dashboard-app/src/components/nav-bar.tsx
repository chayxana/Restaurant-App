import React from "react";
import {
  HomeIcon,
  ShoppingCartIcon,
  UserIcon,
  ChartBarIcon,
  CubeIcon,
} from "@heroicons/react/24/solid";

const NavBar = () => {
  return (
    <div className="flex flex-col h-screen bg-gray-800 text-white w-60">
      <div className="flex items-center justify-center h-20 shadow-md">
        <h1 className="text-2xl font-bold">Dashboard App</h1>
      </div>
      <div className="flex flex-col p-4">
        <nav>
          <a
            href="/"
            className={`flex items-center p-2 my-2 transition-colors duration-200 justify-start text-white hover:text-gray-800 hover:bg-gray-200 rounded-md`}
          >
            <HomeIcon className="w-5 h-5" />
            <span className="mx-4 text-sm font-normal">Home</span>
          </a>
          <a
            href="/orders"
            className={`flex items-center p-2 my-2 transition-colors duration-200 justify-start text-white hover:text-gray-800 hover:bg-gray-200 rounded-md`}
          >
            <ShoppingCartIcon className="w-5 h-5" />
            <span className="mx-4 text-sm font-normal">Orders</span>
            <span className="flex-grow text-right">12</span>
          </a>
          <a
            href="/products"
            className={`flex items-center p-2 my-2 transition-colors duration-200 justify-start text-white hover:text-gray-800 hover:bg-gray-200 rounded-md`}
          >
            <CubeIcon className="w-5 h-5" />
            <span className="mx-4 text-sm font-normal">Products</span>
          </a>
          <a
            href="/customers"
            className={`flex items-center p-2 my-2 transition-colors duration-200 justify-start text-white hover:text-gray-800 hover:bg-gray-200 rounded-md`}
          >
            <UserIcon className="w-5 h-5" />
            <span className="mx-4 text-sm font-normal">Customers</span>
          </a>
        </nav>
      </div>
    </div>
  );
};

export default NavBar;
