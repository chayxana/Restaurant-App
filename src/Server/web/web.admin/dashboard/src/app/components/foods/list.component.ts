import { Component, OnInit,  ViewEncapsulation } from '@angular/core';
import { FoodService } from 'app/services/food.service';
import { Food } from 'app/models/food';
import { AuthService } from 'app/services/auth.service';

@Component({
  selector: 'app-foods-list',
  template: `
  <div class="container">
    <mat-card>
      <table mat-table [dataSource]="foods">
        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef> Name </th>
          <td mat-cell *matCellDef="let element"> {{element.name}} </td>
        </ng-container>
        <ng-container matColumnDef="description">
          <th mat-header-cell *matHeaderCellDef> Description </th>
          <td mat-cell *matCellDef="let row"> {{row.description}} </td>
        </ng-container>
        <ng-container matColumnDef="category">
          <th mat-header-cell *matHeaderCellDef> Category </th>
          <td mat-cell *matCellDef="let row"> {{row.category?.name}} </td>
        </ng-container>
        <ng-container matColumnDef="price">
          <th mat-header-cell *matHeaderCellDef> Price </th>
          <td mat-cell *matCellDef="let row"> {{row.price}} </td>
        </ng-container>
        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>
            Edit / Delete
          </th>
          <td mat-cell *matCellDef="let row">
            <a mat-icon-button [routerLink]="['/foods/create']" href="#" [queryParams]="{ id : row.id}">
              <mat-icon>edit</mat-icon>
            </a>
            <button mat-icon-button (click)="onDelete(row.id)">
              <mat-icon>delete</mat-icon>
            </button>
          </td>
        </ng-container>
        <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
        <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
       </table>
    </mat-card>
    <button mat-fab class="pos-fix" routerLink="/foods/create">
      <mat-icon>add</mat-icon>
    </button>
  </div>
  `,
  styles: [
    `table {
      width: 100%;
    }

    .container {
      position: relative;
      padding: 20px;
    }

    .pos-fix {
      position: fixed !important;
      bottom: 20px;
      right: 20px;
      top: auto;
      left: auto;
    }`
  ]
})
export class FoodListComponent implements OnInit {

  foods: Food[];
  selectedFood: Food;
  isLoading: boolean;
  deleting: boolean;
  displayedColumns: string[] = ['name', 'description', 'category', 'price', 'actions'];
  constructor(private foodService: FoodService, private authService: AuthService) { }

  ngOnInit() {
    this.isLoading = true;
    this.foodService.getAll(this.authService.authorizationHeaderValue).subscribe(foods => {
      console.table(foods);
      foods.forEach(f => {
        f.pictures = f.pictures;
      });
      this.foods = foods;
      this.isLoading = false;
    });
  }

  onDelete(id: string) {
    this.foodService.get(id, this.authService.authorizationHeaderValue).subscribe(food => {
      this.selectedFood = food;
    });
  }

  confirmDelete() {
    this.deleting = true;
    this.foodService.delete(this.selectedFood, this.authService.authorizationHeaderValue).subscribe(x => {
      this.foods = this.foods.filter(food => food.id !== this.selectedFood.id);
      this.deleting = false;
    });
  }

  cancelDelete() {

  }
}
