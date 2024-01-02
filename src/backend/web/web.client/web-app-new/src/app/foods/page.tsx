import React from "react";
import { fetchFoodItems } from "./fetch";
import { FoodsPage } from "./foods";


const Page = async () => {
  const foodItems = await fetchFoodItems();
  return (
    <FoodsPage foodItems={foodItems} />
  );
};

export default Page;
