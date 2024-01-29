package config

import (
	"os"
)

// Configuration injects all environment variables into object
type Configuration struct {
	ServerPort  string
	RedisHost   string
	KafkaBroker string
	OrdersTopic string
}

// Init initializes environment variables into config
func Init() *Configuration {
	_ = os.Getenv("PORT")
	var cfg Configuration
	if redisHost, ok := os.LookupEnv("REDIS_HOST"); ok {
		cfg.RedisHost = redisHost
	}

	if kafkaBroker, ok := os.LookupEnv("KAFKA_BROKER"); ok {
		cfg.KafkaBroker = kafkaBroker
	}

	if ordersTopic, ok := os.LookupEnv("ORDERS_TOPIC"); ok {
		cfg.OrdersTopic = ordersTopic
	}

	return &cfg
}
