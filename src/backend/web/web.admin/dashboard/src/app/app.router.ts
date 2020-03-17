import { RouterModule, Routes } from '@angular/router';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';

export const appRoutes: Routes = [
  {
    path: '',
    redirectTo: '/foods', pathMatch: 'full'
  },
  {
    path: 'auth-callback',
    component: AuthCallbackComponent
  },
];

export const routes: any = RouterModule.forRoot(appRoutes);
