import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { MenuComponent } from './components/menu/menu.component';
import { FoodListComponent } from './components/foods/list.component';
import { AddFoodComponent } from './components/foods/add.component';
import { routes } from "app/app.router";

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    FoodListComponent,
    AddFoodComponent
  ],
  imports: [
    routes,
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
