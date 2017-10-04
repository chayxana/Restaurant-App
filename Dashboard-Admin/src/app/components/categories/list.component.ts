import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Category } from "app/models/category";
import { CategoryService } from "app/services/category.service";

declare var $: any;

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
          <div class="ui basic small icon buttons">
            <a class="ui button" [routerLink]="['/categories/create']" [queryParams]="{ id : category.id}">
              <i class="edit icon"></i>
            </a>
            <button class="ui button" (click)="onDelete(category.id)">
              <i class="remove icon"></i>
            </button>
          </div>
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
  </div>
    <div class="ui modal" #modal>
      <div class="header">
        Deleting category
      </div>
      <div class="image content">
        <div class="description">
          Do you want to delete "{{selectedCategory?.name}}"?
        </div>
      </div>
      <div class="actions">
        <div class="ui primary button" [ngClass]="{ loading : deleting }" (click)="confirmDelete()">Yes</div>
        <div class="ui button" (click)="cancelDelete()">No</div>
      </div>
    </div>
  `
})

export class ListCategoriesComponent implements OnInit {

  @ViewChild('modal') deleteModal: ElementRef;
  isLoading: boolean;
  categories: Category[];
  selectedCategory: Category;
  deleting: boolean;

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getAll().subscribe(cat => {
      this.categories = cat;
    });
  }

  confirmDelete() {
    this.deleting = true;
    this.categoryService.delete(this.selectedCategory).subscribe(x => {
      this.categories = this.categories.filter(food => food.id !== this.selectedCategory.id);
      this.deleting = false;
      $(this.deleteModal.nativeElement).modal('hide');
    });
  }

  cancelDelete() {
    $(this.deleteModal.nativeElement).modal('hide');
  }

  onDelete(id: string) {
    this.categoryService.get(id).subscribe(cat => {
      this.selectedCategory = cat;
      $(this.deleteModal.nativeElement).modal('show');
    })
  }
}
