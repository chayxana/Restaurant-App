// Original file: pb/payments.proto

import type { CreditCardInfo as _payment_CreditCardInfo, CreditCardInfo__Output as _payment_CreditCardInfo__Output } from '../payment/CreditCardInfo';

export interface PaymentRequest {
  'amount'?: (number | string);
  'userId'?: (string);
  'orderId'?: (string);
  'creditCard'?: (_payment_CreditCardInfo | null);
  'cartId'?: (string);
}

export interface PaymentRequest__Output {
  'amount': (number);
  'userId': (string);
  'orderId': (string);
  'creditCard': (_payment_CreditCardInfo__Output | null);
  'cartId': (string);
}
