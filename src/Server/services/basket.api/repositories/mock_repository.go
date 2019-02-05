package repositories

import (
	"github.com/jurabek/basket.api/models"
	"github.com/stretchr/testify/mock"
)

// MockBasketRepository repository extended from Mock
type MockBasketRepository struct {
	mock.Mock
}

// GetBasket mock
func (r *MockBasketRepository) GetBasket(customerID string) (*models.CustomerBasket, error) {
	args := r.Called(customerID)

	return args.Get(0).(*models.CustomerBasket), args.Error(1)
}

// Update Mock
func (r *MockBasketRepository) Update(item *models.CustomerBasket) error {
	args := r.Called(item)
	return args.Error(0)
}

// Delete mock
func (r *MockBasketRepository) Delete(id string) error {
	args := r.Called(id)
	return args.Error(0)
}
