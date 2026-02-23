import { AuthOptions } from 'next-auth';
import KeycloakProvider from 'next-auth/providers/keycloak';

const internalIssuer = process.env.AUTH_INTERNAL_ISSUER ?? process.env.AUTH_ISSUER;

console.log('[auth] AUTH_ISSUER:', process.env.AUTH_ISSUER);
console.log('[auth] AUTH_INTERNAL_ISSUER:', process.env.AUTH_INTERNAL_ISSUER);
console.log('[auth] resolved internalIssuer:', internalIssuer);
console.log('[auth] wellKnown:', `${internalIssuer}/.well-known/openid-configuration`);

export const authOptions: AuthOptions = {
  providers: [
    KeycloakProvider({
      id: 'web-app',
      name: 'Restaraunt App Identity',
      clientId: process.env.AUTH_CLIENT_ID!,
      clientSecret: process.env.AUTH_CLIENT_SECRET!,
      issuer: process.env.AUTH_ISSUER!,
      wellKnown: `${internalIssuer}/.well-known/openid-configuration`,
      token: `${internalIssuer}/protocol/openid-connect/token`,
      userinfo: `${internalIssuer}/protocol/openid-connect/userinfo`,
      authorization: {
        url: `${process.env.AUTH_ISSUER!}/protocol/openid-connect/auth`,
        params: {
          scope: 'openid profile catalog-api order-api cart-api payment-api checkout-api'
        }
      }
    })
  ],
  secret: process.env.NEXTAUTH_SECRET,
  logger: {
    error(code, metadata) {
      console.error('[next-auth][error]', code, JSON.stringify(metadata, null, 2));
    },
    warn(code) {
      console.warn('[next-auth][warn]', code);
    },
    debug(code, metadata) {
      console.log('[next-auth][debug]', code, JSON.stringify(metadata, null, 2));
    }
  },
  callbacks: {
    async jwt({ token, account }) {
      if (account) {
        console.log('[auth][jwt] received account, access_token present:', !!account.access_token);
        token.accessToken = account.access_token;
      }
      return token;
    },
    async session({ session, token }) {
      session.user.user_id = token.sub || 'empty user id, something went wrong';
      session.user.token = token.accessToken as string;
      return session;
    }
  },
  events: {
    async signIn({ user, account, isNewUser }) {
      console.log('[auth][event] signIn', { userId: user.id, provider: account?.provider, isNewUser });
    },
    async signOut() {
      console.log('[auth][event] signOut');
    }
  }
};
