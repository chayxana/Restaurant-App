'use client';

import { UserCircleIcon } from '@heroicons/react/24/solid';
import { signIn, signOut } from 'next-auth/react';
import { useState } from 'react';

export default function OpenUserProfile({ authStatus }: { authStatus: string }) {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  return (
    <div className="relative">
      <div className="flex h-11 w-11 items-center justify-center rounded-md border border-neutral-200 text-black transition-colors">
        <button onClick={() => setIsDropdownOpen(!isDropdownOpen)} type="button">
          <UserCircleIcon className="h-6 w-6" />
        </button>
      </div>

      {isDropdownOpen && (
        <div className="absolute right-0 mt-2 w-48 rounded-md bg-white py-2 shadow-xl">
          {authStatus === 'unauthenticated' && (
            <button onClick={() => signIn()} className="w-full">
              <div className="block w-full px-4 py-2 text-left text-sm text-gray-700 hover:bg-gray-100">
                Login
              </div>
            </button>
          )}
          {authStatus === 'authenticated' && (
            <button onClick={() => signOut()} className="w-full">
              <div className="block px-4 py-2 text-left text-sm text-gray-700 hover:bg-gray-100">
                Log Out
              </div>
            </button>
          )}
        </div>
      )}
    </div>
  );
}
