package main

import (
	"fmt"
	"net/http"
	"os"
	"os/signal"
	"syscall"
	"time"

	"github.com/jurabek/basket.api/database"
	"github.com/jurabek/basket.api/docs"
	"github.com/jurabek/basket.api/handlers"

	"github.com/jurabek/basket.api/middlewares"

	"github.com/jurabek/basket.api/repositories"

	"github.com/gin-gonic/gin"
	"github.com/gomodule/redigo/redis" // swagger embed files

	// docs is generated by Swag CLI, you have to import it.
	ginSwagger "github.com/Jurabek/gin-swagger"   // gin-swagger middleware
	"github.com/Jurabek/gin-swagger/swaggerFiles" // swagger embed files
)

var (
	GitCommit string
	Version   string
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

// @securitydefinitions.oauth2.implicit OAuth
// @authorizationUrl {{.AuthUrl}}
// @scope.basket-api Access to basket-api
func main() {
	gin.SetMode(gin.DebugMode)

	handleSigterm()
	router := gin.Default()
	router.Use(middlewares.RequestMiddleware())

	redisPool, err := initRedis()

	if err != nil {
		fmt.Print(err)
	}

	connectionProvider := database.RedisConnectionProvider{
		Pool: redisPool,
	}

	basketRepository := repositories.NewRedisBasketRepository(&connectionProvider)
	controller := handlers.NewBasketHandler(basketRepository)

	auth := middlewares.CreateAuth()
	router.Use(auth.AuthMiddleware())
	basePath, _ := os.LookupEnv("BASE_PATH")

	api := router.Group(basePath + "/api/v1/")
	{
		basket := api.Group("items")
		{
			basket.GET(":id", controller.Get)
			basket.POST("", controller.Create)
			basket.DELETE(":id", controller.Delete)
		}
	}
	authURL, _ := os.LookupEnv("AUTH_URL")
	authorizeURL := authURL + "/connect/authorize"
	fmt.Fprintf(os.Stderr, "[DEBUG] Using Authorize URL: %s\r\n", authorizeURL)
	docs.OverrideAuthURL(authorizeURL)

	// Home page should be redirected to swagger page
	router.GET(basePath+"/", func(c *gin.Context) {
		c.Redirect(http.StatusMovedPermanently, basePath+"/swagger/index.html")
	})

	router.GET(basePath+"/swagger/*any", ginSwagger.WrapHandler(swaggerFiles.Handler,
		func(c *ginSwagger.Config) {
			c.URL = basePath + "/swagger/doc.json"
		}))

	_ = router.Run()
}

func handleSigterm() {
	c := make(chan os.Signal, 1)
	signal.Notify(c, syscall.SIGTERM, os.Kill, os.Interrupt)
	go func() {
		<-c
		time.Sleep(10 * time.Second)
		os.Exit(0)
	}()
}

func initRedis() (*redis.Pool, error) {
	redisHost := os.Getenv("REDIS_HOST")
	if redisHost == "" {
		redisHost = ":6379"
	}
	pool := database.NewRedisPool(redisHost)
	database.CleanupHook(pool)

	err := database.HealthCheck(pool)

	return pool, err
}
