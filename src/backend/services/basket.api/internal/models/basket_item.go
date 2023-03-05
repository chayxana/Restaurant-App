package models

import (
	"github.com/google/uuid"
)

// BasketItem items for users basket
type BasketItem struct {
	FoodID       uuid.UUID `json:"food_id"`
	UnitPrice    float32   `json:"unit_price"`
	OldUnitPrice float32   `json:"old_unit_price"`
	Quantity     int       `json:"quantity"`
	Picture      string    `json:"picture"`
	FoodName     string    `json:"food_name"`
}
