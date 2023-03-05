package handlers

import (
	"context"
	"encoding/json"
	"fmt"
	"testing"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/basket.api/internal/models"
	"github.com/jurabek/basket.api/internal/repositories"
	"github.com/stretchr/testify/mock"
)

type mockedPublisher struct {
	mock.Mock
}

// Publish implements EventPublisher
func (m *mockedPublisher) Publish(ctx context.Context, data []byte) error {
	args := m.Called(ctx, data)
	return args.Error(0)
}

var _ EventPublisher = (*mockedPublisher)(nil)

func TestCheckOutHandler_Checkout(t *testing.T) {
	tests := []struct {
		name string
	}{
		{
			name: "test",
		},
	}
	for _, tt := range tests {
		t.Run(tt.name, func(t *testing.T) {

			mockedPublisher := &mockedPublisher{}
			mockedCustomerGetter := &repositories.BasketRepositoryMock{}

			_ = &CheckOutHandler{
				customerBasketGetter: mockedCustomerGetter,
				publisher:            mockedPublisher,
			}

			customerCheckout := &models.UserCheckoutEvent{
				CheckOutInfo: &models.Checkout{
					Address:    &models.Address{},
					CreditCard: &models.CreditCardInfo{},
				},
				CustomerBasket: &models.CustomerBasket{},
			}

			str, _ := json.Marshal(customerCheckout)

			fmt.Print(string(str))
			// h.Checkout(nil)
		})
	}

	router := gin.Default()
	_ = router.Group("basket")
	{
	}
}
