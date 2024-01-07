'use client';

import React from 'react';
import Link from 'next/link';
import { Categories } from '@/lib/types/food-item';

type CategoryProps = {
  name: string;
  id: number;
  link: string;
};

const CategoryLink: React.FC<CategoryProps> = ({ name, link }) => {
  return (
    <Link href={link}>
      <li className="block px-4 py-2 hover:bg-gray-100">{name}</li>
    </Link>
  );
};

const CategoriesSidebar = ({ categories }: { categories: Categories }) => {
  return (
    <nav>
      <ul>
        {categories.map((category) => {
          const values = {
            id: category.id,
            name: category.name,
            link: '/foods/' + category.name
          };
          return <CategoryLink key={category.id} {...values} />;
        })}
      </ul>
    </nav>
  );
};

export default CategoriesSidebar;
