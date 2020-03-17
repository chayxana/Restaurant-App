import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FoodListComponent } from './list/list.component';
import { AuthGuard } from 'app/services/auth.guard';
import { AddFoodComponent } from './add/add.component';

const routes: Routes = [
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
    path: 'foods/edit/:id',
    component: AddFoodComponent,
    canActivate: [AuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FoodsRoutingModule { }
