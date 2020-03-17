package repositories

import (
	"encoding/json"
	"fmt"

	"github.com/jurabek/basket.api/database"

	"github.com/gomodule/redigo/redis"
	"github.com/jurabek/basket.api/models"
)

// RedisBasketRepository implementation of BasketRepository
type RedisBasketRepository struct {
	Conn database.ConnectionProvider
}

// NewRedisBasketRepository creates new instance of repository
func NewRedisBasketRepository(conn database.ConnectionProvider) BasketRepository {
	return &RedisBasketRepository{Conn: conn}
}

// GetBasket returns CustomerBasket otherwise nill
func (r *RedisBasketRepository) GetBasket(customerID string) (*models.CustomerBasket, error) {

	conn := r.Conn.Get()
	defer conn.Close()

	var (
		result models.CustomerBasket
		data   []byte
	)
	data, err := redis.Bytes(conn.Do("GET", customerID))
	if err != nil {
		return nil, fmt.Errorf("error getting key %s: %v", customerID, err)
	}

	err = json.Unmarshal(data, &result)
	if err != nil {
		return nil, fmt.Errorf("error marshalling %v to %v", data, result)
	}

	return &result, err
}

// Update updates or creates new CustomerBasket
func (r *RedisBasketRepository) Update(item *models.CustomerBasket) error {
	value, err := json.Marshal(item)

	if err != nil {
		return fmt.Errorf("error marshalling %v", item)
	}

	conn := r.Conn.Get()
	defer conn.Close()

	_, err = conn.Do("SET", item.CustomerID, value)
	if err != nil {
		v := string(value)
		if len(v) > 15 {
			v = v[0:12] + "..."
		}
		return fmt.Errorf("error setting key %s to %s: %v", item.CustomerID, v, err)
	}
	return err
}

// Delete removes existing CustomerBasket
func (r *RedisBasketRepository) Delete(id string) error {
	conn := r.Conn.Get()
	defer conn.Close()

	_, err := conn.Do("DEL", id)
	return err
}

func (r *RedisBasketRepository) getKeys(pattern string) ([]string, error) {

	conn := r.Conn.Get()
	defer conn.Close()

	iter := 0
	keys := []string{}
	for {
		arr, err := redis.Values(conn.Do("SCAN", iter, "MATCH", pattern))
		if err != nil {
			return keys, fmt.Errorf("error retrieving '%s' keys", pattern)
		}

		iter, _ = redis.Int(arr[0], nil)
		k, _ := redis.Strings(arr[1], nil)
		keys = append(keys, k...)

		if iter == 0 {
			break
		}
	}

	return keys, nil
}

func (r *RedisBasketRepository) getAll() ([]string, error) {
	return nil, nil
}
