package repositories

import (
	"encoding/json"
	"testing"

	"github.com/google/uuid"
	"github.com/jurabek/cart-api/internal/models"
)

var items = []models.LineItem{{
	ItemID:       1,
	UnitPrice:    20,
	Quantity:     1,
	Image:      "picture",
	ProductName:     "foodName",
},
}

func TestRedisRepository(t *testing.T) {

	customerBasket := models.Cart{
		ID: uuid.New(),
		LineItems:      &items,
	}

	mockConnectionProvider := RedisConnectionProviderMock{}
	// repository := RedisBasketRepository{
	// 	Conn: &mockConnectionProvider,
	// }

	customerID := customerBasket.ID.String()

	customerBasketString, _ := json.Marshal(&customerBasket)
	mockConnectionProvider.On("Do", "GET", customerID).Return(customerBasketString)

	t.Run("given existing customerID GetBasket should return object", func(t *testing.T) {
		// result, err := repository.GetBasket(customerID)

		// assert.Nil(t, err)
		// assert.Equal(t, customerBasket.CustomerID, result.CustomerID)
	})
}
