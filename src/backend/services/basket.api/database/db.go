package database

import (
	"fmt"
	"os"
	"os/signal"
	"syscall"
	"time"

	"github.com/gomodule/redigo/redis"
)

// ConnectionProvider abstracts redis connection
type ConnectionProvider interface {
	Get() redis.Conn
}

// RedisConnectionProvider provides redis connection from current Redis Pool
type RedisConnectionProvider struct {
	Pool *redis.Pool
}

// Get return redis.Pool connection
func (r *RedisConnectionProvider) Get() redis.Conn {
	return r.Pool.Get()
}

// NewRedisPool creates new pool
func NewRedisPool(server string) *redis.Pool {

	return &redis.Pool{

		MaxIdle:     3,
		IdleTimeout: 240 * time.Second,

		Dial: func() (redis.Conn, error) {
			c, err := redis.Dial("tcp", server)
			if err != nil {
				return nil, err
			}
			return c, err
		},

		TestOnBorrow: func(c redis.Conn, t time.Time) error {
			_, err := c.Do("PING")
			return err
		},
	}
}

// CleanupHook should be clean signals
func CleanupHook(pool *redis.Pool) {

	c := make(chan os.Signal, 1)
	signal.Notify(c, os.Interrupt)
	signal.Notify(c, syscall.SIGTERM)
	signal.Notify(c, syscall.SIGKILL)
	go func() {
		<-c
		pool.Close()
		os.Exit(0)
	}()
}

// HealthCheck checks redis server
func HealthCheck(pool *redis.Pool) error {

	conn := pool.Get()
	defer conn.Close()

	_, err := redis.String(conn.Do("PING"))
	if err != nil {
		return fmt.Errorf("cannot 'PING' db: %v", err)
	}
	return nil
}
