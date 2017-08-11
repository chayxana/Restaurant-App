import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { FoodService } from "app/services/food.service";
import { Food } from "app/models/food";
import { ContentUrl } from "app/shared/constants";

declare var $: any;


@Component({
  selector: 'app-foods-list',
  template: `
  <div class="ui container basic segment" [ngClass]="{ loading : isLoading }">
    <table class="ui selectable celled table">
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
        <tr *ngFor="let food of foods ;let i = index">
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
           <div class="ui small basic icon buttons">
            <a [routerLink]="['/foods/create']" [queryParams]="{ id : food.id}" class="ui button"><i class="edit icon"></i></a>
            <button class="ui button"><i class="remove icon" (click)="onDelete(food.id)"></i></button>
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
      <i class="close icon"></i>
      <div class="header">
        Delete category
      </div>
      <div class="image content">
        <div class="description">
          Do you want to delete?
        </div>
      </div>
      <div class="actions">
        <div class="ui button" (click)="confirmDelete()">Yes</div>
        <div class="ui button" (click)="cancelDelete()">No</div>
      </div>
    </div>
  `
})
export class FoodListComponent implements OnInit, AfterViewInit {

  @ViewChild('modal') deleteModal: ElementRef;

  contentUrl = ContentUrl;
  foods: Food[]
  isLoading: boolean;

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
    $(this.deleteModal.nativeElement).modal('show');
  }

  confirmDelete() {
    console.log("Confirm");
    $(this.deleteModal.nativeElement).modal('hide');
  }

  cancelDelete() {
    $(this.deleteModal.nativeElement).modal('hide');
  }
}
