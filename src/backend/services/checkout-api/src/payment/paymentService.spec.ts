import sinon from 'sinon';
import { expect } from "chai";
import type * as grpc from '@grpc/grpc-js'
import { paymentService, pay } from "./paymentService";
import { CartItem, UserCheckoutReq } from "../models/model";
import { PaymentResponse } from '../gen/payment/PaymentResponse';
import { PaymentRequest } from '../gen/payment/PaymentRequest';


describe("pay", () => {

  let paymentServiceStub: sinon.SinonStub;

  beforeEach(() => {
    // Create a stub for Payment method
    paymentServiceStub = sinon.stub(paymentService, 'Payment');
  });

  afterEach(() => {
    // Restore the stubbed method after each test
    paymentServiceStub.restore();
  });

  it("should calculate the correct amount and call asyncPayment with the correct arguments", async () => {
    const cartItems: CartItem[] = [
      { item_id: "item1", price: 9.99, quantity: 2 },
      { item_id: "item2", price: 14.99, quantity: 1 },
    ];
    const userCheckout: UserCheckoutReq = {
      address: {
        street_address: "123 Main St",
        city: "Anytown",
        state: "CA",
        country: "USA",
        zip_code: 12345,
      },
      credit_card: {
        name_on_card: "test name",
        credit_card_number: "1234567812345678",
        credit_card_cvv: 123,
        credit_card_expiration_month: 12,
        credit_card_expiration_year: 23,
      },
      cart_id: "cart123",
      user_id: 'user123',
      user_currency: "USD",
    };
    const orderId = "abc123";
    // Set up the expected Payment request and response
    const expectedRequest: PaymentRequest = {
      amount: 34.97,
      userId: userCheckout.cart_id,
      orderId: orderId,
      creditCard: {
        creditCardNumber: userCheckout.credit_card.credit_card_number,
        creditCardCvv: userCheckout.credit_card.credit_card_cvv,
        creditCardExpirationMonth: userCheckout.credit_card.credit_card_expiration_month,
        creditCardExpirationYear: userCheckout.credit_card.credit_card_expiration_year,
      },
    };

    const expectedResponse: PaymentResponse = {
      transactionId: '456',
    };

    // Stub the Payment method to return the expected response
    paymentServiceStub.callsFake((req: PaymentRequest, callback: (err: grpc.ServiceError | null, value?: PaymentResponse) => void) => {
      expect(req).to.deep.equal(expectedRequest);
      callback(null, expectedResponse);
    });

    const result = await pay(cartItems, userCheckout, orderId, "some_user");
    expect(result).to.deep.equal(expectedResponse);
  });
});
