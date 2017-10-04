import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { FoodService } from "app/services/food.service";
import { Food } from "app/models/food";
import { ContentUrl } from "app/shared/constants";

declare var $: any;


@Component({
  selector: 'app-foods-list',
  template: `
  <div class="ui container basic segment" [ngClass]="{ loading : isLoading }">
    <table class="ui celled table">
      <thead class="full-width">
        <tr>
          <th>#</th>
          <th>Picture</th>
          <th>Name</th>
          <th>Description</th>
          <th>Category</th>
          <th>Price</th>
          <th class="right aligned">Edit / Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let food of foods;let i = index">
          <td class="collapsing">{{i}}. </td>

          <td class="collapsing">
            <img [src]="food.picture" class="ui mini rounded image">
          </td>

          <td>{{food.name}}</td>

          <td>{{food.description}}</td>
          
          <td>
            <span class="ui green">{{food.category?.name}}</span>
          </td>

          <td class="collapsing">{{food.price}}</td>

          <td class="right aligned collapsing">
           <div class="ui basic small icon buttons">
            <a class="ui button" [routerLink]="['/foods/create']" [queryParams]="{ id : food.id}">
              <i class="edit icon"></i>
            </a>
            <button class="ui button" (click)="onDelete(food.id)">
              <i class="remove icon"></i>
            </button>
          </div>
          </td>
        </tr>
      </tbody>
      <tfoot class="full-width">
        <tr>
          <th colspan="7">
            <a routerLink="/foods/create" class="ui right floated small primary labeled icon button">
              <i class="plus icon"></i> Create food
            </a>
          </th>
        </tr>
      </tfoot>
    </table>
  </div>
   <div class="ui modal" #modal>
      <div class="header">
        Deleting food
      </div>
      <div class="image content">
        <div class="description">
          Do you want to delete "{{selectedFood?.name}}"?
        </div>
      </div>
      <div class="actions">
        <div class="ui primary button" [ngClass]="{ loading : deleting }" (click)="confirmDelete()">Yes</div>
        <div class="ui button" (click)="cancelDelete()">No</div>
      </div>
    </div>
  `
})
export class FoodListComponent implements OnInit, AfterViewInit {

  @ViewChild('modal') deleteModal: ElementRef;

  contentUrl = ContentUrl;
  foods: Food[]
  selectedFood: Food;
  isLoading: boolean;
  deleting: boolean;

  constructor(private foodService: FoodService) { }

  ngAfterViewInit(): void {

  }

  ngOnInit() {
    this.isLoading = true;
    this.foodService.getAll().subscribe(foods => {
      foods.forEach(f => {
        f.picture = ContentUrl + f.picture;
      });
      this.foods = foods;
      this.isLoading = false;
    });
  }

  onDelete(id: string) {
    this.foodService.get(id).subscribe(food => {
      this.selectedFood = food;
    });
    $(this.deleteModal.nativeElement).modal('show');
  }

  confirmDelete() {
    this.deleting = true;
    this.foodService.delete(this.selectedFood).subscribe(x => {
      this.foods = this.foods.filter(food => food.id !== this.selectedFood.id);
      this.deleting = false;
      $(this.deleteModal.nativeElement).modal('hide');
    });
  }

  cancelDelete() {
    $(this.deleteModal.nativeElement).modal('hide');
  }
}
