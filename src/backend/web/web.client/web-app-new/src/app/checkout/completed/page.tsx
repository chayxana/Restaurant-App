import { getOrderByTransactionID, sleep } from '@/lib/fetch';
import OrderCompletion from './component';

export default async function Page({ searchParams }: { searchParams?: { [key: string]: string } }) {
  const address = '';
  let orderNumber = '';
  let orderDate = new Date().toISOString();
  let orderTotal = '';

  if (searchParams?.transactionId) {
    const order = await getOrderByTransactionID(searchParams?.transactionId);
    await sleep(4000);
    orderNumber = order.id;
    orderDate = order.orderedDate;
    const totalPrice = order.orderItems.reduce(
      (total, item) => total + item.unitPrice * item.units,
      0
    );
    orderTotal = `${totalPrice.toFixed(2)} Eur`;
  }

  return (
    <OrderCompletion
      orderTotal={orderTotal}
      address={address}
      orderNumber={orderNumber}
      orderDateTime={orderDate}
    />
  );
}
