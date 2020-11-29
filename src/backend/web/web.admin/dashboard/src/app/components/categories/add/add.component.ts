import { Component, OnInit } from '@angular/core';
import { Category } from 'app/models/category';
import { CategoryService } from 'app/services/category.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { FormControl, FormGroup, Validators, NgForm } from '@angular/forms';
import * as uuid from 'uuid';
import { MatProgressButtonOptions } from 'mat-progress-buttons';
import { AuthService } from 'app/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add-categories',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
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
    id: uuid.v4(),
    color: '',
    name: ''
  };

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.route.paramMap.subscribe(route => {
      const id = route.get('id');
      if (id) {
        this.isEditMode = true;
        this.isLoading = true;
        this.categoryService.get(id, this.authService.authHeader).subscribe(cat => {
          this.category = cat;
          this.isLoading = false;
        });
      }
    });
  }

  async onSubmit(form: NgForm) {
    this.saveButtonsOpts.active = true;
    if (this.isEditMode) {
      await this.categoryService.update(this.category, this.authService.authHeader).toPromise();
      this.showMessage('Category updated successfully!');
    } else {
      await this.categoryService.create(this.category, this.authService.authHeader).toPromise();
      this.showMessage('Category created successfully!');
      form.reset();
    }
    this.saveButtonsOpts.active = false;
  }

  onCancel() {
    this.router.navigate(['/categories']);
  }

  private showMessage(message: string) {
    this.snackBar.open(message, null, {
      duration: 2000
    });
  }
}
