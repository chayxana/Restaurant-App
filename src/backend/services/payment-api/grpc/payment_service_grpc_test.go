package grpc

import (
	"context"
	"testing"

	pbv1 "github.com/chayxana/payment-api/pb/v1"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/mock"
)

func TestPaymentServiceGrpc_Payment(t *testing.T) {
	tests := []struct {
		name            string
		enableTestCards bool
		req             *pbv1.PaymentRequest
		want            *pbv1.PaymentResponse
		wantErr         bool
	}{
		{
			name: "Successful payment with valid Visa card",
			req: &pbv1.PaymentRequest{
				CreditCard: &pbv1.CreditCardInfo{
					CreditCardNumber:          "4111111111111111",
					CreditCardCvv:             123,
					CreditCardExpirationMonth: 12,
					CreditCardExpirationYear:  2032,
				},
				Amount:  10.99,
				OrderId: "ORDER123",
				UserId:  "USER123",
			},
			enableTestCards: true,
			want: &pbv1.PaymentResponse{
				TransactionId: mock.Anything,
			},
		},
	}

	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			p := NewPaymentServiceGrpc(tt.enableTestCards)

			got, err := p.Payment(context.Background(), tt.req)

			if (err != nil) != tt.wantErr {
				t.Errorf("PaymentServiceGrpc.Payment() error = %v, wantErr %v", err, tt.wantErr)
				return
			}

			assert.NotNil(t, got)
		})
	}
}
