// Original file: pb/payments.proto

import type * as grpc from '@grpc/grpc-js'
import type { MethodDefinition } from '@grpc/proto-loader'
import type { PaymentRequest as _payment_PaymentRequest, PaymentRequest__Output as _payment_PaymentRequest__Output } from '../payment/PaymentRequest';
import type { PaymentResponse as _payment_PaymentResponse, PaymentResponse__Output as _payment_PaymentResponse__Output } from '../payment/PaymentResponse';

export interface PaymentServiceClient extends grpc.Client {
  Payment(argument: _payment_PaymentRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  Payment(argument: _payment_PaymentRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  Payment(argument: _payment_PaymentRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  Payment(argument: _payment_PaymentRequest, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  payment(argument: _payment_PaymentRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  payment(argument: _payment_PaymentRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  payment(argument: _payment_PaymentRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  payment(argument: _payment_PaymentRequest, callback: grpc.requestCallback<_payment_PaymentResponse__Output>): grpc.ClientUnaryCall;
  
}

export interface PaymentServiceHandlers extends grpc.UntypedServiceImplementation {
  Payment: grpc.handleUnaryCall<_payment_PaymentRequest__Output, _payment_PaymentResponse>;
  
}

export interface PaymentServiceDefinition extends grpc.ServiceDefinition {
  Payment: MethodDefinition<_payment_PaymentRequest, _payment_PaymentResponse, _payment_PaymentRequest__Output, _payment_PaymentResponse__Output>
}
