package models

import "github.com/google/uuid"

// BasketItem items for users basket
type BasketItem struct {
	FoodID          int     `json:"food_id"`
	UnitPrice       float32 `json:"unit_price"`
	OldUnitPrice    float32 `json:"old_unit_price"`
	Quantity        int     `json:"quantity"`
	Picture         string  `json:"picture"`
	FoodName        string  `json:"food_name"`
	FoodDescription string  `json:"food_description"`
}

// CustomerBasket relationship user and basket items
type CustomerBasket struct {
	CustomerID uuid.UUID     `json:"customer_id"`
	Items      *[]BasketItem `json:"items"`
}
