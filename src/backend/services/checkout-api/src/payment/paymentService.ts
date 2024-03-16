import { config } from '../config/config';
import * as grpc from '@grpc/grpc-js'
import * as protoLoader from '@grpc/proto-loader'
import { ProtoGrpcType } from '../gen/payments';
import path from 'path'
import { UserCheckoutReq } from '../models/model';
import { PaymentResponse__Output } from '../gen/payment/PaymentResponse';
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

const asyncPayment = (req: PaymentRequest): Promise<PaymentResponse__Output> => {
  return new Promise<PaymentResponse__Output>((resolve, reject) => {
    paymentService.payment(req, (err: grpc.ServiceError | null, value?: PaymentResponse__Output) => {
      if (err) {
        reject(err);
      } else {
        if (!value) {
          reject(new Error("PaymentResponse__Output is undifined"))
        } else {
          resolve(value);
        }
      }
    })
  })
}

export function pay(cartItems: CartItem[], userCheckout: UserCheckoutReq, orderId: string, userId: string): Promise<PaymentResponse__Output> {
  let amount = 0;
  for (const cartItem of cartItems) {
    const totalPrice = Number(cartItem.price) * Number(cartItem.quantity);
    amount += totalPrice;
  }
  const { credit_card, cart_id: cartId } = userCheckout;
  const creditCard = {
    creditCardNumber: String(credit_card.credit_card_number),
    creditCardCvv: credit_card.credit_card_cvv,
    creditCardExpirationMonth: credit_card.credit_card_expiration_month,
    creditCardExpirationYear: credit_card.credit_card_expiration_year,
  };

  return asyncPayment({
    amount, userId, orderId, creditCard, cartId
  });
}
