import React from "react";
import { fetchFoodItemsByCategory } from "../../../lib/fetch";
import { FoodsPage } from "../foods";

const Page = async ({ params }: { params: { category: string } }) => {
  const foodItems = await fetchFoodItemsByCategory(params.category);
  return (
    <FoodsPage foodItems={foodItems}></FoodsPage>
  );
};

export default Page;
