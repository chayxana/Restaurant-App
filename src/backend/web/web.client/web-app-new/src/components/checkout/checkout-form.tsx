'use client';

import { PaymentDetails } from '@/components/checkout/payment-details';
import { ShippingAddressDetails } from '@/components/checkout/shipping-address-details';
import React from 'react';
import { checkoutServer } from './actions';
import { useFormState } from 'react-dom';

export function CheckoutForm() {
  const [message, formAction] = useFormState(checkoutServer, null);

  return (
    <form action={formAction}>
      <div className="mb-3 border-b border-gray-200">
        <PaymentDetails />
      </div>
      <div className="border-black-200 border-b">
        <ShippingAddressDetails />
      </div>
      {message?.backend_error && (
        <div>
          <h2>Something went wrong!</h2>
          <button>Try again</button>
        </div>
      )}
      {message?.client_error &&
        message.client_error?.map((m) => (
          <div key={m.code}>
            {m.path}: {m.message}
          </div>
        ))}
      <button
        type="submit"
        className="w-full rounded bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-600"
      >
        Complete
      </button>
    </form>
  );
}
