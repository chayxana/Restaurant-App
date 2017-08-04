import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from "app/services/category.service";
import { FoodService } from "app/services/food.service";
import { Category } from "app/models/category";
import { Food } from "app/models/food";

@Component({
  selector: 'app-add-food',
  template: `
  <div class="ui raised very padded text container segment">
    <form class="ui form" #ngForm>
      <div class="field">
        <label>Name</label>
        <input type="text" [(ngModel)]="food.name" name="name" placeholder="Name">
      </div>
      <div class="field">
        <label>Description</label>
        <input type="text" [(ngModel)]="food.description" name="description" placeholder="Description">
      </div>
      <div class="two fields">
        <div class="field">
          <label>Price</label>
          <input type="text" name="price" [(ngModel)]="food.price" placeholder="Price">
        </div>
        <div class="field">
          <label>Category</label>
          <select [(ngModel)]="food.category" class="ui fluid dropdown">
              <option *ngFor="let category of categories" [ngValue]="category">{{category.name}}</option>
          </select>
        </div>
      </div>
      <div class="field">
        <label>Picture</label>
        <input type="file">
      </div>
      <button class="ui button blue" type="submit">Save</button>
      <button class="ui button" type="submit">Cancel</button>
    </form>
  </div>`
})
export class AddFoodComponent implements OnInit {

  categories: Category[];
  food: Food = {
    id: '',
    name: '',
    description: '',
    price: 0,
    category: null
  }

  constructor(
    private categoryService: CategoryService,
    private foodService: FoodService,
    private route: ActivatedRoute,
    private router: Router) {

  }

  ngOnInit() {
  }
}
