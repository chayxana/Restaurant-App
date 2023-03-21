package repositories

import (
	"context"

	"github.com/jurabek/basket.api/internal/models"
	"github.com/stretchr/testify/mock"
)

// BasketRepositoryMock repository extended from Mock
type BasketRepositoryMock struct {
	mock.Mock
}

// Get mock
func (r *BasketRepositoryMock) Get(ctx context.Context, customerID string) (*models.CustomerBasket, error) {
	args := r.Called(ctx, customerID)
	return args.Get(0).(*models.CustomerBasket), args.Error(1)
}

// Update Mock
func (r *BasketRepositoryMock) Update(ctx context.Context, item *models.CustomerBasket) error {
	args := r.Called(ctx, item)
	return args.Error(0)
}

// Delete mock
func (r *BasketRepositoryMock) Delete(ctx context.Context, id string) error {
	args := r.Called(ctx, id)
	return args.Error(0)
}
