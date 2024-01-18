'use server';

import { authOptions } from '@/lib/auth';
import {
  CheckoutResponse,
  CheckoutResponseScheme,
  addressSchema,
  checkoutScheme,
  creditCardSchema
} from '@/lib/types/checkout';
import { createUrl } from '@/lib/utils';
import { getServerSession } from 'next-auth';
import { redirect } from 'next/navigation';
import { ZodIssue } from 'zod';

const checkoutUrl = `${process.env.API_BASE_URL}/checkout/api/v1/checkout`;

export type CheckoutError = {
  backend_error?: {
    error_message: unknown;
  };
  client_error?: ZodIssue[];
};

export async function checkoutServer(prevState: any, formData: FormData): Promise<CheckoutError> {
  // payment data
  const expiration =
    formData.get('cardExpiration')?.toString()?.split('/') || 'invalid expiration date';
  const creditCardInfoValidated = creditCardSchema.safeParse({
    name_on_card: formData.get('nameOnCard'),
    credit_card_number: formData.get('cardNumber'),
    credit_card_cvv: Number(formData.get('cardCVC')),
    credit_card_expiration_month: Number(expiration[0]),
    credit_card_expiration_year: Number(expiration[1])
  });

  if (!creditCardInfoValidated.success) {
    return { client_error: creditCardInfoValidated.error.issues };
  }

  const addresDetailsValidated = addressSchema.safeParse({
    street_address: formData.get('streetAddress'),
    city: formData.get('city'),
    state: formData.get('state'),
    zip_code: Number(formData.get('postalCode')),
    country: 'default'
  });

  if (!addresDetailsValidated.success) {
    return { client_error: addresDetailsValidated.error.issues };
  }

  const session = await getServerSession(authOptions);

  const checkout = checkoutScheme.parse({
    address: addresDetailsValidated.data,
    credit_card: creditCardInfoValidated.data,
    customer_id: session?.user.user_id
  });

  const requestOptions: RequestInit = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      Authorization: `Bearer ${session?.user.token}`
    },
    body: JSON.stringify(checkout),
    cache: 'no-store',
    next: { tags: ['checkout'] }
  };

  const res = await fetch(checkoutUrl, requestOptions);
  if (!res.ok) {
    const body = JSON.stringify(await res.json());
    console.log(body);
    return { backend_error: { error_message: body } };
  }

  const checkouted: CheckoutResponse = CheckoutResponseScheme.parse(await res.json());
  const params = new URLSearchParams({
    transactionId: checkouted.transaction_id,
    checkoutId: checkouted.checkout_id
  });

  redirect(createUrl(`/checkout/complete`, params));
}
