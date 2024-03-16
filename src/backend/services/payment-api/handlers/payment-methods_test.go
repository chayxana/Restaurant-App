package handlers

import (
	"net/http"
	"testing"
)

func TestPaymentMethodsHandler_GetPaymentMethod(t *testing.T) {
	type args struct {
		w http.ResponseWriter
		r *http.Request
	}
	tests := []struct {
		name string
		p    *PaymentMethodsHandler
		args args
	}{
		// TODO: Add test cases.
	}
	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {
			p := &PaymentMethodsHandler{}
			p.GetPaymentMethod(tt.args.w, tt.args.r)
		})
	}
}
