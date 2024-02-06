import { AuthOptions } from 'next-auth';
import IdentityServer4Provider from 'next-auth/providers/identity-server4';

export const authOptions: AuthOptions = {
  providers: [
    IdentityServer4Provider({
      id: 'web-app',
      name: 'Restaraunt App Identity',
      clientId: process.env.AUTH_CLIENT_ID,
      clientSecret: process.env.AUTH_CLIENT_SECRET,
      issuer: process.env.AUTH_ISSUER,
      authorization: {
        params: {
          scope: 'openid profile catalog-api order-api cart-api payment-api checkout-api'
        }
      }
    })
  ],
  secret: process.env.NEXTAUTH_SECRET,
  callbacks: {
    async jwt({ token, account }) {
      if (account) {
        console.log('has account: ' + account.access_token);
        token.accessToken = account.access_token;
      }
      return token;
    },
    async session({ session, token }) {
      session.user.user_id = token.sub || 'empty user id, something went wrong';
      session.user.token = token.accessToken as string;
      return session;
    }
  }
};
