package config

import (
	"os"
)

// Configuration injects all environment variables into object
type Configuration struct {
	ServerPort string
	RedisHost  string
}

// Init initializes environment variables into config
func (c *Configuration) Init() {
	_ = os.Getenv("PORT")
	_, _ = os.LookupEnv("REDIS_HOST")
}
