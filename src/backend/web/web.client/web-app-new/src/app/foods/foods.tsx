import CategoriesSidebar from "@/components/CategoriesSidebar";
import FoodItem from "@/components/FoodItem";
import { FoodItems } from "./fetch";

export const FoodsPage = ({foodItems}: {foodItems: FoodItems}) => {
  return (
    <div className="flex flex-1 min-h-screen">
      <CategoriesSidebar />
      <div className="container mx-auto">
        <div className="flex flex-wrap justify-center">
          {foodItems.map((item) => (
            <FoodItem key={item.id} {...item} />
          ))}
        </div>
      </div>
    </div>
  );
};
