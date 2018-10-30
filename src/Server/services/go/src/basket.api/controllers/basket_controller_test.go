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

	"basket.api/models"
	"basket.api/repositories"
	"github.com/gin-gonic/gin"
	"github.com/stretchr/testify/assert"
)

// var mockedBasketRepository = &repositories.BasketRepositoryMock{
// 	DeleteFunc: func(id string) error {
// 		if id == "wrongId" {
// 			return fmt.Errorf("Error removing item")
// 		}
// 		fmt.Println("Item removed!")
// 		return nil
// 	},
//
// }

func TestBasketControllerGetShouldReturnOkWhenValidCustomerID(t *testing.T) {
	gin.SetMode(gin.TestMode)
	customerBasket := models.CustomerBasket{}
	var mockedBasketRepository = &repositories.BasketRepositoryMock{
		GetBasketFunc: func(customerID string) (*models.CustomerBasket, error) {
			if customerID == "abcd" {
				return &customerBasket, nil
			}
			return nil, fmt.Errorf("Not found item with id: %s", customerID)
		},
	}
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
	customerBasket := models.CustomerBasket{}
	var mockedBasketRepository = &repositories.BasketRepositoryMock{
		GetBasketFunc: func(customerID string) (*models.CustomerBasket, error) {
			if customerID == "abcd" {
				return &customerBasket, nil
			}
			return nil, fmt.Errorf("Not found item with id: %s", customerID)
		},
	}
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
	items := []models.BasketItem{
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

	customerBasket := models.CustomerBasket{
		CustomerID: uuid.New(),
		Items:      &items,
	}
	var mockedBasketRepository = &repositories.BasketRepositoryMock{
		UpdateFunc: func(item *models.CustomerBasket) error {
			if item.CustomerID == customerBasket.CustomerID {
				fmt.Print("Updated")
				return nil
			}

			return fmt.Errorf("Could not update item")
		},
		GetBasketFunc: func(customerID string) (*models.CustomerBasket, error) {
			if customerID == customerBasket.CustomerID.String() {
				return &customerBasket, nil
			}
			return nil, fmt.Errorf("Not found item with id: %s", customerID)
		},
	}

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
