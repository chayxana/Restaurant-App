"use client";

import { CheckoutComponent } from "./checkout";
import { signIn, useSession } from "next-auth/react";

const Page = () => {
  const { status } = useSession();
  return <>{status === "authenticated" ? <CheckoutComponent /> : signIn()}</>;
};

export default Page;
