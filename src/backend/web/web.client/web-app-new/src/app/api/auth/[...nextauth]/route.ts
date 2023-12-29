import NextAuth, { AuthOptions } from "next-auth"
import IdentityServer4Provider from "next-auth/providers/identity-server4";

export const authOptions: AuthOptions = {
    providers: [
        IdentityServer4Provider({
            id: "web-app",
            name: "Restaraunt App Identity",
            clientId: "nextjs-web-app",
            clientSecret: "secret",
            issuer: "http://localhost:8080/identity",
            authorization: { params: { scope: "openid profile catalog-api order-api basket-api payment-api checkout-api" } },
        })
    ],
    secret: process.env.NEXTAUTH_SECRET,
}

const handler = NextAuth(authOptions);
export { handler as GET, handler as POST }