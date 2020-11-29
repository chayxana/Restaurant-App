import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Category } from 'app/models/category';
import { CategoryService } from 'app/services/category.service';
import { AuthService } from 'app/services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'app/components/confirmation-dialog/confirmation-dialog.component';
import { DialogData } from 'app/models/dialog-data';
import { filter, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

declare var $: any;

@Component({
  selector: 'app-list-categories',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})

export class ListCategoriesComponent implements OnInit {

  isLoading: boolean;
  categories$: Observable<Category[]>;
  selectedCategory: Category;
  deleting: boolean;

  displayedColumns: string[] = ['name', 'color', 'actions'];

  constructor(private categoryService: CategoryService,
    private authService: AuthService,
    public dialog: MatDialog) { }

  ngOnInit() {
    this.categories$ = this.categoryService.getAll(this.authService.authHeader);
  }


  async onDelete(id: string) {
    this.selectedCategory = await this.categoryService.get(id, this.authService.authHeader).toPromise();
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: <DialogData>{
        title: 'Delete category?',
        description: `Do you want to delete ${this.selectedCategory.name}, all foods in this category will be terminated!`,
        positiveButtonText: 'Yes',
        negativeButtonText: 'No'
      }
    });

    dialogRef.afterClosed().
      pipe(
        filter(x => x === true),
        switchMap(_ => this.categoryService.delete(this.selectedCategory, this.authService.authHeader))
      ).subscribe(() => {
        this.categories$ = this.categoryService.getAll(this.authService.authHeader);
      });
  }
}
