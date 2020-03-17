package middlewares

import (
	"fmt"
	"os"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/basket.api/docs"
)

// RequestMiddleware changes swagger Info on runtime
func RequestMiddleware() gin.HandlerFunc {
	return func(c *gin.Context) {
		for key, val := range c.Request.Header {
			fmt.Printf("Header %s: %s", key, val)
		}
		fmt.Printf("Request: %s\r\n", c.Request.RequestURI)

		basePath, _ := os.LookupEnv("BASE_PATH")
		docs.SwaggerInfo.BasePath = basePath + "/api/v1/"
		fmt.Printf("Swagger base path: %s\r\n", docs.SwaggerInfo.BasePath)

		docs.SwaggerInfo.Host = "localhost"

		if forwardedHost := c.Request.Header["X-Forwarded-Host"]; forwardedHost != nil {
			docs.SwaggerInfo.Host = forwardedHost[0]
			fmt.Printf("Swagger host: %s\r\n", docs.SwaggerInfo.Host)
		}
		c.Next()
	}
}
