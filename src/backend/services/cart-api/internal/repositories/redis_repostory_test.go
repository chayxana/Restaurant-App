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

	cart := models.Cart{
		ID: uuid.New(),
		LineItems:      items,
	}

	mockConnectionProvider := RedisConnectionProviderMock{}
	// repository := RedisBasketRepository{
	// 	Conn: &mockConnectionProvider,
	// }

	customerID := cart.ID.String()

	customerBasketString, _ := json.Marshal(&cart)
	mockConnectionProvider.On("Do", "GET", customerID).Return(customerBasketString)

	t.Run("given existing cart id Cart should return object", func(t *testing.T) {
		// result, err := repository.GetBasket(customerID)

		// assert.Nil(t, err)
		// assert.Equal(t, customerBasket.CustomerID, result.CustomerID)
	})
}
