import { expect } from 'chai';
import sinon from 'sinon';
import  { cartService } from './cartService';
import { GetCartResponse } from '../gen/cart/GetCartResponse';

describe('CartService', () => {
  describe('getCustomerCartItems', () => {
    it('should retrieve cart items for a customer', async () => {
      const customerId = 'customer123';
      const expectedResponse: GetCartResponse = {
        items: [
          {
            itemId: 'item1',
            price: 10,
            quantity: 2,
          },
          {
            itemId: 'item2',
            price: 20,
            quantity: 1,
          },
        ],
      };
    });
  });
});
