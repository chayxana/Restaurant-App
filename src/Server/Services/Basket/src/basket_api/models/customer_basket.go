package models

import "github.com/google/uuid"

type CustomerBasket struct {
	CustomerID uuid.UUID     `json:"customerId"`
	Items      *[]BasketItem `json:"items"`
}
