"use client";
import { CartProvider } from "@/context/CartContext";
import { PropsWithChildren } from "react";
import { ThemeProvider } from "next-themes";
import { SessionProvider } from "next-auth/react";

export const Providers = ({ children }: PropsWithChildren<{}>) => {
  return (
    <SessionProvider>
      <CartProvider>{children}</CartProvider>
    </SessionProvider>
  );
};
