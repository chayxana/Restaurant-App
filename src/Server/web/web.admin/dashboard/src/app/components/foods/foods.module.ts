import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FoodsRoutingModule } from './foods-routing.module';
import { AddFoodComponent } from './add/add.component';
import { FoodListComponent } from './list/list.component';
import { FoodService } from 'app/services/food.service';
import { SharedModule } from 'app/shared.module';

@NgModule({
  declarations: [
    AddFoodComponent,
    FoodListComponent,
  ],
  imports: [
    SharedModule,
    CommonModule,
    FoodsRoutingModule,
  ],
  providers: [FoodService]
})
export class FoodsModule { }
