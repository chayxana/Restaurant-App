import { RouterModule, Routes } from '@angular/router';
import { FoodListComponent } from 'app/components/foods/list.component';
import { AddFoodComponent } from 'app/components/foods/add.component';
import { AddCategoryComponent } from 'app/components/categories/add.component';
import { ListCategoriesComponent } from 'app/components/categories/list.component';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { AuthGuard } from './services/auth.guard';
import { LoginComponent } from './components/account/login/login.component';

export const appRoutes: Routes = [
  {
    path: 'auth-callback',
    component: AuthCallbackComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'foods',
    component: FoodListComponent,
    data: {
      breadcrumb: 'Foods'
    },
    canActivate: [AuthGuard]
  },
  {
    path: 'foods/create',
    component: AddFoodComponent,
    data: {
      breadcrumb: 'Create'
    },
    canActivate: [AuthGuard]
  },
  {
    path: 'foods/create/:id',
    component: AddFoodComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'categories',
    component: ListCategoriesComponent,
    data: {
      breadcrumb: 'Categories'
    },
    canActivate: [AuthGuard]
  },
  {
    path: 'categories/create',
    component: AddCategoryComponent,
    data: {
      breadcrumb: 'Create'
    },
    canActivate: [AuthGuard]
  },
  {
    path: 'categories/create/:id',
    component: AddCategoryComponent,
    data: {
      breadcrumb: 'Edit'
    },
    canActivate: [AuthGuard]
  },
];

export const routes: any = RouterModule.forRoot(appRoutes);
