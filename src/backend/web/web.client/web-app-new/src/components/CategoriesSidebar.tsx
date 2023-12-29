import React from "react";
import Link from "next/link";
import { z } from "zod";

type CategoryProps = {
  name: string;
  id: number;
  link: string;
};

const CategoryLink: React.FC<CategoryProps> = ({ name, link }) => {
  return (
    <Link href={link}>
      <div className="block px-4 py-2 hover:bg-gray-100">{name}</div>
    </Link>
  );
};

const CategoriesSidebar: React.FC = async () => {
  const categories = await fetchCategories();

  return (
    <aside className="pt-8 min-h-screen p-5 bg-gray-100">
      <nav>
        {categories.map((category) => {
          const values = {
            id: category.id,
            name: category.name,
            link: "/foods/" + category.name,
          };
          return <CategoryLink key={category.id} {...values} />;
        })}
      </nav>
    </aside>
  );
};

const Categories = z.array(
  z.object({
    id: z.number(),
    name: z.string(),
  })
);

type Categories = z.infer<typeof Categories>;

async function fetchCategories(): Promise<Categories> {
  const apiUrl = process.env.BASE_URL + "/catalog/categories";
  const res = await fetch(apiUrl);
  if (!res.ok) {
    throw new Error("Failed to fetch categories data");
  }

  const categories: Categories = Categories.parse(
    await res.json()
  );
  return categories;
}

export default CategoriesSidebar;
