package controllers

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/google/uuid"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/basket.api/models"
	"github.com/jurabek/basket.api/repositories"
	"github.com/stretchr/testify/assert"
)

var items = []models.BasketItem{
	models.BasketItem{
		ID:           uuid.New(),
		FoodID:       uuid.New(),
		UnitPrice:    20,
		OldUnitPrice: 10,
		Quantity:     1,
		Picture:      "picture",
		FoodName:     "foodName",
	},
}

func TestBasketControllerGetShouldReturnOkWhenValidCustomerID(t *testing.T) {
	gin.SetMode(gin.TestMode)
	customerBasket := models.CustomerBasket{}
	var mockedBasketRepository = &repositories.MockBasketRepository{}

	mockedBasketRepository.On("GetBasket", "abcd").Return(&customerBasket, nil).Once()

	var controller = NewBasketController(mockedBasketRepository)

	router := gin.Default()
	basket := router.Group("basket")
	{
		basket.GET(":id", controller.Get)
	}

	w := httptest.NewRecorder()
	r, _ := http.NewRequest("GET", "/basket/abcd", nil)
	router.ServeHTTP(w, r)
	assert.Equal(t, http.StatusOK, w.Code)
}

func TestBasketControllerGetShouldReturnBadRequestWhenInValidCustomerID(t *testing.T) {
	gin.SetMode(gin.TestMode)
	var mockedBasketRepository = &repositories.MockBasketRepository{}

	customerBasket := models.CustomerBasket{}
	mockedBasketRepository.On("GetBasket", "invalid").Return(&customerBasket, fmt.Errorf("Not found item with id: %s", "invalid")).Once()
	var controller = NewBasketController(mockedBasketRepository)

	router := gin.Default()
	basket := router.Group("basket")
	{
		basket.GET(":id", controller.Get)
	}

	w := httptest.NewRecorder()
	r, _ := http.NewRequest("GET", "/basket/invalid", nil)
	router.ServeHTTP(w, r)
	assert.Equal(t, http.StatusBadRequest, w.Code)
}

func TestBasketControllerCreateShouldCreateItemAndReturnOk(t *testing.T) {
	gin.SetMode(gin.TestMode)

	customerBasket := models.CustomerBasket{
		CustomerID: uuid.New(),
		Items:      &items,
	}

	var mockedBasketRepository = &repositories.MockBasketRepository{}
	mockedBasketRepository.On("Update", &customerBasket).Return(nil)
	mockedBasketRepository.On("GetBasket", customerBasket.CustomerID.String()).Return(&customerBasket, nil)

	var controller = NewBasketController(mockedBasketRepository)

	router := gin.Default()
	basket := router.Group("basket")
	{
		basket.POST("", controller.Create)
	}
	body, _ := json.Marshal(customerBasket)

	w := httptest.NewRecorder()
	r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
	router.ServeHTTP(w, r)

	assert.Equal(t, http.StatusOK, w.Code)

	var result models.CustomerBasket
	bodyResult, _ := ioutil.ReadAll(w.Body)
	json.Unmarshal(bodyResult, &result)

	assert.Equal(t, result.CustomerID, customerBasket.CustomerID)
}

func TestBasketControllerCreateShouldNotCreateItemAndReturn_400(t *testing.T) {
	gin.SetMode(gin.TestMode)

	customerBasket := models.CustomerBasket{
		CustomerID: uuid.New(),
		Items:      &items,
	}

	var mockedBasketRepository = &repositories.MockBasketRepository{}
	mockedBasketRepository.On("Update", &customerBasket).Return(fmt.Errorf("Could not update item id: %s", customerBasket.CustomerID))
	var controller = NewBasketController(mockedBasketRepository)

	router := gin.Default()
	basket := router.Group("basket")
	{
		basket.POST("", controller.Create)
	}
	body, _ := json.Marshal(customerBasket)

	w := httptest.NewRecorder()
	r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
	router.ServeHTTP(w, r)

	assert.Equal(t, http.StatusBadRequest, w.Code)
}

func TestBasketControllerCreateShouldCreateItemAndWhenCouldNotFindCreatedItemReturn_400(t *testing.T) {
	gin.SetMode(gin.TestMode)

	customerBasket := models.CustomerBasket{
		CustomerID: uuid.New(),
		Items:      &items,
	}

	var mockedBasketRepository = &repositories.MockBasketRepository{}
	mockedBasketRepository.On("Update", &customerBasket).Return(nil)
	mockedBasketRepository.On("GetBasket", customerBasket.CustomerID.String()).Return(&customerBasket, fmt.Errorf("Could not found created item with id: %s", customerBasket.CustomerID))
	var controller = NewBasketController(mockedBasketRepository)

	router := gin.Default()
	basket := router.Group("basket")
	{
		basket.POST("", controller.Create)
	}
	body, _ := json.Marshal(customerBasket)

	w := httptest.NewRecorder()
	r, _ := http.NewRequest("POST", "/basket", bytes.NewBuffer(body))
	router.ServeHTTP(w, r)

	assert.Equal(t, http.StatusBadRequest, w.Code)
}
