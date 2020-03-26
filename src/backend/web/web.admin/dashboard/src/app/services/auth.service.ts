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

  async hasSession(): Promise<boolean> {
    try {
      const _ = await this.manager.querySessionStatus();
      return true;
    } catch (error) {
      return false;
    }
  }
  async loginSilent() {
    this.user = await this.manager.signinSilent();
    this._authNavStatusSource.next(this.isAuthenticated());
  }
  async login() {
    return this.manager.signinRedirect();
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    console.log(this.user);
    this._authNavStatusSource.next(this.isAuthenticated());
  }

  async signOut() {
    const a = await this.manager.signoutRedirect();
    await this.manager.clearStaleState();
  }

  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  get authHeader(): string {
    console.log(`${this.user.token_type} ${this.user.access_token}`);
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
      redirect_uri: window.location.origin + '/dashboard' + '/auth-callback',
      post_logout_redirect_uri: window.location.origin + '/dashboard',
      response_type: 'id_token token',
      scope: 'openid profile menu-api order-api basket-api',
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: window.location.origin + '/dashboard' + '/assets/silent-renew.html'
  };
}
