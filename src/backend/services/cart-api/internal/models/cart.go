package models

import "github.com/google/uuid"

type CreateCartReq struct {
	LineItems *[]LineItem `json:"items,omitempty"`
	UserID    *string     `json:"user_id,omitempty"`
}

type UpdateCartReq struct {
	LineItems *[]LineItem `json:"items,omitempty"`
	UserID    *string     `json:"user_id,omitempty"`
	Status    *string     `json:"status,omitempty"`
	Discount  *float32    `json:"discount,omitempty"`
}

func MapUpdateCartReqToCart(existingCart *Cart, req UpdateCartReq) *Cart {
	if req.LineItems == nil {
		req.LineItems = &existingCart.LineItems
	}
	if req.UserID == nil {
		*req.UserID = *existingCart.UserID
	}

	cart := &Cart{
		LineItems: *req.LineItems,
		UserID:    req.UserID,
		ID:        existingCart.ID,
		Status:    MapStatusStringToStatus(req.Status),
		Discount:  req.Discount,
	}
	return cart
}

func MapCreateCartReqToCart(req CreateCartReq) *Cart {
	if req.LineItems == nil {
		req.LineItems = &[]LineItem{}
	}
	if req.UserID == nil {
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

// Status Enum
type Status int

const (
	CartStatusNew Status = iota + 1
	CartStatusProcessing
	CartStatusCompleted
	CartStatusCancelled
)

func (s Status) String() string {
	return [...]string{"new", "processing", "completed", "cancelled"}[s]
}

func MapStatusStringToStatus(status *string) Status {
	if status == nil {
		return CartStatusNew
	}

	switch *status {
	case "new":
		return CartStatusNew
	case "processing":
		return CartStatusProcessing
	case "completed":
		return CartStatusCompleted
	case "cancelled":
		return CartStatusCancelled
	default:
		return CartStatusNew
	}
}

// Cart
type Cart struct {
	ID        uuid.UUID  `json:"id"`
	LineItems []LineItem `json:"items"`
	Total     float64    `json:"total"`

	UserID         *string  `json:"user_id,omitempty"`
	Discount       *float32 `json:"discount,omitempty"`
	Tax            *float32 `json:"tax,omitempty"`
	Shipping       *float32 `json:"shipping,omitempty"`
	ShippingMethod *string  `json:"shipping_method,omitempty"`
	Currency       *string  `json:"currency,omitempty"`
	Status         Status   `json:"status,omitempty"`
	OrderID        *string  `json:"order_id,omitempty"`
	TransactionID  *string  `json:"transaction_id,omitempty"`
}
