import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'app/services/category.service';
import { FoodService } from 'app/services/food.service';
import { Category } from 'app/models/category';
import { Food } from 'app/models/food';
import { NgForm } from '@angular/forms';
import * as uuid from 'uuid';
import { MatProgressButtonOptions } from 'mat-progress-buttons';
import { AuthService } from 'app/services/auth.service';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-add-food',
  template: `
  <mat-card class="container">
    <form #foodForm class='form-container' (ngSubmit)="onSubmit(foodForm)">
      <mat-form-field>
        <input matInput placeholder="Food name" [(ngModel)]="food.name" name="name">
      </mat-form-field>
      <mat-form-field>
        <input matInput placeholder="Food description" [(ngModel)]="food.description" name="description">
      </mat-form-field>
      <mat-form-field>
        <input type="number" matInput placeholder="Price" [(ngModel)]="food.price" name="price">
      </mat-form-field>

      <mat-form-field>
        <mat-select placeholder="Categories" [(ngModel)]="food.categoryId" name="categoryId">
          <mat-option *ngFor="let category of categories" [value]="category.id">
            {{category.name}}
          </mat-option>
        </mat-select>
      </mat-form-field>

      <div style="margin:15px 0">
        <input (change)="imageUpload($event)" #file type="file" id="file">
      </div>

      <mat-card *ngIf="imageUrl" >
        <img [src]="imageUrl" class="food-image"/>
      </mat-card>
      <mat-card-actions>
        <mat-spinner-button [options]="saveButtonsOpts" type="submit">Save</mat-spinner-button>
        <button mat-button type="button" (click)="onCancel()">Cancel</button>
      </mat-card-actions>
    </form>
  </mat-card>`,
  styles: [
    `
      .container {
        margin: 50px;
      }
      .form-container {
        padding: 20px;
        display: flex;
        flex-direction: column;
      },
      .food-image {
        width: "40px";
      }
    `
  ]
})
export class AddFoodComponent implements OnInit {

  saveButtonsOpts: MatProgressButtonOptions = {
    active: false,
    text: 'Save',
    spinnerSize: 19,
    raised: false,
    stroked: true,
    flat: false,
    fab: false,
    buttonColor: 'primary',
    spinnerColor: 'accent',
    fullWidth: false,
    disabled: false,
    mode: 'indeterminate',
  };
  food: Food = {
    id: null,
    name: '',
    description: '',
    price: null,
    category: null,
    categoryId: null,
    pictures: null
  };

  imageUrls: string[];
  file: File;
  categories: Category[];
  isSaving: boolean;
  isEditMode: boolean;
  imageUpdated: boolean;

  constructor(
    private categoryService: CategoryService,
    private foodService: FoodService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.route.queryParams.subscribe(async params => {
      const id = params['id'];
      if (id) {
        const food = await this.foodService.get(id, this.authService.authorizationHeaderValue).toPromise();
        this.food = food;
        this.imageUrls = this.food.pictures;
      }
    });
    this.categoryService.getAll(this.authService.authorizationHeaderValue).subscribe(cat => {
      this.categories = cat;
    });
  }

  imageUpload(e) {
    const reader = new FileReader();
    this.file = e.target.files[0];
    reader.onloadend = () => {
      if (this.isEditMode) {
        this.imageUpdated = true;
      }

      this.imageUrls = reader.result;
    };
    reader.readAsDataURL(this.file);
  }

  async onSubmit(form: NgForm, file: HTMLInputElement) {
    this.isSaving = true;
    if (this.isEditMode) {
      if (this.imageUpdated) {
        await this.uploadPicture();
      }
      await this.foodService.update(this.food, this.authService.authorizationHeaderValue).toPromise();
      this.showMessage('Food updated successfully!');

    } else {
      this.food.id = uuid();

      await this.createFood();
      await this.uploadPicture();

      this.showMessage('Food created successfully!');
      this.reset(form, file);
    }
  }

  private showMessage(message: string) {
    this.snackBar.open(message, null, {
      duration: 2000
    });
  }

  private uploadPicture(): Promise<any> {
    const files = new Set<File>();
    files.add(this.file);
    return this.foodService.uploadImage(files, this.food.id, this.authService.authorizationHeaderValue).toPromise();
  }

  private createFood(): Promise<any> {
    return this.foodService.create(this.food, this.authService.authorizationHeaderValue).toPromise();
  }

  onCancel() {
    this.router.navigate(['/foods']);
  }

  reset(form: NgForm, file: HTMLInputElement) {
    form.reset();
    file.value = '';
    this.imageUrls = null;
    this.isSaving = false;
  }
}
