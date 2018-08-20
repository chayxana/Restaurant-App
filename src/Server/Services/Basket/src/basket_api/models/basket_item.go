package models

import (
	"github.com/google/uuid"
)

type BasketItem struct {
	ID           uuid.UUID `json:"id"`
	FoodID       uuid.UUID `json:foodId`
	UnitPrice    float32   `json:"unitPrice"`
	OldUnitPrice float32   `json:"oldUnitPrice"`
	Quantity     int       `json:"quantity"`
	Picture      string    `json:picture`
}
