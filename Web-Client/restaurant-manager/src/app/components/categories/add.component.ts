import { Component, OnInit } from '@angular/core';
import { Category } from "app/models/category";
import { CategoryService } from "app/services/category.service";

@Component({
  selector: 'app-add-categories',
  template: `
    <div class="ui raised very padded text container segment">
      <form class="ui form" #ngForm="ngForm" (ngSubmit)="onSubmit()">
        <div class="field">
          <label>Name</label>
          <input type="text" placeholder="Name" [(ngModel)]="category.name" name="name">
        </div>
        <div class="field">
          <label>Color</label>
          <input type="color" [(ngModel)]="category.color" name="color">
        </div>
        <button class="ui button blue" type="submit">Save</button>
        <button class="ui button" type="submit">Cancel</button>
      </form>

      <div class="ui icon info message">
        <i class="notched circle loading icon"></i>
        <div class="content">
          <div class="header">Just one second </div>
          <p>We're fetching that content for you.</p>
        </div>
      </div>
    </div>`

})
export class AddCategoryComponent implements OnInit {
  saving: boolean;
  category: Category = {
    id: '',
    color: '',
    name: ''
  }
  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
  }

  onSubmit() {
    this.saving = true;
    this.categoryService.create(this.category).subscribe(x => {
      this.saving = false;
    });
  }
}
