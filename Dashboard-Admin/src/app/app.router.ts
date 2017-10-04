import { RouterModule, Routes } from '@angular/router';
import { FoodListComponent } from "app/components/foods/list.component";
import { AddFoodComponent } from "app/components/foods/add.component";
import { AddCategoryComponent } from "app/components/categories/add.component";
import { ListCategoriesComponent } from "app/components/categories/list.component";
import { AddDailyEatingComponent } from "app/components/dailyeatings/add.component";

export const appRoutes: Routes = [
    { path: 'foods', component: FoodListComponent },
    { path: 'foods/create', component: AddFoodComponent },
    { path: 'foods/create/:id', component: AddFoodComponent },
    { path: 'categories', component: ListCategoriesComponent },
    { path: 'categories/create', component: AddCategoryComponent },
    { path: 'categories/create/:id', component: AddCategoryComponent },
    { path: 'dailyeatings', component: AddDailyEatingComponent },
];

export const routes: any = RouterModule.forRoot(appRoutes);