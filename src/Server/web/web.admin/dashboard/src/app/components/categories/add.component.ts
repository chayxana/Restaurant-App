import { Component, OnInit } from '@angular/core';
import { Category } from 'app/models/category';
import { CategoryService } from 'app/services/category.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { FormControl, FormGroup, Validators, NgForm } from '@angular/forms';
import * as uuid from 'uuid';
import { MatProgressButtonOptions } from 'mat-progress-buttons';
import { AuthService } from 'app/services/auth.service';

@Component({
  selector: 'app-add-categories',
  template: `
  <mat-card class="container">
    <form class="form-container" #categoryForm="ngForm" (ngSubmit)="onSubmit(categoryForm)">
        <mat-form-field [ngClass]="{error : name.invalid && (name.dirty || name.touched) }">
          <input matInput type="text" required placeholder="Name" [(ngModel)]="category.name" name="name" #name="ngModel">
        </mat-form-field>
        <mat-form-field [ngClass]="{error : color.invalid && (color.dirty || color.touched)}">
          <input matInput required placeholder="Color"
              [(colorPicker)]="category.color"
              cpPosition='bottom'
              [(ngModel)]="category.color" name="color" #color="ngModel"/>
        </mat-form-field >
      <mat-card-actions>
        <mat-spinner-button [options]="saveButtonsOpts" type="submit">Save</mat-spinner-button>
        <button mat-button type="button" (click)="onCancel()">Cancel</button>
      </mat-card-actions>
    </form>
  </mat-card>`,
  styles: [
    ` .container {
        margin: 25px;
      }
      .form-container {
        padding: 20px;
        display: flex;
        flex-direction: column;
      }
      .form-container > * {
        width: 100%;
      }
    `
  ]
})

export class AddCategoryComponent implements OnInit {

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
    private route: ActivatedRoute,
    private authService: AuthService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(route => {
      const id = route['id'];
      if (id) {
        this.isEditMode = true;
        this.isLoading = true;
        this.categoryService.get(id, this.authService.authorizationHeaderValue).subscribe(cat => {
          this.category = cat;
          this.isLoading = false;
        });
      }
    });
  }

  onSubmit(form: NgForm) {
    this.saveButtonsOpts.active = true;
    if (this.isEditMode) {
      this.categoryService.update(this.category, this.authService.authorizationHeaderValue).subscribe(x => {
        this.saveButtonsOpts.active = false;
      });
    } else {
      this.category.id = uuid();
      this.categoryService.create(this.category, this.authService.authorizationHeaderValue).subscribe(x => {
        form.reset();
        this.saveButtonsOpts.active = false;
      });
    }
  }

  onCancel() {
    this.router.navigate(['/categories']);
  }
}
