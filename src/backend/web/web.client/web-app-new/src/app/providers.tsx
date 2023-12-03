import { CartProvider } from "@/context/CartContext";
import { PropsWithChildren } from "react";

export const Providers = ({ children }: PropsWithChildren<{}>) => {
  return <CartProvider>{children}</CartProvider>;
};
