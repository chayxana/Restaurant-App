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
	"github.com/jurabek/basket.api/internal/models"
	"github.com/jurabek/basket.api/internal/repositories"
	"github.com/stretchr/testify/assert"
)

var items = []models.BasketItem{{
	FoodID:       1,
	UnitPrice:    20,
	OldUnitPrice: 10,
	Quantity:     1,
	Picture:      "picture",
	FoodName:     "foodName",
},
}

func TestBasketController(t *testing.T) {
	ctx := context.TODO()

	gin.SetMode(gin.TestMode)
	customerBasket := models.CustomerBasket{
		CustomerID: uuid.New(),
		Items:      &items,
	}

	var mockedBasketRepository = &repositories.BasketRepositoryMock{}

	mockedBasketRepository.On("Get", ctx, "abcd").Return(&customerBasket, nil).Once()
	mockedBasketRepository.On("Get", ctx, "invalid").Return(&customerBasket, fmt.Errorf("Not found item with id: %s", "invalid")).Once()
	mockedBasketRepository.On("Update", ctx, &customerBasket).Return(nil)
	var controller = NewBasketHandler(mockedBasketRepository)

	router := gin.Default()
	basket := router.Group("basket")
	{
		basket.GET(":id", controller.Get)
		basket.POST("", controller.Create)
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
		mockedBasketRepository.On("Get", ctx, customerBasket.CustomerID.String()).Return(&customerBasket, nil)

		body, _ := json.Marshal(customerBasket)

		w := httptest.NewRecorder()
		r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
		router.ServeHTTP(w, r)

		assert.Equal(t, http.StatusOK, w.Code)

		var result models.CustomerBasket
		bodyResult, _ := ioutil.ReadAll(w.Body)
		_ = json.Unmarshal(bodyResult, &result)

		assert.Equal(t, result.CustomerID, customerBasket.CustomerID)
	})

	t.Run("Create should not create item and return code 400", func(t *testing.T) {
		invalidCustomerBasket := models.CustomerBasket{
			CustomerID: uuid.New(),
		}
		mockedBasketRepository.On("Update", ctx, &invalidCustomerBasket).Return(fmt.Errorf("could not update item id: %s", customerBasket.CustomerID))

		body, _ := json.Marshal(invalidCustomerBasket)

		w := httptest.NewRecorder()
		r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
		router.ServeHTTP(w, r)
		assert.Equal(t, http.StatusBadRequest, w.Code)
	})

	t.Run("Create should create item and when could not find created item return code 400", func(t *testing.T) {
		invalidCustomerBasket := models.CustomerBasket{
			CustomerID: uuid.New(),
		}
		mockedBasketRepository.On("Update", ctx, &invalidCustomerBasket).Return(nil)
		mockedBasketRepository.On("Get", ctx, invalidCustomerBasket.CustomerID.String()).Return(
			&invalidCustomerBasket,
			fmt.Errorf("could not found created item with id: %s", invalidCustomerBasket.CustomerID))
		body, _ := json.Marshal(invalidCustomerBasket)

		w := httptest.NewRecorder()
		r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
		router.ServeHTTP(w, r)
		assert.Equal(t, http.StatusBadRequest, w.Code)
	})
}
