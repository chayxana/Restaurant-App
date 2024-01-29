package repositories

import (
	"encoding/json"
	"testing"

	"github.com/gomodule/redigo/redis"
	"github.com/google/uuid"
	"github.com/jurabek/cart-api/internal/models"
	"github.com/stretchr/testify/mock"
)

// RedisConnectionProviderMock mocked redis provider
type RedisConnectionProviderMock struct {
	mock.Mock
}

// Get return instance of RedisConnMock
func (r *RedisConnectionProviderMock) Get() redis.Conn {
	return new(RedisConnMock)
}

// RedisConnMock mocked struct for redis.Conn
type RedisConnMock struct {
	mock.Mock
}

// Close mocks redis Close method
func (r *RedisConnMock) Close() error {
	return nil
}

// Err mocks Redis Err method
func (r *RedisConnMock) Err() error {
	return nil
}

// Do mocks redis Do method
func (r *RedisConnMock) Do(commandName string, args ...interface{}) (reply interface{}, err error) {
	a := r.Called(commandName, args)
	return a.Get(0), a.Error(1)
}

// Send mocks redis send method
func (r *RedisConnMock) Send(commandName string, args ...interface{}) error {
	a := r.Called(commandName, args)
	return a.Error(0)
}

// Flush mocks redis flush method
func (r *RedisConnMock) Flush() error {
	return nil
}

// Receive mocks redis Receive method
func (r *RedisConnMock) Receive() (reply interface{}, err error) {
	args := r.MethodCalled("Receive")
	return args.Get(0), args.Error(1)
}

var items = []models.LineItem{{
	ItemID:      1,
	UnitPrice:   20,
	Quantity:    1,
	Image:       "picture",
	ProductName: "foodName",
},
}

func TestRedisRepository(t *testing.T) {

	cart := models.Cart{
		ID:        uuid.New(),
		LineItems: items,
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
