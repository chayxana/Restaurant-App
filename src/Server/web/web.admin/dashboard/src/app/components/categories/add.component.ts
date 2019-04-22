import { Component, OnInit } from '@angular/core';
import { Category } from 'app/models/category';
import { CategoryService } from 'app/services/category.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators, NgForm } from '@angular/forms';
import * as uuid from 'uuid';

@Component({
  selector: 'app-add-categories',
  template: `
  <mat-card class="container">
    <form class="form-container" #categoryForm="ngForm" (ngSubmit)="onSubmit(categoryForm)">
      <mat-form-field [ngClass]="{error : name.invalid && (name.dirty || name.touched) }">
        <input matInput type="text" required placeholder="Name" [(ngModel)]="category.name" name="name" #name="ngModel">
      </mat-form-field>
      <div [ngClass]="{error : color.invalid && (color.dirty || color.touched)}">
        <label>Color </label>
        <input type="color" required [(ngModel)]="category.color" name="color" #color="ngModel">
      </div>
      <button mat-button [disabled]="categoryForm.invalid" type="submit" [ngClass]="{ loading : saving }">Save</button>
      <button mat-button type="button" (click)="onCancel()">Cancel</button>
    </form>
  </mat-card>`,
  styles: [
    ` .container {
        margin: 50px;
      }
      .form-container {
        padding: 20px;
        display: flex;
        flex-direction: column;
      }
    `
  ]
})

export class AddCategoryComponent implements OnInit {
  saving = false;
  isEditMode: boolean;
  isLoading: boolean;

  category: Category = {
    id: '',
    color: '',
    name: ''
  };

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(route => {
      const id = route['id'];
      if (id) {
        this.isEditMode = true;
        this.isLoading = true;
        this.categoryService.get(id).subscribe(cat => {
          this.category = cat;
          this.isLoading = false;
        });
      }
    });
  }

  onSubmit(form: NgForm) {
    this.saving = true;
    if (this.isEditMode) {
      this.categoryService.update(this.category).subscribe(x => {
        this.saving = false;
      });
    } else {
      this.category.id = uuid();
      this.categoryService.create(this.category).subscribe(x => {
        form.reset();
        this.saving = false;
      });
    }
  }

  onCancel() {
    this.router.navigate(['/categories']);
  }
}
