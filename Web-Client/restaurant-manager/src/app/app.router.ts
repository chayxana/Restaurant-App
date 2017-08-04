import { RouterModule, Routes } from '@angular/router';
import { FoodListComponent } from "app/components/foods/list.component";
import { AddFoodComponent } from "app/components/foods/add.component";
import { AddCategoryComponent } from "app/components/categories/add.component";

export const appRoutes: Routes = [
    { path: 'foods', component: FoodListComponent },
    { path: 'foods/create', component: AddFoodComponent },
    { path: 'categories', component: AddCategoryComponent }
];

export const routes: any = RouterModule.forRoot(appRoutes);