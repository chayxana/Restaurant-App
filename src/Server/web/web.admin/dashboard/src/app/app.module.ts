import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AddCategoryComponent } from 'app/components/categories/add.component';
import { AddFoodComponent } from 'app/components/foods/add.component';
import { ListCategoriesComponent } from 'app/components/categories/list.component';
import { FoodListComponent } from 'app/components/foods/list.component';
import { ListUsersComponent } from 'app/components/users/list.component';
import { CreateUserComponent } from 'app/components/users/create.component';
import { CategoryService } from 'app/services/category.service';
import { FoodService } from 'app/services/food.service';
import { routes } from 'app/app.router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { CommonModule } from '@angular/common';
import {
  MatInputModule,
  MatFormFieldModule,
  MatCardModule,
  MatSidenavModule,
  MatListModule,
  MatSelectModule,
  MatButtonModule,
  MatToolbarModule, MatIconModule
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LayoutModule } from '@angular/cdk/layout';

@NgModule({
  declarations: [
    AppComponent,
    AddCategoryComponent,
    ListCategoriesComponent,
    AddFoodComponent,
    FoodListComponent,
    ListUsersComponent,
    CreateUserComponent,
    NavMenuComponent
  ],
  imports: [
    routes,
    FormsModule,
    HttpModule,
    ReactiveFormsModule,
    CommonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSidenavModule,
    MatListModule,
    MatSelectModule,
    MatButtonModule, MatIconModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    LayoutModule
  ],
  providers: [FoodService, CategoryService],
  bootstrap: [AppComponent]
})
export class AppModule {}
