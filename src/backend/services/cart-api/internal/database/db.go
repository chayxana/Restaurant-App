package database

import (
	"context"
	"fmt"

	"github.com/redis/go-redis/v9"
)

// HealthCheck checks redis server
func HealthCheck(ctx context.Context, rdb *redis.Client) error {
	conn := rdb.Conn()
	defer conn.Close()

	err := conn.Ping(ctx).Err()
	if err != nil {
		return fmt.Errorf("cannot 'PING' db: %v", err)
	}
	return nil
}
