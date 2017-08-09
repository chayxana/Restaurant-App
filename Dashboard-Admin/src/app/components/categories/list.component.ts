import { Component, OnInit } from '@angular/core';
import { Category } from "app/models/category";
import { CategoryService } from "app/services/category.service";

@Component({
  selector: 'app-list-categories',
  template: `
  <div class="ui container basic segment" [ngClass]="{ loading : isLoading }">
    <table class="ui blue selectable celled table ">
      <thead class="full-width">
        <tr>
          <th>#</th>
          <th>Name</th>
          <th>Color</th>
          <th class="right aligned">Edit / Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr *ngFor="let category of categories ;let i = index">
          <td class="collapsing">{{i}}. </td>

          <td>{{category.name}}</td>
          
          <td><h4 class="ui header" [ngStyle]="{ color : category.color }">{{category.color}}</h4></td>

          <td class="right aligned collapsing">
            <a [routerLink]="['/categories/create']" [queryParams]="{ id : category.id}">Edit</a> /
            <a href="#">Delete</a>
          </td>
        </tr>
      </tbody>
      <tfoot class="full-width">
        <tr>
          <th colspan="4">
            <a routerLink="/categories/create" class="ui right floated small primary labeled icon button">
              <i class="plus icon"></i> Create category
          </a>
          </th>
        </tr>
      </tfoot>
    </table>
  </div>`
})

export class ListCategoriesComponent implements OnInit {
  isLoading: boolean;
  categories: Category[];

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getAll().subscribe(cat => {
      this.categories = cat;
    });
  }
}
