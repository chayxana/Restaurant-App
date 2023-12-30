"use client";

import { UserCircleIcon } from "@heroicons/react/24/solid";
import Link from "next/link";
import { useState } from "react";

export default function OpenUserProfile() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  return (
    <div className="relative">
      <div className="flex h-11 w-11 items-center justify-center rounded-md border border-neutral-200 text-black transition-colors">
        <button
          onClick={() => setIsDropdownOpen(!isDropdownOpen)}
          type="button"
        >
          <UserCircleIcon className="h-6 w-6" />
        </button>
      </div>

      {isDropdownOpen && (
        <div className="absolute right-0 mt-2 py-2 w-48 bg-white rounded-md shadow-xl">
          {/* Dropdown items */}
          <Link href="/profile">
            <div className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">
              Profile
            </div>
          </Link>
          <Link href="/api/auth/signin">
            <div className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">
              Login
            </div>
          </Link>
        </div>
      )}
    </div>
  );
}
