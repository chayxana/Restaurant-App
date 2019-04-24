package config

// Configuration injects all environment variables into object
type Configuration struct {
	ServerPort             string
	RedisHost              string
	EurekaClientServiceURL string
	IdentityServerURL      string
}

// Init initializes environment variables into config
func (c *Configuration) Init() {

}
