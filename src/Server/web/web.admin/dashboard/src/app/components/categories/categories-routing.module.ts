import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddCategoryComponent } from './add/add.component';
import { AuthGuard } from 'app/services/auth.guard';
import { ListCategoriesComponent } from './list/list.component';

const routes: Routes = [
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
    path: 'categories/edit/:id',
    component: AddCategoryComponent,
    data: {
      breadcrumb: 'Edit'
    },
    canActivate: [AuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }
