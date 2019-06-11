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
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import {
  MatInputModule,
  MatFormFieldModule,
  MatCardModule,
  MatSidenavModule,
  MatListModule,
  MatSelectModule,
  MatButtonModule,
  MatToolbarModule, MatIconModule, MatTableModule
} from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatProgressButtonsModule } from 'mat-progress-buttons';


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
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule,
    MatTableModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSidenavModule,
    MatListModule,
    MatSelectModule,
    MatButtonModule, MatIconModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatProgressButtonsModule.forRoot(),
  ],
  providers: [FoodService, CategoryService],
  bootstrap: [AppComponent]
})
export class AppModule {}
