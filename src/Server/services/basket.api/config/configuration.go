package config

import (
	"os"
)

// Configuration injects all environment variables into object
type Configuration struct {
	ServerPort             string
	RedisHost              string
	EurekaClientServiceURL string
	IdentityServerURL      string
}

// Init initializes environment variables into config
func (c *Configuration) Init() {
	_ = os.Getenv("PORT")
	_, _ = os.LookupEnv("REDIS_HOST")
	_, _ = os.LookupEnv("EUREKA_CLIENT_URL")
	_, _ = os.LookupEnv("IDENTITY_SERVER_URL")
}
