import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private manager = new UserManager(getClientSettings());
  private user: User | null;
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);

  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  login() {
    return this.manager.signinRedirect();
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this._authNavStatusSource.next(this.isAuthenticated());
  }

  signOut() {
    this.manager.signoutRedirect();
  }

  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  get authorizationHeaderValue(): string {
    console.log(this.user.access_token);
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  get name(): string {
    return this.user != null ? this.user.profile.name : '';
  }
}

function getClientSettings(): UserManagerSettings {
  return {
      authority: environment.identityUrl,
      client_id: 'dashboard-spa',
      redirect_uri: environment.spaBaseUrl + '/auth-callback',
      post_logout_redirect_uri: environment.spaBaseUrl,
      response_type: 'id_token token',
      scope: 'openid profile menu-api order-api basket-api',
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: environment.spaBaseUrl + '/silent-refresh.html'
  };
}
