import React from 'react';
import { FoodsPage } from './foods';
import { fetchCategories, fetchFoodItems } from '@/lib/fetch';

const Page = async () => {
  const [categories, foodItems] = await Promise.all([fetchCategories(), fetchFoodItems()]);
  return <FoodsPage foodItems={foodItems} categories={categories} />;
};

export const dynamic = 'force-dynamic';

export default Page;
