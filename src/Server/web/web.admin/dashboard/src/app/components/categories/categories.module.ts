import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoriesRoutingModule } from './categories-routing.module';
import { ListCategoriesComponent } from './list/list.component';
import { AddCategoryComponent } from './add/add.component';
import { CategoryService } from 'app/services/category.service';
import { SharedModule } from 'app/shared.module';

@NgModule({
  declarations: [
    AddCategoryComponent,
    ListCategoriesComponent
  ],
  imports: [
    SharedModule,
    CommonModule,
    CategoriesRoutingModule,
  ],
  providers: [CategoryService]
})
export class CategoriesModule { }
