import { Component, OnInit } from '@angular/core';
import { Category } from "app/models/category";
import { CategoryService } from "app/services/category.service";
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators, NgForm } from '@angular/forms';
import { GuidService } from "app/services/guid.service";

@Component({
  selector: 'app-add-categories',
  template: `
    <div class="ui raised very padded text container segment">
      <form class="ui form" #categoryForm="ngForm" (ngSubmit)="onSubmit(categoryForm)">
        <div class="field" [ngClass]="{error : name.invalid && (name.dirty || name.touched) }">
          <label>Name</label>
          <input type="text" required placeholder="Name" [(ngModel)]="category.name" name="name" #name="ngModel">
        </div>
        <div class="field" [ngClass]="{error : color.invalid && (color.dirty || color.touched)}">
          <label>Color</label>
          <input type="color" required [(ngModel)]="category.color" name="color" #color="ngModel">
        </div>
        <button [disabled]="categoryForm.invalid" class="ui button blue" type="submit" [ngClass]="{ loading : saving }">Save</button>
        <button class="ui button" type="button" (click)="onCancel()">Cancel</button>
      </form>
    </div>`
})

export class AddCategoryComponent implements OnInit {
  saving: boolean = false;
  isEditMode: boolean;
  isLoading: boolean;

  category: Category = {
    id: '',
    color: '',
    name: ''
  }

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private guidService: GuidService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(route => {
      var id = route["id"];
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
    }
    else {
      this.category.id = this.guidService.GetNewGuid();
      this.categoryService.create(this.category).subscribe(x => {
        form.reset();
        this.saving = false;
      });
    }
  }

  onCancel() {
    this.router.navigate(['/categories'])
  }
}
