import CategoriesSidebar from '@/components/categories-sidebar';
import { Item } from '@/components/food-item';
// import RightSidebar from '@/components/right-sidebar';
import { Categories, FoodItems } from '@/lib/types/food-item';

export const FoodsPage = ({
  foodItems,
  categories
}: {
  foodItems: FoodItems;
  categories: Categories;
}) => {
  return (
    <div className="mx-auto flex max-w-screen-2xl flex-col gap-8 px-4 pb-4 md:flex-row">
      <div className="order-first w-full flex-none md:max-w-[125px]">
        <CategoriesSidebar categories={categories} />
      </div>
      <div className="order-last min-h-screen w-full md:order-none">
        <div className="flex flex-wrap justify-center">
          {foodItems.map((item) => (
            <Item foodItem={item} key={item.id} />
          ))}
        </div>
      </div>
      {/* <RightSidebar /> */}
    </div>
  );
};
