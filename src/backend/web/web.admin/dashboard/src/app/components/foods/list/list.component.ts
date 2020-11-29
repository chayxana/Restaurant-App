import { Component, OnInit } from '@angular/core';
import { FoodService } from 'app/services/food.service';
import { Food } from 'app/models/food';
import { AuthService } from 'app/services/auth.service';
import { Observable } from 'rxjs';
import { filter, switchMap } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'app/components/confirmation-dialog/confirmation-dialog.component';
import { DialogData } from 'app/models/dialog-data';

@Component({
  selector: 'app-foods-list',
  templateUrl : './list.component.html',
  styleUrls: ['./list.component.css']
})
export class FoodListComponent implements OnInit {
  foods$: Observable<Food[]>;
  selectedFood: Food;
  isLoading: boolean;
  deleting: boolean;
  displayedColumns: string[] = ['name', 'description', 'category', 'price', 'actions'];
  constructor(private foodService: FoodService, private authService: AuthService, public dialog: MatDialog) { }

  ngOnInit() {
    this.foods$ = this.foodService.getAll(this.authService.authHeader);
  }

  async onDelete(id: string) {
    this.selectedFood = await this.foodService.get(id, this.authService.authHeader).toPromise();
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: <DialogData>{
        title: 'Delete food?',
        description: `Do you want to delete ${this.selectedFood.name} ?`,
        positiveButtonText: 'Yes',
        negativeButtonText: 'No'
      }
    });

    dialogRef.afterClosed().
      pipe(
        filter(x => x === true),
        switchMap(_ => this.foodService.delete(this.selectedFood, this.authService.authHeader))
      ).subscribe(() => {
        this.foods$ = this.foodService.getAll(this.authService.authHeader);
      });
  }
}
