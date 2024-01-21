package models

import "github.com/google/uuid"

// LineItem
type LineItem struct {
	ItemID             int                    `json:"item_id"`
	UnitPrice          float32                `json:"unit_price"`
	Quantity           int                    `json:"quantity"`
	Image              string                 `json:"img"`
	ProductName        string                 `json:"product_name"`
	ProductDescription string                 `json:"product_description"`
	Attributes         map[string]interface{} `json:"attributes"`
}

// Cart
type Cart struct {
	ID            uuid.UUID   `json:"id"`
	LineItems     *[]LineItem `json:"items"`
	UserID        string      `json:"user_id"`
	Total         float32     `json:"total"`
	Tax           float32     `json:"tax"`
	Discount      float32     `json:"discount"`
	Shipping      float32     `json:"shipping"`
	Currency      string      `json:"currency"`
	Status        string      `json:"status"`
	PaymentMethod string      `json:"payment_method"`
	PaymentID     string      `json:"payment_id"`
}
