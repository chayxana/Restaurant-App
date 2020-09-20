// import { User, UserManager, UserManagerSettings } from 'oidc-client';

// export class AuthService {
//     private manager = new UserManager(getClientSettings());
//     private user: User | null;
//     // private _authNavStatusSource = new BehaviorSubject<boolean>(false);

//     // Observable navItem stream
//     // authNavStatus$ = this._authNavStatusSource.asObservable();

//     async hasSession(): Promise<boolean> {
//         try {
//             await this.manager.querySessionStatus();
//             return true;
//         } catch (error) {
//             return false;
//         }
//     }
//     async loginSilent() {
//         this.user = await this.manager.signinSilent();
//         //   this._authNavStatusSource.next(this.isAuthenticated());
//     }
//     async login() {
//         return this.manager.signinRedirect();
//     }

//     async completeAuthentication() {
//         this.user = await this.manager.signinRedirectCallback();
//         console.log(this.user);
//         //   this._authNavStatusSource.next(this.isAuthenticated());
//     }

//     async signOut() {
//         await this.manager.signoutRedirect();
//         await this.manager.clearStaleState();
//     }

//     isAuthenticated(): boolean {
//         return this.user != null && !this.user.expired;
//     }

//     get authHeader(): string {
//         if (this.user) {
//             console.log(`${this.user.token_type} ${this.user.access_token}`);
//             return `${this.user.token_type} ${this.user.access_token}`;
//         }
//         return "";
//     }

//     get name(): string {
//         // if (this.user && this.user.profile) {
//         //     return this.user != null ? this.user.profile.name : '';
//         // }
//         return '';
//     }
// }

// function getClientSettings(): UserManagerSettings {
//     return {
//         authority: process.env.REACT_APP_IDENTITY_URL,
//         client_id: 'dashboard-spa',
//         redirect_uri: window.location.origin + '/dashboard' + '/auth-callback',
//         post_logout_redirect_uri: window.location.origin + '/dashboard',
//         response_type: 'id_token token',
//         scope: 'openid profile menu-api order-api basket-api',
//         filterProtocolClaims: true,
//         loadUserInfo: true,
//         automaticSilentRenew: true,
//         silent_redirect_uri: window.location.origin + '/dashboard' + '/assets/silent-renew.html'
//     };
// }