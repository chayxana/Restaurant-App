import NextAuth, { AuthOptions } from "next-auth"
import IdentityServer4Provider from "next-auth/providers/identity-server4";

const authOptions: AuthOptions = {
    providers: [
        IdentityServer4Provider({
            id: "web-app",
            name: "Restaraunt App Identity",
            clientId: process.env.AUTH_CLIENT_ID,
            clientSecret: process.env.AUTH_CLIENT_SECRET,
            issuer: process.env.AUTH_ISSUER,
            authorization: { params: { scope: "openid profile catalog-api order-api basket-api payment-api checkout-api" } },
        })
    ],
    secret: process.env.NEXTAUTH_SECRET,
    callbacks: {
        async session({ session, token, user }) {
            session.user = token;
            return session
        },
    }
}

const handler = NextAuth(authOptions);
export { handler as GET, handler as POST }