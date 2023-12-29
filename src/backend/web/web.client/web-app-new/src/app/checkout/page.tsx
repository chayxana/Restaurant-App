import { getServerSession } from "next-auth";
import { authOptions } from "../api/auth/[...nextauth]/route";
import { CheckoutComponent } from "./checkout";
import { redirect } from "next/navigation";

const Page = async () => {
  const session = await getServerSession(authOptions);
  return (
    <>{session?.user ? <CheckoutComponent /> : redirect("api/auth/signin")}</>
  );
};

export default Page;
