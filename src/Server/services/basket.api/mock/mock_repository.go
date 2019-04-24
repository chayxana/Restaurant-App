package mock

import (
	"github.com/jurabek/basket.api/models"
	"github.com/stretchr/testify/mock"
)

// BasketRepositoryMock repository extended from Mock
type BasketRepositoryMock struct {
	mock.Mock
}

// GetBasket mock
func (r *BasketRepositoryMock) GetBasket(customerID string) (*models.CustomerBasket, error) {
	args := r.Called(customerID)
	return args.Get(0).(*models.CustomerBasket), args.Error(1)
}

// Update Mock
func (r *BasketRepositoryMock) Update(item *models.CustomerBasket) error {
	args := r.Called(item)
	return args.Error(0)
}

// Delete mock
func (r *BasketRepositoryMock) Delete(id string) error {
	args := r.Called(id)
	return args.Error(0)
}
