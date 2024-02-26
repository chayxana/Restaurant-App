import { getOrderByTransactionID } from '@/lib/fetch';
import CheckoutCompleted from '../../components/checkout/checkout-completed';
import { revalidateTag } from 'next/cache';

export default async function Page({ searchParams }: { searchParams?: { [key: string]: string } }) {
  const address = '';
  let orderNumber = '';
  let orderDate = new Date().toISOString();
  let orderTotal = '';

  if (searchParams?.transactionId) {
    const order = await getOrderByTransactionID(searchParams?.transactionId);
    orderNumber = order.id;
    orderDate = order.orderedDate;
    const totalPrice = order.orderItems.reduce(
      (total, item) => total + item.unitPrice * item.units,
      0
    );
    orderTotal = `${totalPrice.toFixed(2)} Eur`;
  }
  revalidateTag('cart');

  return (
    <CheckoutCompleted
      orderTotal={orderTotal}
      address={address}
      orderNumber={orderNumber}
      orderDateTime={orderDate}
    />
  );
}
