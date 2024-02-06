'use client';

import { UserCircleIcon } from '@heroicons/react/24/solid';
import { signIn, signOut, useSession } from 'next-auth/react';
import { Menu } from '@headlessui/react';

export default function OpenUserProfile() {
  const { data, status } = useSession();
  return (
    <div className="relative">
      <Menu>
        <Menu.Button>
          <div className="flex h-11 w-11 items-center justify-center rounded-md border border-neutral-200 text-black">
            <UserCircleIcon className="h-6 w-6" />
          </div>
        </Menu.Button>
        <Menu.Items className="absolute right-0 mt-2 w-56 origin-top-right divide-y divide-gray-100 rounded-md bg-white shadow-lg ring-1 ring-black/5 focus:outline-none">
          <Menu.Item>
            {status === 'unauthenticated' ? (
              <button
                onClick={() => signIn()}
                className="group flex w-full items-center rounded-md px-2 py-2 text-sm text-gray-900"
              >
                Login
              </button>
            ) : (
              <div className="p-2">
                <ul className="x-4 group py-2 text-left text-sm text-gray-700">
                  <li>UserID: {data?.user?.user_id}</li>
                </ul>
                <button
                  onClick={() => signOut()}
                  className="flex  w-full items-center rounded-md text-sm text-gray-900"
                >
                  Log Out
                </button>
              </div>
            )}
          </Menu.Item>
        </Menu.Items>
      </Menu>
    </div>
  );
}
