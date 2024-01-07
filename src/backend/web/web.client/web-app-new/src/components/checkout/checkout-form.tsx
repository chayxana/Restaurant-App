'use client';
import { PaymentDetails } from '@/components/checkout/payment-details';
import { ShippingAddressDetails } from '@/components/checkout/shipping-address-details';
import React, { useState } from 'react';

export const CheckoutForm: React.FC = () => {
  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    street_address: '',
    city: '',
    state: '',
    zip_code: '',
    country: '',
    credit_card_number: '',
    credit_card_expiration_month: '',
    credit_card_expiration_year: '',
    credit_card_cvv: ''
  });

  console.log(setFormData);

  // const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  //   setFormData({ ...formData, [e.target.name]: e.target.value });
  // };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    // TODO: Implement the checkout functionality here
    console.log(formData);
  };

  return (
    <form onSubmit={handleSubmit} className="mx-auto max-w-lg">
      <PaymentDetails />
      <ShippingAddressDetails />
      <button
        type="submit"
        className="rounded w-full bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-600"
      >
        Complete
      </button>
    </form>
  );
};
