import { expect } from 'chai';
import sinon from 'sinon';
import { GetCustomerCartRequest } from './gen/cart/GetCustomerCartRequest';
import { GetCustomerCartResponse } from './gen/cart/GetCustomerCartResponse';
import  { cartService } from './cartService';

describe('CartService', () => {
  describe('getCustomerCartItems', () => {
    it('should retrieve cart items for a customer', async () => {
      const customerId = 'customer123';
      const expectedResponse: GetCustomerCartResponse = {
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
