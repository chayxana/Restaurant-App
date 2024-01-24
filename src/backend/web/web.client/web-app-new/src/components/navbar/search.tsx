'use client';

import { useRouter, useSearchParams } from 'next/navigation';

import { MagnifyingGlassIcon } from '@heroicons/react/24/solid';

export default function Search() {
  const searchParams = useSearchParams();
  const router = useRouter();

  const handleSearch = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const val = e.target as HTMLFormElement;
    const search = val.search as HTMLInputElement;
    const newParams = new URLSearchParams(searchParams.toString());

    if (search.value) {
      newParams.set('q', search.value);
    } else {
      newParams.delete('q');
    }

    router.push('');
  };

  return (
    <form onSubmit={handleSearch} className="w-max-[550px] relative w-full lg:w-80 xl:w-full">
      <input
        className="w-full rounded-lg border border-gray-300 px-4 py-2 focus:ring-2 focus:ring-orange-500"
        type="text"
        placeholder="Search for items..."
      />
      <button type="submit" className="absolute right-4 top-1/2 -translate-y-1/2 transform">
        <MagnifyingGlassIcon className="h-5 w-5 text-gray-600" />
      </button>
    </form>
  );
}
