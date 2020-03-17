import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { AuthService } from 'app/services/auth.service';
import { MatCardModule, MatButtonModule } from '@angular/material';
import { StorageService } from 'app/services/storage.service';
import { MatProgressButtonsModule } from 'mat-progress-buttons';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    MatCardModule,
    MatButtonModule,
    MatProgressButtonsModule.forRoot(),
  ],
  providers: [AuthService, StorageService]
})
export class AccountModule { }
