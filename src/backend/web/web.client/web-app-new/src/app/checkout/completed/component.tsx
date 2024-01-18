import React from 'react';
import { CheckCircleIcon } from '@heroicons/react/24/solid';

const OrderCompletion: React.FC = () => {
  // Replace with your actual data
  const orderDetails = {
    address: '24 Lexington Drive, Bella Vista, NSW 22353',
    deliveryTime: 'Ready at 2:20 PM 03 March 2020',
    orderNumber: 45,
    orderTotal: '$33.89',
    paymentMethod: 'Master Card ending **** 0987',
    estimatedDeliveryTime: '11:53 AM',
    email: 'some@paviw.co.uk',
    helpNumber: '+02 9629 4884'
  };

  return (
    <div className="mx-auto max-w-md rounded-lg bg-white p-6 shadow-lg">
      <div className="text-center">
        <CheckCircleIcon className="mx-auto h-12 w-12 text-green-500" />
        <h2 className="my-2 text-lg font-semibold">Order Submitted</h2>
        <p className="text-gray-600">
          {orderDetails.orderTotal} Paid with {orderDetails.paymentMethod}
        </p>
        <button className="my-4 rounded bg-orange-500 px-4 py-2 text-white">Track order</button>
      </div>
      <div className="my-4">
        <h3 className="font-bold">Delivery Address</h3>
        <p>{orderDetails.address}</p>
        <p>{orderDetails.deliveryTime}</p>
      </div>
      <div className="my-4">
        <h3 className="font-bold">Order Number</h3>
        <p>#{orderDetails.orderNumber}</p>
        <p>Estimated Delivery Time: {orderDetails.estimatedDeliveryTime}</p>
      </div>
      <div className="my-4">
        <p>An email confirmation will be sent to {orderDetails.email}</p>
      </div>
      <div className="my-4">
        <p>Need help with anything?</p>
        <p>Call {orderDetails.helpNumber}</p>
      </div>
      <button className="my-4 w-full rounded bg-gray-200 px-4 py-2 text-gray-800">
        Start a new order
      </button>
    </div>
  );
};

export default OrderCompletion;
