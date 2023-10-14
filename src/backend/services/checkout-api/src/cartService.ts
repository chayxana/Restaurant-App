import { tracer } from './tracer'; // must be registered first
import { config } from './config';
import * as grpc from '@grpc/grpc-js'
import * as protoLoader from '@grpc/proto-loader'
import { ProtoGrpcType } from './gen/cart';
import path from 'path'
import { GetCustomerCartResponse } from './gen/cart/GetCustomerCartResponse';
import { GetCustomerCartRequest } from './gen/cart/GetCustomerCartRequest';
import { logger } from './logger';

const _ = tracer; // tracer must be loaded before all other imports

const packageDefinition = protoLoader.loadSync(
  path.resolve(__dirname, '../pb/cart.proto'),
);
const proto = grpc.loadPackageDefinition(packageDefinition) as unknown as ProtoGrpcType;

export const cartService = new proto.cart.CartService(
  config.cartAPIGrpcUrl,
  grpc.credentials.createInsecure()
);

const asyncCustomerCartItems = (req: GetCustomerCartRequest) : Promise<GetCustomerCartResponse> => {
  return new Promise<GetCustomerCartResponse>((resolve, reject) => {
    cartService.GetCustomerCart(req, (err: grpc.ServiceError | null, value?: GetCustomerCartResponse) => {
      if (err){
        reject(err);
      } else {
        resolve(value!!);
      }
    })
  })
}

export default function getCustomerCartItems(customerId: string): Promise<GetCustomerCartResponse | undefined> {
    logger.child({ "customer_id": customerId }).info("retrieving cart items for customer");
    return asyncCustomerCartItems({ customerId });
}
