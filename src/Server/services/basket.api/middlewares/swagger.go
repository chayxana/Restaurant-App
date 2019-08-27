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
		basePath, ok := os.LookupEnv("BASE_PATH")
		if ok {
			docs.SwaggerInfo.BasePath = basePath + "/api/v1/"
			fmt.Printf("Swagger base path: %s\r\n", docs.SwaggerInfo.BasePath)
		} else {
			docs.SwaggerInfo.BasePath = "/api/v1/"
		}
		if forwardedHost := c.Request.Header["X-Forwarded-Host"]; forwardedHost != nil {
			docs.SwaggerInfo.Host = forwardedHost[0]
			fmt.Printf("Swagger host: %s\r\n", docs.SwaggerInfo.Host)
		}
		c.Next()
	}
}
