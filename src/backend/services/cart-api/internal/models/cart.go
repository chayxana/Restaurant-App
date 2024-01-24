package models

import "github.com/google/uuid"

type CreateCartReq struct {
	LineItems *[]LineItem `json:"items,omitempty"`
	UserID    *string     `json:"user_id,omitempty"`
}

func MapCreateCartReqToCart(req CreateCartReq) *Cart {
	if req.LineItems == nil {
		req.LineItems = &[]LineItem{}
	}
	if req.UserID == nil || *req.UserID == "" {
		*req.UserID = "anonymous"
	}
	cart := &Cart{
		LineItems: *req.LineItems,
		UserID:    req.UserID,
		ID:        uuid.New(),
	}
	return cart
}

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
	ID        uuid.UUID  `json:"id"`
	LineItems []LineItem `json:"items"`
	Total     float32    `json:"total"`

	UserID         *string  `json:"user_id,omitempty"`
	Discount       *float32 `json:"discount,omitempty"`
	Tax            *float32 `json:"tax,omitempty"`
	Shipping       *float32 `json:"shipping,omitempty"`
	ShippingMethod *string  `json:"shipping_method,omitempty"`
	Currency       *string  `json:"currency,omitempty"`
	Status         *string  `json:"status,omitempty"`
}
