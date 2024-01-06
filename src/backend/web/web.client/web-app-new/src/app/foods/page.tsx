import React from 'react';
import { fetchCategories, fetchFoodItems } from '../../lib/fetch';
import { FoodsPage } from './foods';

const Page = async () => {
  const [categories, foodItems] = await Promise.all([fetchCategories(), fetchFoodItems()]);
  return <FoodsPage foodItems={foodItems} categories={categories} />;
};

export const dynamic = 'force-dynamic';

export default Page;
