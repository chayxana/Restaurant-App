package repositories

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"

	"github.com/jurabek/cart-api/internal/models"
	"github.com/redis/go-redis/v9"
)

// CartRepository implementation of redis repositor
type CartRepository struct {
	client *redis.Client
}

// NewCartRepository creates new instance of repository
func NewCartRepository(client *redis.Client) *CartRepository {
	return &CartRepository{client: client}
}

var ErrCartNotFound = errors.New("cart not found");

// Get returns cart otherwise nill
func (r *CartRepository) Get(ctx context.Context, cartID string) (*models.Cart, error) {
	var (
		result models.Cart
		data   []byte
	)
	data, err := r.client.Get(ctx, cartID).Bytes()
	if err != nil {
		if(err == redis.Nil){
			return nil, ErrCartNotFound
		}
		return nil, fmt.Errorf("error getting key %s: %v", cartID, err)
	}

	err = json.Unmarshal(data, &result)
	if err != nil {
		return nil, fmt.Errorf("error marshalling %v to %v", data, result)
	}

	return &result, err
}

func calculateTotalPrice(items []models.LineItem) float64 {
    var totalPrice float64
    for _, item := range items {
        totalPrice += float64(item.UnitPrice) * float64(item.Quantity)
    }
    return totalPrice
}

func (r *CartRepository) SetItem(ctx context.Context, cartID string, newItem models.LineItem) error {
    // Fetch the existing cart
    existingCart, err := r.Get(ctx, cartID)
    if err != nil {
        return err
    }

	foundIndex := -1
	for i, item := range existingCart.LineItems {
        if item.ItemID == newItem.ItemID {
			foundIndex = i; 
			break
        }
    }
	if foundIndex > -1 {
		existingCart.LineItems[foundIndex].Quantity += newItem.Quantity
	} else {
		existingCart.LineItems = append(existingCart.LineItems, newItem)
	}
	existingCart.Total = calculateTotalPrice(existingCart.LineItems)
    return r.Update(ctx, existingCart)
}

func (r *CartRepository) DeleteItem(ctx context.Context, cartID string, itemID int) error {
    // Fetch the existing cart
    existingCart, err := r.Get(ctx, cartID)
    if err != nil {
        return err
    }

    // Update the cart in Redis
    updatedItems := []models.LineItem{}
    for _, bi := range existingCart.LineItems {
        if bi.ItemID != itemID {
            updatedItems = append(updatedItems, bi)
        }
    }
    existingCart.LineItems = updatedItems
	existingCart.Total = calculateTotalPrice(existingCart.LineItems)
    return r.Update(ctx, existingCart)
}

// Update updates or creates new Cart
func (r *CartRepository) Update(ctx context.Context, item *models.Cart) error {
	value, err := json.Marshal(item)

	if err != nil {
		return fmt.Errorf("error marshalling %v", item)
	}

	err = r.client.Set(ctx, item.ID.String(), value, 0).Err()
	if err != nil {
		v := string(value)
		if len(v) > 15 {
			v = v[0:12] + "..."
		}
		return fmt.Errorf("error setting key %s to %s: %v", item.ID, v, err)
	}
	return err
}

// Delete removes existing Cart
func (r *CartRepository) Delete(ctx context.Context, id string) error {
	return r.client.Del(ctx, id).Err()
}