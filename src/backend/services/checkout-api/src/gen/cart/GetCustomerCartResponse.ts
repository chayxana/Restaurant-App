// Original file: pb/cart.proto

import type { CartItem as _cart_CartItem, CartItem__Output as _cart_CartItem__Output } from '../cart/CartItem';

export interface GetCustomerCartResponse {
  'customerId'?: (string);
  'items'?: (_cart_CartItem)[];
}

export interface GetCustomerCartResponse__Output {
  'customerId': (string);
  'items': (_cart_CartItem__Output)[];
}
