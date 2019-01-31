package main

import (
	"fmt"
	"os"

	"github.com/jurabek/basket.api/controllers"
	redisConfig "github.com/jurabek/basket.api/redis"
	"github.com/jurabek/basket.api/repositories"

	"github.com/gin-gonic/gin"
	"github.com/gomodule/redigo/redis" // swagger embed files
)

// @title Basket API
// @version 1.0
// @description This is a rest api for basket which saves items to redis server
// @termsOfService http://swagger.io/terms/

// @contact.name API Support
// @contact.url http://www.swagger.io/support
// @contact.email support@swagger.io

// @license.name Apache 2.0
// @license.url http://www.apache.org/licenses/LICENSE-2.0.html

// @host localhost:5000
// @BasePath /api/
func main() {

	router := gin.Default()
	redisPool, err := initRedis()

	if err != nil {
		fmt.Print(err)
	}

	basketRepository := repositories.NewRedisBasketRepository(redisPool)
	controller := controllers.NewBasketController(basketRepository)

	api := router.Group("/api")
	{
		basket := api.Group("basket")
		{
			basket.GET(":id", controller.Get)
			basket.POST("", controller.Create)
		}
	}
	router.Run()
}

func initRedis() (*redis.Pool, error) {
	redisHost := os.Getenv("REDIS_HOST")
	if redisHost == "" {
		redisHost = ":6379"
	}
	pool := redisConfig.NewRedisPool(redisHost)
	redisConfig.CleanupHook(pool)

	err := redisConfig.HealthCheck(pool)

	return pool, err
}
