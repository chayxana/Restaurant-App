package config

import (
	"os"
)

// Configuration injects all environment variables into object
type Configuration struct {
	ServerPort    string
	RedisHost     string
	KafkaBroker   string
	CheckoutTopic string
}

// Init initializes environment variables into config
func (c *Configuration) Init() *Configuration {
	_ = os.Getenv("PORT")
	var cfg Configuration
	if redisHost, ok := os.LookupEnv("REDIS_HOST"); ok {
		cfg.RedisHost = redisHost
	}

	if kafkaBroker, ok := os.LookupEnv("KAFKA_BROKER"); ok {
		cfg.KafkaBroker = kafkaBroker
	}

	if checkoutTopic, ok := os.LookupEnv("CHECKOUT_TOPIC"); ok {
		cfg.CheckoutTopic = checkoutTopic
	}

	return &cfg
}
