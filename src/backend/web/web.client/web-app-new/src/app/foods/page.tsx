import React from "react";
import FoodItem from "@/components/FoodItem";
import { z } from "zod";

const Page = async () => {
  const foodItems = await fetchFoodItems();

  return (
    <div className="container mx-auto">
      <div className="flex flex-wrap justify-center">
        {foodItems.map((item) => (
          <FoodItem key={item.id} {...item} />
        ))}
      </div>
    </div>
  );
};

const FoodItemObject = z.array(
  z.object({
    id: z.number(),
    name: z.string(),
    description: z.string(),
    price: z.number(),
    image: z.string(),
    currency: z.string(),
  })
);

type FoodItemType = z.infer<typeof FoodItemObject>;

async function fetchFoodItems(): Promise<FoodItemType> {
  const apiUrl = process.env.BASE_URL + "/catalog/items/all";
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error("Failed to fetch catalog items data");
  }

  const items: FoodItemType = FoodItemObject.parse(await res.json());

  const updatedItems = items.map((item) => {
    const updatedImage = process.env.BASE_URL + item.image;
    return {
      ...item,
      image: updatedImage,
    };
  });

  return updatedItems;
}

export default Page;
