import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from "app/services/category.service";
import { FoodService } from "app/services/food.service";
import { Category } from "app/models/category";
import { Food } from "app/models/food";
import { Http } from "@angular/http";
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-food',
  template: `
  <div class="ui raised very padded text container segment">
    <div class="ui small message" *ngIf="isSuccess" [ngClass]="{ success : isSuccess }">
        <div class="content">
          <p>{{statusMessage}}</p>
        </div>
    </div>

    <form class="ui form" #foodForm="ngForm" (ngSubmit)="onSubmit(foodForm, file)">
      <div class="field" [ngClass]="{error : name.invalid && (name.dirty || name.touched) }">
        <label>Name</label>
        <input type="text" required [(ngModel)]="food.name" name="name" #name="ngModel" placeholder="Name">
      </div>
      <div class="field">
        <label>Description</label>
        <input type="text" [(ngModel)]="food.description" name="description" #description="ngModel" placeholder="Description">
      </div>
      <div class="two fields">
        <div class="field" [ngClass]="{error : price.invalid && (price.dirty || price.touched) }">
          <label>Price</label>
          <input type="text" required name="price" [(ngModel)]="food.price" #price="ngModel" placeholder="Price">
        </div>
        <div class="field" [ngClass]="{error : category.invalid && (category.dirty || category.touched) }">
          <label>Category</label>
          <select [(ngModel)]="food.categoryId" placeholder="categories" name="category" class="ui fluid dropdown" required #category="ngModel">
              <option *ngFor="let category of categories" [ngValue]="category.id">{{category.name}}</option>
          </select>
        </div>
      </div>
      <div class="field">
        <label>Picture</label>
        <input type="file" (change)="imageUpload($event)" #file>
        <img [src]="imageUrl" width="300" *ngIf="imageUrl" height="300" />
      </div>
      <button class="ui button blue" [ngClass]="{ loading : isLoading }" [disabled]="foodForm.invalid" type="submit">Save</button>
      <button class="ui button" type="submit">Cancel</button>
    </form>
  </div>`
})
export class AddFoodComponent implements OnInit {

  food: Food = {
    id: null,
    name: '',
    description: '',
    price: null,
    category: null,
    categoryId: null
  };

  imageUrl: any;
  file: File;
  categories: Category[];
  isLoading: boolean;

  constructor(
    private categoryService: CategoryService,
    private foodService: FoodService,
    private route: ActivatedRoute,
    private router: Router,
    private http: Http) {

  }

  ngOnInit() {
    this.categoryService.getAll().subscribe(cat => {
      this.categories = cat;
    })
  }

  imageUpload(e) {
    let reader = new FileReader();
    this.file = e.target.files[0];
    reader.onloadend = () => {
      this.imageUrl = reader.result;
    }
    reader.readAsDataURL(this.file);
  }

  onSubmit(form: NgForm, file: HTMLInputElement) {
    this.isLoading = true;
    this.foodService.uploadImage(this.file).then(uploaded => {
      if (uploaded) {
        this.foodService.create(this.food).subscribe(x => {
          this.isLoading = false;
          form.reset();
          file.value = "";
          this.imageUrl = null;
        });
      }
    });
  }
}
