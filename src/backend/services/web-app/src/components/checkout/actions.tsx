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
import { cookies } from 'next/headers';
import { redirect } from 'next/navigation';
import { ZodIssue } from 'zod';

const checkoutUrl = `${process.env.INTERNAL_API_BASE_URL}/checkout/api/v1/checkout`;

export type CheckoutError = {
  backend_error?: {
    error_message: unknown;
  };
  client_error?: ZodIssue[];
};

export async function checkoutServer(prevState: any, formData: FormData): Promise<CheckoutError> {
  const cartId = cookies().get('cartId')?.value;
  if (!cartId) {
    return { backend_error: { error_message: 'Cart is empty' } };
  }

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
    cart_id: cartId,
    user_id: session?.user.user_id
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
  const body = await res.json();
  console.log(body);
  if (!res.ok) {
    const error = JSON.stringify(body);
    return { backend_error: { error_message: error } };
  }

  const checkouted: CheckoutResponse = CheckoutResponseScheme.parse(body);
  const params = new URLSearchParams({
    transactionId: checkouted.transaction_id,
    checkoutId: checkouted.checkout_id
  });

  cookies().delete('cartId');
  redirect(createUrl(`/completed`, params));
}
