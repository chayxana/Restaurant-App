package repositories

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"

	"github.com/jurabek/cart-api/internal/models"
	"github.com/redis/go-redis/v9"
)

// RedisBasketRepository implementation of BasketRepository
type RedisBasketRepository struct {
	client *redis.Client
}

// NewRedisBasketRepository creates new instance of repository
func NewRedisBasketRepository(client *redis.Client) *RedisBasketRepository {
	return &RedisBasketRepository{client: client}
}

var ErrCartNotFound = errors.New("cart not found");

// Get returns cart otherwise nill
func (r *RedisBasketRepository) Get(ctx context.Context, cartID string) (*models.Cart, error) {
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

// Update updates or creates new CustomerBasket
func (r *RedisBasketRepository) Update(ctx context.Context, item *models.Cart) error {
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

// Delete removes existing CustomerBasket
func (r *RedisBasketRepository) Delete(ctx context.Context, id string) error {
	return r.client.Del(ctx, id).Err()
}
