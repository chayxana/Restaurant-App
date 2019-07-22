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
import { Observable, from } from 'rxjs';
import { switchMap, filter, map, tap } from 'rxjs/operators';
import { FoodPicture } from 'app/models/foodPicture';
import { environment } from 'environments/environment';
import { getFoodInstance, getProgressButtonOptions } from 'app/models/instances';

@Component({
  selector: 'app-add-food',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddFoodComponent implements OnInit {

  saveButtonsOpts: MatProgressButtonOptions = getProgressButtonOptions('Save');
  food: Food = getFoodInstance();
  allPictures: FoodPicture[] = [];
  newImages: File[] = [];
  categories$: Observable<Category[]>;
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
    this.route.paramMap.pipe(
      filter(param => param.get('id') != null),
      tap(_ => this.isEditMode = true),
      map(param => param.get('id')),
      switchMap((id: string) => this.foodService.get(id, this.authService.authorizationHeaderValue)
      )).pipe(
        tap(food => {
          food.pictures = food.pictures.map(x => <FoodPicture>{
            id: x.id,
            filePath: environment.apiUrl + x.filePath
          });
        })
      ).subscribe(food => {
        this.food = food;
        this.food.deletedPictures = [];
        this.allPictures = this.food.pictures.slice();
      });
    this.categories$ = this.categoryService.getAll(this.authService.authorizationHeaderValue);
  }

  imageUpload(e) {
    for (let i = 0; i < e.target.files.length; i++) {
      this.newImages.push(e.target.files[i]);
    }
    const reader = new FileReader();
    reader.onloadend = () => {
      if (this.isEditMode) {
        this.imageUpdated = true;
      }
      this.allPictures.push(<FoodPicture>{ filePath: reader.result as string, id: uuid.v4() });
    };
    this.newImages.forEach(f => reader.readAsDataURL(f));
  }

  deletePicture(picture: FoodPicture) {
    const index = this.allPictures.map(x => x.id).indexOf(picture.id);
    const originalIndex = this.food.pictures.map(x => x.id).indexOf(picture.id);

    if (originalIndex >= 0) {
      this.food.deletedPictures.push(picture);
    }
    this.allPictures.splice(index, 1);
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
      await this.foodService.create(this.food, this.authService.authorizationHeaderValue).toPromise();
      if (this.newImages.length > 0) {
        await this.uploadPicture();
      }

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
    return this.foodService.uploadImage(this.newImages, this.food.id, this.authService.authorizationHeaderValue).toPromise();
  }

  onCancel() {
    this.router.navigate(['/foods']);
  }

  reset(form: NgForm, file: HTMLInputElement) {
    form.reset();
    file.value = '';
    this.allPictures = null;
    this.isSaving = false;
  }
}
