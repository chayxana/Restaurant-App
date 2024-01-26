package handlers

import (
	"bytes"
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/google/uuid"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/cart-api/internal/models"
	"github.com/stretchr/testify/assert"
	"github.com/stretchr/testify/mock"
)

var items = []models.LineItem{{
	ItemID:      1,
	UnitPrice:   20,
	Quantity:    1,
	Image:       "picture",
	ProductName: "foodName",
},
}

// CartRepositoryMock repository extended from Mock
type CartRepositoryMock struct {
	mock.Mock
}

// UpdateItem implements GetCreateDeleter.
func (r *CartRepositoryMock) UpdateItem(ctx context.Context, cartID string, itemID int, item models.LineItem) error {
	args := r.Called(ctx, cartID, itemID, item)
	return args.Error(0)
}

// DeleteItem implements GetCreateDeleter.
func (r *CartRepositoryMock) DeleteItem(ctx context.Context, cartID string, itemID int) error {
	args := r.Called(ctx, cartID, itemID)
	return args.Error(0)
}

// AddItem implements GetCreateDeleter.
func (r *CartRepositoryMock) AddItem(ctx context.Context, cartID string, item models.LineItem) error {
	args := r.Called(ctx, cartID, item)
	return args.Error(0)
}

var _ GetCreateDeleter = (*CartRepositoryMock)(nil)

// Get mock
func (r *CartRepositoryMock) Get(ctx context.Context, customerID string) (*models.Cart, error) {
	args := r.Called(ctx, customerID)
	return args.Get(0).(*models.Cart), args.Error(1)
}

// Update Mock
func (r *CartRepositoryMock) Update(ctx context.Context, item *models.Cart) error {
	args := r.Called(ctx, item)
	return args.Error(0)
}

// Delete mock
func (r *CartRepositoryMock) Delete(ctx context.Context, id string) error {
	args := r.Called(ctx, id)
	return args.Error(0)
}

func TestCartHandler(t *testing.T) {
	t.Skip()
	ctx := context.TODO()

	gin.SetMode(gin.TestMode)
	customerBasket := models.Cart{
		ID:        uuid.New(),
		LineItems: items,
	}

	var mockedBasketRepository = &CartRepositoryMock{}

	mockedBasketRepository.On("Get", ctx, "abcd").Return(&customerBasket, nil).Once()
	mockedBasketRepository.On("Get", ctx, "invalid").Return(&customerBasket, fmt.Errorf("Not found item with id: %s", "invalid")).Once()
	mockedBasketRepository.On("Update", ctx, &customerBasket).Return(nil)
	var controller = NewCartHandler(mockedBasketRepository)

	router := gin.Default()
	basket := router.Group("basket")
	{
		basket.GET(":id", ErrorHandler(controller.Get))
		basket.POST("", ErrorHandler(controller.Create))
	}

	t.Run("Get should return ok when valid CustomerID", func(t *testing.T) {
		w := httptest.NewRecorder()
		r, _ := http.NewRequest("GET", "/basket/abcd", nil)
		router.ServeHTTP(w, r)
		assert.Equal(t, http.StatusOK, w.Code)
	})

	t.Run("Get should return BadRequest when invalid CustomerID", func(t *testing.T) {
		w := httptest.NewRecorder()
		r, _ := http.NewRequest("GET", "/basket/invalid", nil)
		router.ServeHTTP(w, r)
		assert.Equal(t, http.StatusBadRequest, w.Code)
	})

	t.Run("Create should create item and return ok", func(t *testing.T) {
		mockedBasketRepository.On("Get", ctx, customerBasket.ID.String()).Return(&customerBasket, nil)

		body, _ := json.Marshal(customerBasket)

		w := httptest.NewRecorder()
		r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
		router.ServeHTTP(w, r)

		assert.Equal(t, http.StatusOK, w.Code)

		var result models.Cart
		bodyResult, _ := ioutil.ReadAll(w.Body)
		_ = json.Unmarshal(bodyResult, &result)

		assert.Equal(t, result.ID, customerBasket.ID)
	})

	t.Run("Create should not create item and return code 400", func(t *testing.T) {
		invalidCustomerBasket := models.Cart{
			ID: uuid.New(),
		}
		mockedBasketRepository.On("Update", ctx, &invalidCustomerBasket).Return(fmt.Errorf("could not update item id: %s", customerBasket.ID))

		body, _ := json.Marshal(invalidCustomerBasket)

		w := httptest.NewRecorder()
		r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
		router.ServeHTTP(w, r)
		assert.Equal(t, http.StatusBadRequest, w.Code)
	})

	t.Run("Create should create item and when could not find created item return code 400", func(t *testing.T) {
		invalidCustomerBasket := models.Cart{
			ID: uuid.New(),
		}
		mockedBasketRepository.On("Update", ctx, &invalidCustomerBasket).Return(nil)
		mockedBasketRepository.On("Get", ctx, invalidCustomerBasket.ID.String()).Return(
			&invalidCustomerBasket,
			fmt.Errorf("could not found created item with id: %s", invalidCustomerBasket.ID))
		body, _ := json.Marshal(invalidCustomerBasket)

		w := httptest.NewRecorder()
		r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
		router.ServeHTTP(w, r)
		assert.Equal(t, http.StatusBadRequest, w.Code)
	})
}
