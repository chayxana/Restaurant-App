import CategoriesSidebar from "@/components/CategoriesSidebar";
import FoodItem from "@/components/FoodItem";
import RightSidebar from "@/components/RightSidebar";
import { FoodItems } from "@/lib/types";

export const FoodsPage = ({ foodItems }: { foodItems: FoodItems }) => {
  return (
    <div className="mx-auto flex max-w-screen-2xl flex-col gap-8 px-4 pb-4 md:flex-row">
      <div className="order-first w-full flex-none md:max-w-[125px]">
        <CategoriesSidebar />
      </div>
      <div className="order-last min-h-screen w-full md:order-none">
        <div className="flex flex-wrap justify-center">
          {foodItems.map((item) => (
            <FoodItem key={item.id} {...item} />
          ))}
        </div>
      </div>
      <RightSidebar />
    </div>
  );
};
