import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatMenuModule} from '@angular/material/menu';
import { LayoutModule } from '@angular/cdk/layout';
import { MatProgressButtonsModule } from 'mat-progress-buttons';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ColorPickerModule } from 'ngx-color-picker';

import {
  MatInputModule,
  MatFormFieldModule,
  MatCardModule,
  MatSidenavModule,
  MatListModule,
  MatSelectModule,
  MatButtonModule,
  MatToolbarModule, MatIconModule, MatTableModule, MatDialogModule
} from '@angular/material';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule,
    MatTableModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSidenavModule,
    MatListModule,
    MatSelectModule,
    MatButtonModule, MatIconModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatProgressButtonsModule.forRoot(),
    ColorPickerModule,
    MatSnackBarModule,
    MatMenuModule,
    MatDialogModule,
  ],
  exports: [
    ColorPickerModule,
    CommonModule,
    FormsModule,
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    CommonModule,
    MatTableModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSidenavModule,
    MatListModule,
    MatSelectModule,
    MatButtonModule, MatIconModule,
    MatToolbarModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatProgressButtonsModule,
    ColorPickerModule,
    MatSnackBarModule,
    MatMenuModule,
    MatDialogModule,
  ],
})
export class SharedModule { }
