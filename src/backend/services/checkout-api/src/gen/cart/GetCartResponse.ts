// Original file: pb/cart.proto

import type { CartItem as _cart_CartItem, CartItem__Output as _cart_CartItem__Output } from '../cart/CartItem';

export interface GetCartResponse {
  'cartId'?: (string);
  'items'?: (_cart_CartItem)[];
}

export interface GetCartResponse__Output {
  'cartId': (string);
  'items': (_cart_CartItem__Output)[];
}
