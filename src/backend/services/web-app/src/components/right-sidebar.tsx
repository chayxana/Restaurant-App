'use client';
import React, { useState } from 'react';
import { ChevronDownIcon } from '@heroicons/react/20/solid';

const RightSidebar: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedSort, setSelectedSort] = useState('Relevance');

  const sortingOptions = [
    'Relevance',
    'Trending',
    'Latest arrivals',
    'Price: Low to high',
    'Price: High to low'
  ];

  const handleSortSelection = (option: string) => {
    setSelectedSort(option);
    setIsOpen(false);
    // Implement the sorting functionality here
  };

  return (
    <div className="absolute right-0 mr-4 mt-4">
      <div className="relative">
        <button
          onClick={() => setIsOpen(!isOpen)}
          className="flex w-48 items-center justify-between rounded-md border border-gray-300 bg-white px-4 py-2 text-gray-700 shadow"
        >
          <span>Sort by</span>
          <ChevronDownIcon className={`h-5 w-5 ${isOpen ? 'rotate-180' : ''}`} />
        </button>

        {isOpen && (
          <div className="absolute right-0 z-10 mt-1 w-48 rounded-md border border-gray-300 bg-white shadow">
            {sortingOptions.map((option) => (
              <div
                key={option}
                onClick={() => handleSortSelection(option)}
                className={`cursor-pointer px-4 py-2 hover:bg-gray-100 ${
                  selectedSort === option ? 'bg-gray-100' : ''
                }`}
              >
                {option}
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default RightSidebar;
