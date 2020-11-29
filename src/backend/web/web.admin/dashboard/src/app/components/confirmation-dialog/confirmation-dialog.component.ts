import { Component, OnInit, Inject } from '@angular/core';
import { DialogData } from '../../models/dialog-data';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  template: `
  <h1 mat-dialog-title>{{data.title}}</h1>
  <div mat-dialog-content>
    <p>{{data.description}}</p>
  </div>
  <div mat-dialog-actions>
    <button mat-raised-button color="warn" [mat-dialog-close]="true">{{data.positiveButtonText}}</button>
    <button mat-raised-button (click)="onNoClick()" cdkFocusInitial>{{data.negativeButtonText}}</button>
  </div>
  `
})
export class ConfirmationDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
