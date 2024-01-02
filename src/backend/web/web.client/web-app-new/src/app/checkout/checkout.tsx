"use client";
import React, { useState } from "react";

export const CheckoutComponent: React.FC = () => {
  const [formData, setFormData] = useState({
    fullName: "",
    email: "",
    street_address: "",
    city: "",
    state: "",
    zip_code: "",
    country: "",
    credit_card_number: "",
    credit_card_expiration_month: "",
    credit_card_expiration_year: "",
    credit_card_cvv: "",
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
      <h1 className="text-3xl font-bold mb-6">Checkout</h1>
      <form onSubmit={handleSubmit} className="max-w-lg mx-auto">
        {/* Customer Information */}
        <div className="mb-4">
          <label htmlFor="fullName" className="block mb-2">
            Full Name
          </label>
          <input
            type="text"
            name="fullName"
            id="fullName"
            value={formData.fullName}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded"
            required
          />
        </div>
        <div className="mb-4">
          <label htmlFor="email" className="block mb-2">
            Email
          </label>
          <input
            type="email"
            name="email"
            id="email"
            value={formData.email}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded"
            required
          />
        </div>

        {/* Address Details */}
        <fieldset className="mb-4">
          <legend className="text-xl font-semibold mb-4">Address</legend>
          <div className="mb-4">
            <label htmlFor="street_address" className="block mb-2">
              Street Address
            </label>
            <input
              type="text"
              name="street_address"
              id="street_address"
              value={formData.street_address}
              onChange={handleChange}
              className="w-full p-2 border border-gray-300 rounded"
              required
            />
          </div>
          {/* ... other address fields such as city, state, zip_code, and country */}
        </fieldset>

        {/* Credit Card Details */}
        <fieldset>
          <legend className="text-xl font-semibold mb-4">Credit Card</legend>
          <div className="mb-4">
            <label htmlFor="credit_card_number" className="block mb-2">
              Card Number
            </label>
            <input
              type="text"
              name="credit_card_number"
              id="credit_card_number"
              value={formData.credit_card_number}
              onChange={handleChange}
              className="w-full p-2 border border-gray-300 rounded"
              required
            />
          </div>
          <div className="flex gap-4 mb-4">
            <div className="flex-1">
              <label
                htmlFor="credit_card_expiration_month"
                className="block mb-2"
              >
                Expiration Month
              </label>
              <input
                type="text"
                name="credit_card_expiration_month"
                id="credit_card_expiration_month"
                value={formData.credit_card_expiration_month}
                onChange={handleChange}
                className="w-full p-2 border border-gray-300 rounded"
                required
              />
            </div>
            <div className="flex-1">
              <label
                htmlFor="credit_card_expiration_year"
                className="block mb-2"
              >
                Expiration Year
              </label>
              <input
                type="text"
                name="credit_card_expiration_year"
                id="credit_card_expiration_year"
                value={formData.credit_card_expiration_year}
                onChange={handleChange}
                className="w-full p-2 border border-gray-300 rounded"
                required
              />
            </div>
            <div className="flex-1">
              <label htmlFor="credit_card_cvv" className="block mb-2">
                CVV
              </label>
              <input
                type="text"
                name="credit_card_cvv"
                id="credit_card_cvv"
                value={formData.credit_card_cvv}
                onChange={handleChange}
                className="w-full p-2 border border-gray-300 rounded"
                required
              />
            </div>
          </div>
        </fieldset>

        <button
          type="submit"
          className="bg-orange-500 hover:bg-orange-600 text-white font-bold py-2 px-4 rounded"
        >
          Complete
        </button>
      </form>
    </div>
  );
};
