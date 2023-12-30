import { config } from '../config';
import * as grpc from '@grpc/grpc-js'
import * as protoLoader from '@grpc/proto-loader'
import { ProtoGrpcType } from '../gen/payments';
import path from 'path'
import { UserCheckoutReq } from '../model';
import { PaymentResponse } from '../gen/payment/PaymentResponse';
import { PaymentRequest } from '../gen/payment/PaymentRequest';
import { CartItem } from '../gen/cart/CartItem';

const packageDefinition = protoLoader.loadSync(
  path.resolve(__dirname, '../../pb/payments.proto'),
);
const proto = grpc.loadPackageDefinition(packageDefinition) as unknown as ProtoGrpcType;

export const paymentService = new proto.payment.PaymentService(
  config.paymentAPIGrpcUrl,
  grpc.credentials.createInsecure()
);

const asyncPayment = (req: PaymentRequest): Promise<PaymentResponse> => {
  return new Promise<PaymentResponse>((resolve, reject) => {
    paymentService.Payment(req, (err: grpc.ServiceError | null, value?: PaymentResponse) => {
      if (err) {
        reject(err);
      } else {
        resolve(value!!);
      }
    })
  })
}

export default function Payment(
  checkoutID: string,
  userCheckout: UserCheckoutReq,
  cartItems: CartItem[],
): Promise<PaymentResponse> {
    return pay(cartItems, userCheckout, checkoutID);
}

export function pay(cartItems: CartItem[], userCheckout: UserCheckoutReq, orderId: string): Promise<PaymentResponse> {
  let amount = 0;
  for (const cartItem of cartItems) {
    const totalPrice = Number(cartItem.price) * Number(cartItem.quantity);
    amount += totalPrice;
  }
  const userId = userCheckout.customer_id;
  const creditCard = {
    creditCardNumber: userCheckout.credit_card.credit_card_number,
    creditCardCvv: userCheckout.credit_card.credit_card_cvv,
    creditCardExpirationMonth: userCheckout.credit_card.credit_card_expiration_month,
    creditCardExpirationYear: userCheckout.credit_card.credit_card_expiration_year,
  };

  return asyncPayment({
    amount, userId, orderId, creditCard
  });
}
