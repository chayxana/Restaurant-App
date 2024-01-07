'use server';

import { getServerSession } from 'next-auth';
import { redirect } from 'next/navigation';
import { authOptions } from '@/lib/auth';
import { headers } from 'next/headers';
import { CheckoutForm } from '@/components/checkout/checkout-form';

const Page = async () => {
  const session = await getServerSession(authOptions);
  if (!session?.user) {
    const host = headers().get('x-forwarded-host');
    const protocol = headers().get('x-forwarded-proto') || 'http';
    const callbackUrl = `${protocol}://${host}/checkout`;

    const urlParam = new URLSearchParams({ callbackUrl });
    return redirect(`api/auth/signin/web-app?${urlParam}`);
  }
  return (
    <div className="container mx-auto p-6">
      <h1 className="mb-6 text-3xl font-bold">Checkout</h1>
      <div className="rounded-lg bg-white p-6 shadow-lg">
        <CheckoutForm />
      </div>
    </div>
  );
};

export default Page;
