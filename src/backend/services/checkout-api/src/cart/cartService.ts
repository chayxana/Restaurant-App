import { config } from '../config/config';
import * as grpc from '@grpc/grpc-js'
import * as protoLoader from '@grpc/proto-loader'
import { ProtoGrpcType } from '../gen/cart';
import path from 'path'
import { logger } from '../utils/logger';
import { GetCartRequest } from '../gen/cart/GetCartRequest';
import { GetCartResponse__Output } from '../gen/cart/GetCartResponse';

const packageDefinition = protoLoader.loadSync(
  path.resolve(__dirname, '../../pb/cart.proto'),
);
const proto = grpc.loadPackageDefinition(packageDefinition) as unknown as ProtoGrpcType;

export const cartService = new proto.cart.CartService(
  config.cartAPIGrpcUrl,
  grpc.credentials.createInsecure()
);

const asyncCustomerCartItems = (req: GetCartRequest): Promise<GetCartResponse__Output> => {
  return new Promise<GetCartResponse__Output>((resolve, reject) => {
    cartService.getCart(req, (err: grpc.ServiceError | null, value?: GetCartResponse__Output) => {
      if (err) {
        reject(err);
      } else {
        if (value) {
          resolve(value);
        } else {
          reject(new Error("GetCartResponse__Output is undifined"));
        }
      }
    })
  })
}

export default function getCustomerCartItems(cartId: string): Promise<GetCartResponse__Output> {
  logger.child({ cartId }).info("retrieving cart items for customer");
  return asyncCustomerCartItems({ cartId });
}
