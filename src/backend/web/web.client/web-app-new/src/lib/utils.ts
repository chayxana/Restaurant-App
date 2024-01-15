import { ReadonlyURLSearchParams } from 'next/navigation';
import { CustomerCartItem } from './types/cart';
import { SessionCartItem } from '@/context/cart-context';

export const createUrl = (pathname: string, params: URLSearchParams | ReadonlyURLSearchParams) => {
  const paramsString = params.toString();
  const queryString = `${paramsString.length ? '?' : ''}${paramsString}`;

  return `${pathname}${queryString}`;
};


export function mapCustomerCartItemToSessionCartItem(cartItem: CustomerCartItem): SessionCartItem {
  return {
    id: cartItem.food_id,
    name: cartItem.food_name,
    description: cartItem.food_description,
    image: cartItem.picture,
    price: cartItem.unit_price,
    quantity: cartItem.quantity
  };
}