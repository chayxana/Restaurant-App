'use client';
import React, { useState } from 'react';

export const CheckoutComponent: React.FC = () => {
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

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    // TODO: Implement the checkout functionality here
    console.log(formData);
  };

  return (
    <div className="container mx-auto p-6">
      <h1 className="mb-6 text-3xl font-bold">Checkout</h1>
      <form onSubmit={handleSubmit} className="mx-auto max-w-lg">
        {/* Customer Information */}
        <div className="mb-4">
          <label htmlFor="fullName" className="mb-2 block">
            Full Name
          </label>
          <input
            type="text"
            name="fullName"
            id="fullName"
            value={formData.fullName}
            onChange={handleChange}
            className="w-full rounded border border-gray-300 p-2"
            required
          />
        </div>
        <div className="mb-4">
          <label htmlFor="email" className="mb-2 block">
            Email
          </label>
          <input
            type="email"
            name="email"
            id="email"
            value={formData.email}
            onChange={handleChange}
            className="w-full rounded border border-gray-300 p-2"
            required
          />
        </div>

        {/* Address Details */}
        <fieldset className="mb-4">
          <legend className="mb-4 text-xl font-semibold">Address</legend>
          <div className="mb-4">
            <label htmlFor="street_address" className="mb-2 block">
              Street Address
            </label>
            <input
              type="text"
              name="street_address"
              id="street_address"
              value={formData.street_address}
              onChange={handleChange}
              className="w-full rounded border border-gray-300 p-2"
              required
            />
          </div>
          {/* ... other address fields such as city, state, zip_code, and country */}
        </fieldset>

        {/* Credit Card Details */}
        <fieldset>
          <legend className="mb-4 text-xl font-semibold">Credit Card</legend>
          <div className="mb-4">
            <label htmlFor="credit_card_number" className="mb-2 block">
              Card Number
            </label>
            <input
              type="text"
              name="credit_card_number"
              id="credit_card_number"
              value={formData.credit_card_number}
              onChange={handleChange}
              className="w-full rounded border border-gray-300 p-2"
              required
            />
          </div>
          <div className="mb-4 flex gap-4">
            <div className="flex-1">
              <label htmlFor="credit_card_expiration_month" className="mb-2 block">
                Expiration Month
              </label>
              <input
                type="text"
                name="credit_card_expiration_month"
                id="credit_card_expiration_month"
                value={formData.credit_card_expiration_month}
                onChange={handleChange}
                className="w-full rounded border border-gray-300 p-2"
                required
              />
            </div>
            <div className="flex-1">
              <label htmlFor="credit_card_expiration_year" className="mb-2 block">
                Expiration Year
              </label>
              <input
                type="text"
                name="credit_card_expiration_year"
                id="credit_card_expiration_year"
                value={formData.credit_card_expiration_year}
                onChange={handleChange}
                className="w-full rounded border border-gray-300 p-2"
                required
              />
            </div>
            <div className="flex-1">
              <label htmlFor="credit_card_cvv" className="mb-2 block">
                CVV
              </label>
              <input
                type="text"
                name="credit_card_cvv"
                id="credit_card_cvv"
                value={formData.credit_card_cvv}
                onChange={handleChange}
                className="w-full rounded border border-gray-300 p-2"
                required
              />
            </div>
          </div>
        </fieldset>

        <button
          type="submit"
          className="rounded bg-orange-500 px-4 py-2 font-bold text-white hover:bg-orange-600"
        >
          Complete
        </button>
      </form>
    </div>
  );
};
