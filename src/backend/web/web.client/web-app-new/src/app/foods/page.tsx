import React from "react";
import { fetchFoodItems } from "../../lib/fetch";
import { FoodsPage } from "./foods";


const Page = async () => {
  const foodItems = await fetchFoodItems();
  return (
    <FoodsPage foodItems={foodItems} />
  );
};

export const dynamic = 'force-dynamic'

export default Page;
