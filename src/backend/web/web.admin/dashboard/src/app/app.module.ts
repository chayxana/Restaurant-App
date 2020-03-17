import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { routes } from 'app/app.router';


import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { AuthGuard } from './services/auth.guard';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { CategoriesModule } from './components/categories/categories.module';
import { AccountModule } from './components/account/account.module';
import { FoodsModule } from './components/foods/foods.module';
import { SharedModule } from './shared.module';
import { StorageService } from './services/storage.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AuthCallbackComponent,
    ConfirmationDialogComponent
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ],
  imports: [
    routes,
    SharedModule,
    CategoriesModule,
    AccountModule,
    FoodsModule
  ],
  providers: [AuthGuard, StorageService],
  bootstrap: [AppComponent]
})
export class AppModule {}
