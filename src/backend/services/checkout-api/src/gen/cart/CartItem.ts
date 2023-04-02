// Original file: pb/cart.proto

import type { Long } from '@grpc/proto-loader';

export interface CartItem {
  'itemId'?: (number | string | Long);
  'price'?: (number | string);
  'quantity'?: (number | string | Long);
}

export interface CartItem__Output {
  'itemId': (string);
  'price': (number);
  'quantity': (string);
}
