package middlewares

import (
	"os"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/cart-api/internal/docs"
	"github.com/rs/zerolog/log"
)

// RequestMiddleware changes swagger Info on runtime
func RequestMiddleware() gin.HandlerFunc {
	return func(c *gin.Context) {
		for key, val := range c.Request.Header {
			log.Printf("Header %s: %s", key, val)
		}
		log.Printf("Request: %s\r\n", c.Request.RequestURI)

		basePath, _ := os.LookupEnv("BASE_PATH")
		docs.SwaggerInfo.BasePath = basePath + "/api/v1/"
		log.Printf("Swagger base path: %s\r\n", docs.SwaggerInfo.BasePath)

		docs.SwaggerInfo.Host = "localhost"

		if forwardedHost := c.Request.Header["X-Forwarded-Host"]; forwardedHost != nil {
			docs.SwaggerInfo.Host = forwardedHost[0]
			log.Printf("Swagger host: %s\r\n", docs.SwaggerInfo.Host)
		}
		c.Next()
	}
}
