package repositories

import (
	"github.com/jurabek/basket.api/models"
)

// BasketRepository abstraction for repository
type BasketRepository interface {
	GetBasket(customerID string) (*models.CustomerBasket, error)
	Update(item *models.CustomerBasket) error
	Delete(id string) error
}
