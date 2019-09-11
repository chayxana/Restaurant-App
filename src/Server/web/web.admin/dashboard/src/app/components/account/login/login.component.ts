import { Component, OnInit } from '@angular/core';
import { AuthService } from 'app/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, tap, map } from 'rxjs/operators';
import { StorageService } from 'app/services/storage.service';
import { REDIRECT_URL_KEY } from 'app/config/constants';
import { getProgressButtonOptions } from 'app/models/instances';
import { MatProgressButtonOptions } from 'mat-progress-buttons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['login.component.css']
})
export class LoginComponent implements OnInit {
  loginButtonsOpts: MatProgressButtonOptions = getProgressButtonOptions('Login with IdentityServer');
  title = 'Login';
  redirectUrl = '';
  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private localStorageService: StorageService) { }

  async ngOnInit() {
    this.route.queryParams.pipe(
      filter(param => param['redirect'] != null),
      map(param => param['redirect']),
      tap(redirect => this.redirectUrl = redirect)
      ).subscribe(redirect => {
        this.localStorageService.setItem(REDIRECT_URL_KEY, redirect);
        // this.login();
      });
  }


  async login() {
    this.loginButtonsOpts.active = true;
    if (await this.authService.hasSession()) {
      await this.authService.loginSilent();
      if (this.redirectUrl) {
        this.router.navigate([this.redirectUrl]);
      } else {
        this.router.navigate(['/']);
      }
    } else {
      this.loginButtonsOpts.active = true;
      this.authService.login();
    }
  }
}
