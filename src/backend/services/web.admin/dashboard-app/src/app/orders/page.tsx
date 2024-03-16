import OrdersTable from "@/components/orders-table";
import React from "react";

const ordersData = [
  {
    id: 123,
    customer: "customer 1",
  },
  {
    id: 124,
    customer: "customer 2",
  },
  {
    id: 123,
    customer: "customer 1",
  },
  {
    id: 124,
    customer: "customer 2",
  },
  {
    id: 123,
    customer: "customer 1",
  },
  {
    id: 124,
    customer: "customer 2",
  },
  {
    id: 123,
    customer: "customer 1",
  },
  {
    id: 124,
    customer: "customer 2",
  },
];

const OrdersPage: React.FC = () => {
  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-semibold mb-6">Recent Orders</h1>
      <OrdersTable orders={ordersData} />
    </div>
  );
};

export default OrdersPage;
