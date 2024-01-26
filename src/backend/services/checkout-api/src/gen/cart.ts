import type * as grpc from '@grpc/grpc-js';
import type { MessageTypeDefinition } from '@grpc/proto-loader';

import type { CartServiceClient as _cart_CartServiceClient, CartServiceDefinition as _cart_CartServiceDefinition } from './cart/CartService';

type SubtypeConstructor<Constructor extends new (...args: any) => any, Subtype> = {
  new(...args: ConstructorParameters<Constructor>): Subtype;
};

export interface ProtoGrpcType {
  cart: {
    CartItem: MessageTypeDefinition
    CartService: SubtypeConstructor<typeof grpc.Client, _cart_CartServiceClient> & { service: _cart_CartServiceDefinition }
    GetCartRequest: MessageTypeDefinition
    GetCartResponse: MessageTypeDefinition
  }
}

