'use server';

import { CheckoutComponent } from './checkout';
import { getServerSession } from 'next-auth';
import { redirect } from 'next/navigation';
import { authOptions } from '@/lib/auth';

const Page = async () => {
  const session = await getServerSession(authOptions);
  if (!session?.user) {
    const urlParam = new URLSearchParams({
      callbackUrl: window.location.href //after sign redirect into checkout page again
    });
    return redirect(`api/auth/signin/web-app?${urlParam}`);
  }

  return <CheckoutComponent />;
};

export default Page;
