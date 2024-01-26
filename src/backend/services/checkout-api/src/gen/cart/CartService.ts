// Original file: pb/cart.proto

import type * as grpc from '@grpc/grpc-js'
import type { MethodDefinition } from '@grpc/proto-loader'
import type { GetCartRequest as _cart_GetCartRequest, GetCartRequest__Output as _cart_GetCartRequest__Output } from '../cart/GetCartRequest';
import type { GetCartResponse as _cart_GetCartResponse, GetCartResponse__Output as _cart_GetCartResponse__Output } from '../cart/GetCartResponse';

export interface CartServiceClient extends grpc.Client {
  GetCart(argument: _cart_GetCartRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  GetCart(argument: _cart_GetCartRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  GetCart(argument: _cart_GetCartRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  GetCart(argument: _cart_GetCartRequest, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  getCart(argument: _cart_GetCartRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  getCart(argument: _cart_GetCartRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  getCart(argument: _cart_GetCartRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  getCart(argument: _cart_GetCartRequest, callback: grpc.requestCallback<_cart_GetCartResponse__Output>): grpc.ClientUnaryCall;
  
}

export interface CartServiceHandlers extends grpc.UntypedServiceImplementation {
  GetCart: grpc.handleUnaryCall<_cart_GetCartRequest__Output, _cart_GetCartResponse>;
  
}

export interface CartServiceDefinition extends grpc.ServiceDefinition {
  GetCart: MethodDefinition<_cart_GetCartRequest, _cart_GetCartResponse, _cart_GetCartRequest__Output, _cart_GetCartResponse__Output>
}
