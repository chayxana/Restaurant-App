import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'app/services/category.service';
import { FoodService } from 'app/services/food.service';
import { Category } from 'app/models/category';
import { Food } from 'app/models/food';
import { NgForm } from '@angular/forms';
import * as uuid from 'uuid';
import { MatProgressButtonOptions } from 'mat-progress-buttons';

@Component({
  selector: 'app-add-food',
  template: `
  <mat-card class="container">
    <form #foodForm class='form-container'>
      <mat-form-field>
        <input matInput placeholder="Food name">
      </mat-form-field>
      <mat-form-field>
        <input matInput placeholder="Food description">
      </mat-form-field>
      <mat-form-field>
        <input type="number" matInput placeholder="Price">
      </mat-form-field>

      <mat-form-field>
        <mat-select placeholder="Categories">
          <mat-option *ngFor="let category of categories" [value]="category.id">
            {{category.name}}
          </mat-option>
        </mat-select>
      </mat-form-field>

      <div style="margin:15px 0">
        <input (change)="imageUpload($event)" #file type="file" id="file">
      </div>

      <mat-card *ngIf="imageUrl" >
        <img [src]="imageUrl" mat-card-image/>
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
    picture: null
  };

  imageUrl: any;
  file: File;
  categories: Category[];
  isSaving: boolean;
  isEditMode: boolean;
  isLoading: boolean;
  imageUpdated: boolean;

  constructor(
    private categoryService: CategoryService,
    private foodService: FoodService,
    private route: ActivatedRoute,
    private router: Router,
    vcr: ViewContainerRef
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.isLoading = true;
        this.foodService.get(id).subscribe(food => {
          this.food = food;
          this.imageUrl = this.food.picture;
          this.isEditMode = true;
          this.isLoading = false;
        });
      }
    });

    this.categoryService.getAll().subscribe(cat => {
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

      this.imageUrl = reader.result;
    };
    reader.readAsDataURL(this.file);
  }

  onSubmit(form: NgForm, file: HTMLInputElement) {
    this.isSaving = true;
    if (this.isEditMode) {
      if (this.imageUpdated) {
        this.uploadPicture().then(uploaded => {
          this.foodService.update(this.food).subscribe(x => {
            this.isSaving = false;
            // this.toastr.success('Food updated successfully!', 'Updated!', { toastLife: 2000 });
          });
        });
      } else {
        this.foodService.update(this.food).subscribe(x => {
          this.isSaving = false;
          // this.toastr.success('Food updated successfully!', 'Updated!', { toastLife: 2000 });
        });
      }
    } else {
      this.food.id = uuid();
      this.uploadPicture().then(uploaded => {
        if (uploaded) {
          this.createFood().subscribe(x => {
            this.reset(form, file);
          });
        } else {
          this.isSaving = false;
        }
        // this.toastr.success('Food created successfully!', 'Created!', { toastLife: 2000 });
      });
    }
  }

  uploadPicture() {
    return this.foodService.uploadImage(this.file, this.food.id);
  }

  createFood() {
    return this.foodService.create(this.food);
  }

  onCancel() {
    this.router.navigate(['/foods']);
  }

  reset(form: NgForm, file: HTMLInputElement) {
    form.reset();
    file.value = '';
    this.imageUrl = null;
    this.isSaving = false;
  }
}
