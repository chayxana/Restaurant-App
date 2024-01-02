'use client'
import React, { useState } from 'react';
import { ChevronDownIcon } from '@heroicons/react/20/solid';

const RightSidebar: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedSort, setSelectedSort] = useState('Relevance');

  const sortingOptions = ['Relevance', 'Trending', 'Latest arrivals', 'Price: Low to high', 'Price: High to low'];

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
          className="bg-white border border-gray-300 text-gray-700 py-2 px-4 flex items-center justify-between w-48 rounded-md shadow"
        >
          <span>Sort by</span>
          <ChevronDownIcon className={`w-5 h-5 ${isOpen ? 'rotate-180' : ''}`} />
        </button>

        {isOpen && (
          <div className="absolute right-0 w-48 bg-white border border-gray-300 rounded-md shadow mt-1 z-10">
            {sortingOptions.map((option) => (
              <div
                key={option}
                onClick={() => handleSortSelection(option)}
                className={`px-4 py-2 hover:bg-gray-100 cursor-pointer ${selectedSort === option ? 'bg-gray-100' : ''}`}
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
