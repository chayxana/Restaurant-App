import { Component, OnInit } from '@angular/core';
import { AuthService } from 'app/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StorageService } from 'app/services/storage.service';
import { REDIRECT_URL_KEY } from 'app/config/constants';

@Component({
  selector: 'app-auth-callback',
  template: `
    <div *ngIf="error" class="row justify-content-center">
      <div class="col-md-8 text-center">
            <div class="alert alert-warning" role="alert">
              Oops, there was an error, please try to <a routerLink="/login">login again</a>.
            </div>
      </div>
    </div>
  `
})
export class AuthCallbackComponent implements OnInit {

  error: boolean;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private localStorageService: StorageService) { }

  async ngOnInit() {

    if (this.route.snapshot.fragment.indexOf('error') >= 0) {
      this.error = true;
      return;
    }
    await this.authService.completeAuthentication();
    const redirectUrl = this.localStorageService.getItem(REDIRECT_URL_KEY);
    if (redirectUrl) {
      this.router.navigate([redirectUrl]);
    } else {
      this.router.navigate(['/']);
    }
  }
}
