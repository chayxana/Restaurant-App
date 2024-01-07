'use server';

import { CheckoutComponent } from './checkout';
import { getServerSession } from 'next-auth';
import { redirect } from 'next/navigation';
import { authOptions } from '@/lib/auth';
import { headers } from 'next/headers';

const Page = async () => {
  const session = await getServerSession(authOptions);
  if (!session?.user) {
    const headersList = headers();
    const callbackUrl = headersList.get('referer') || "";; //after sign redirect into checkout page again
    const urlParam = new URLSearchParams({ callbackUrl });
    return redirect(`api/auth/signin/web-app?${urlParam}`);
  }
  return <CheckoutComponent />;
};

export default Page;
