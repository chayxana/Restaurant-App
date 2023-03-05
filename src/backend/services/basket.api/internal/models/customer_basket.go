package models

import "github.com/google/uuid"

// CustomerBasket relationship user and basket items
type CustomerBasket struct {
	CustomerID uuid.UUID     `json:"customer_id"`
	Items      *[]BasketItem `json:"items"`
}
