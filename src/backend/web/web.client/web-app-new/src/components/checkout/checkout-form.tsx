'use client';
import { PaymentDetails } from '@/components/checkout/payment-details';
import { ShippingAddressDetails } from '@/components/checkout/shipping-address-details';
import React from 'react';
import { checkoutServer } from './actions';
import { useFormState } from 'react-dom';

export const CheckoutForm: React.FC = () => {
  const [message, formAction] = useFormState(checkoutServer, null);
  return (
    <form action={formAction} className="mx-auto max-w-lg">
      <PaymentDetails />
      <ShippingAddressDetails />
      <p aria-live="polite">
        {message?.errors.credit_card_cvv ? 'CVV:' + message?.errors.credit_card_cvv : ''}
      </p>
      <button
        type="submit"
        className="w-full rounded bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-600"
      >
        Complete
      </button>
    </form>
  );
};
