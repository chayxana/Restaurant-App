package repositories

import (
	"encoding/json"
	"testing"

	"github.com/google/uuid"
	"github.com/jurabek/basket.api/mock"
	"github.com/jurabek/basket.api/models"
)

var items = []models.BasketItem{{
	ID:           uuid.New(),
	FoodID:       uuid.New(),
	UnitPrice:    20,
	OldUnitPrice: 10,
	Quantity:     1,
	Picture:      "picture",
	FoodName:     "foodName",
},
}

func TestRedisRepository(t *testing.T) {

	customerBasket := models.CustomerBasket{
		CustomerID: uuid.New(),
		Items:      &items,
	}

	mockConnectionProvider := mock.RedisConnectionProviderMock{}
	// repository := RedisBasketRepository{
	// 	Conn: &mockConnectionProvider,
	// }

	customerID := customerBasket.CustomerID.String()

	customerBasketString, _ := json.Marshal(&customerBasket)
	mockConnectionProvider.On("Do", "GET", customerID).Return(customerBasketString)

	t.Run("given existing customerID GetBasket should return object", func(t *testing.T) {
		// result, err := repository.GetBasket(customerID)

		// assert.Nil(t, err)
		// assert.Equal(t, customerBasket.CustomerID, result.CustomerID)
	})
}
