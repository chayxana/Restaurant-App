import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Category } from 'app/models/category';
import { CategoryService } from 'app/services/category.service';
import { AuthService } from 'app/services/auth.service';

declare var $: any;

@Component({
  selector: 'app-list-categories',
  template: `
  <div class="container">
    <mat-card [ngClass]="{ loading : isLoading }">
      <table mat-table [dataSource]="categories">
        <ng-container matColumnDef="name">
          <th mat-header-cell *matHeaderCellDef> Name </th>
          <td mat-cell *matCellDef="let element"> {{element.name}} </td>
        </ng-container>
        <ng-container matColumnDef="color">
          <th mat-header-cell *matHeaderCellDef> Color </th>
          <td mat-cell *matCellDef="let element"> {{element.color}} </td>
        </ng-container>
        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>
            Actions
          </th>
          <td mat-cell *matCellDef="let row">
            <button mat-icon-button [matMenuTriggerFor]="beforeMenu">
              <mat-icon>more_horiz</mat-icon>
            </button>
            <mat-menu #beforeMenu="matMenu" xPosition="before">
              <a mat-menu-item [routerLink]="['/categories/edit', row.id]" href="#">
                <mat-icon>edit</mat-icon>
                Edit
              </a>
              <button mat-menu-item (click)="onDelete(row.id)">
                <mat-icon>delete</mat-icon>
                Delete
              </button>
            </mat-menu>
          </td>
        </ng-container>

        <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
        <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
      </table>
    </mat-card>
    <div class="ui modal" #modal style="display: none">
      <div class="header">
        Deleting category
      </div>
      <div class="image content">
        <div class="description">
          Do you want to delete "{{selectedCategory?.name}}"?
        </div>
      </div>
      <div>
        <div class="ui primary button" [ngClass]="{ loading : deleting }" (click)="confirmDelete()">Yes</div>
        <div class="ui button" (click)="cancelDelete()">No</div>
      </div>
    </div>

    <button mat-fab class="pos-fix" routerLink="/categories/create">
      <mat-icon>add</mat-icon>
    </button>
  </div>
  `,
  styles : [
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
    },
    `
  ]
})

export class ListCategoriesComponent implements OnInit {

  @ViewChild('modal') deleteModal: ElementRef;
  isLoading: boolean;
  categories: Category[];
  selectedCategory: Category;
  deleting: boolean;

  displayedColumns: string[] = ['name', 'color', 'actions'];

  constructor(private categoryService: CategoryService,
    private authService: AuthService) { }

  ngOnInit() {
    this.categoryService.getAll(this.authService.authorizationHeaderValue).subscribe(cat => {
      console.log(cat);
      this.categories = cat;
    });
  }

  confirmDelete() {
    this.deleting = true;
    this.categoryService.delete(this.selectedCategory, this.authService.authorizationHeaderValue).subscribe(x => {
      this.categories = this.categories.filter(food => food.id !== this.selectedCategory.id);
      this.deleting = false;
      $(this.deleteModal.nativeElement).modal('hide');
    });
  }

  cancelDelete() {
    $(this.deleteModal.nativeElement).modal('hide');
  }

  onDelete(id: string) {
    this.categoryService.get(id, this.authService.authorizationHeaderValue).subscribe(cat => {
      this.selectedCategory = cat;
      $(this.deleteModal.nativeElement).modal('show');
    });
  }
}
