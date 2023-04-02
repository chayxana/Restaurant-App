// Original file: pb/cart.proto

import type * as grpc from '@grpc/grpc-js'
import type { MethodDefinition } from '@grpc/proto-loader'
import type { GetCustomerCartRequest as _cart_GetCustomerCartRequest, GetCustomerCartRequest__Output as _cart_GetCustomerCartRequest__Output } from '../cart/GetCustomerCartRequest';
import type { GetCustomerCartResponse as _cart_GetCustomerCartResponse, GetCustomerCartResponse__Output as _cart_GetCustomerCartResponse__Output } from '../cart/GetCustomerCartResponse';

export interface CartServiceClient extends grpc.Client {
  GetCustomerCart(argument: _cart_GetCustomerCartRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  GetCustomerCart(argument: _cart_GetCustomerCartRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  GetCustomerCart(argument: _cart_GetCustomerCartRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  GetCustomerCart(argument: _cart_GetCustomerCartRequest, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  getCustomerCart(argument: _cart_GetCustomerCartRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  getCustomerCart(argument: _cart_GetCustomerCartRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  getCustomerCart(argument: _cart_GetCustomerCartRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  getCustomerCart(argument: _cart_GetCustomerCartRequest, callback: grpc.requestCallback<_cart_GetCustomerCartResponse__Output>): grpc.ClientUnaryCall;
  
}

export interface CartServiceHandlers extends grpc.UntypedServiceImplementation {
  GetCustomerCart: grpc.handleUnaryCall<_cart_GetCustomerCartRequest__Output, _cart_GetCustomerCartResponse>;
  
}

export interface CartServiceDefinition extends grpc.ServiceDefinition {
  GetCustomerCart: MethodDefinition<_cart_GetCustomerCartRequest, _cart_GetCustomerCartResponse, _cart_GetCustomerCartRequest__Output, _cart_GetCustomerCartResponse__Output>
}
