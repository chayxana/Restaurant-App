import React, { Suspense } from 'react';
import Link from 'next/link';
import { UserCircleIcon } from '@heroicons/react/24/solid';
import Search from './search';
import OpenCart from '../cart/open-cart';
import OpenUserProfile from './user-profile';

export default async function Navbar() {
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
            <Suspense fallback={<div>loading</div>}>
              <OpenCart />
            </Suspense>
          </Link>
          <OpenUserProfile />
        </div>
      </div>
    </nav>
  );
}
