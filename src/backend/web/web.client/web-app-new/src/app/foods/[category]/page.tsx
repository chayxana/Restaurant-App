import { fetchCategories, fetchFoodItemsByCategory } from '@/lib/fetch';
import { FoodsPage } from '../foods';

const Page = async ({ params }: { params: { category: string } }) => {
  const [categories, foodItems] = await Promise.all([
    fetchCategories(),
    fetchFoodItemsByCategory(params.category)
  ]);
  return <FoodsPage foodItems={foodItems} categories={categories}></FoodsPage>;
};

export default Page;
