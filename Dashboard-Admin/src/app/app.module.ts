import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { MenuComponent } from './components/menu/menu.component';
import { FoodListComponent } from './components/foods/list.component';
import { AddFoodComponent } from './components/foods/add.component';
import { routes } from "app/app.router";
import { AddCategoryComponent } from "app/components/categories/add.component";
import { CategoryService } from "app/services/category.service";
import { FoodService } from "app/services/food.service";
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ListCategoriesComponent } from "app/components/categories/list.component";
import { GuidService } from "app/services/guid.service";
import { AddDailyEatingComponent } from './components/dailyeatings/add.component';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { RegisterComponent } from './components/users/register.component';
import { ListUsersComponent } from './components/users/list.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    FoodListComponent,
    AddFoodComponent,
    AddCategoryComponent,
    ListCategoriesComponent,
    AddDailyEatingComponent,
    AddDailyEatingComponent,
    RegisterComponent,
    ListUsersComponent
  ],
  imports: [
    routes,
    BrowserModule,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,
    CommonModule,
    BrowserAnimationsModule,
    ToastModule.forRoot()
  ],
  providers: [FoodService, CategoryService, GuidService],
  bootstrap: [AppComponent]
})
export class AppModule { }
