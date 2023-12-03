'use client'

import React, { useState } from 'react';

const CheckoutPage: React.FC = () => {
    const [formData, setFormData] = useState({
        fullName: '',
        email: '',
        address: '',
        city: '',
        zip: '',
        country: '',
        cardNumber: '',
        cardExpiration: '',
        cardCVC: '',
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        // Process the checkout data here
        console.log(formData);
    };

    return (
        <div className="container mx-auto p-6">
            <h1 className="text-3xl font-bold mb-6">Checkout</h1>
            <form onSubmit={handleSubmit} className="max-w-lg mx-auto">
                {/* Full Name */}
                <div className="mb-4">
                    <label htmlFor="fullName" className="block mb-2">Full Name</label>
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
                {/* ... other input fields */}
                {/* Card Details */}
                <div className="mb-4">
                    <label htmlFor="cardNumber" className="block mb-2">Card Number</label>
                    <input
                        type="text"
                        name="cardNumber"
                        id="cardNumber"
                        value={formData.cardNumber}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div className="flex gap-4 mb-4">
                    <div className="flex-1">
                        <label htmlFor="cardExpiration" className="block mb-2">Expiration</label>
                        <input
                            type="text"
                            name="cardExpiration"
                            id="cardExpiration"
                            value={formData.cardExpiration}
                            onChange={handleChange}
                            className="w-full p-2 border border-gray-300 rounded"
                            required
                        />
                    </div>
                    <div className="flex-1">
                        <label htmlFor="cardCVC" className="block mb-2">CVC</label>
                        <input
                            type="text"
                            name="cardCVC"
                            id="cardCVC"
                            value={formData.cardCVC}
                            onChange={handleChange}
                            className="w-full p-2 border border-gray-300 rounded"
                            required
                        />
                    </div>
                </div>
                <button type="submit" className="bg-orange-500 hover:bg-orange-600 text-white font-bold py-2 px-4 rounded">
                    Complete Order
                </button>
            </form>
        </div>
    );
};

export default CheckoutPage;
