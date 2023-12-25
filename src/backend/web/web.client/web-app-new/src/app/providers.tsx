import { CartProvider } from "@/context/CartContext";
import { PropsWithChildren } from "react";
import { ThemeProvider } from "next-themes"

export const Providers = ({ children }: PropsWithChildren<{}>) => {
  return <CartProvider>{children}</CartProvider>;
};
