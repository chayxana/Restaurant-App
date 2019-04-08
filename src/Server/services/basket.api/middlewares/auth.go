package middlewares

import (
	"fmt"
	"net/http"
	"strings"

	"github.com/dgrijalva/jwt-go"
	"github.com/gin-gonic/gin"
	"github.com/jurabek/basket.api/models"
)

func AuthMiddleware() gin.HandlerFunc {
	return func(c *gin.Context) {
		r := c.Request

		tokenHeader := r.Header.Get("Authorization") //Grab the token from the header

		if tokenHeader == "" { //Token is missing, returns with error code 403 Unauthorized
			httpError := models.NewHTTPError(http.StatusUnauthorized, fmt.Errorf("token is missing"))
			c.AbortWithStatusJSON(http.StatusUnauthorized, httpError)
		}

		splitted := strings.Split(tokenHeader, " ") //The token normally comes in format `Bearer {token-body}`, we check if the retrieved token matched this requirement
		if len(splitted) != 2 {
			httpError := models.NewHTTPError(http.StatusUnauthorized, fmt.Errorf("bearer token invalid format"))
			c.AbortWithStatusJSON(http.StatusUnauthorized, httpError)
		}

		bearerToken := splitted[1]

		toValidate := map[string]string{}
		toValidate["aud"] = "api://default"
		toValidate["cid"] = "{CLIENT_ID}"

		token, err := jwt.Parse(bearerToken, func(token *jwt.Token) (interface{}, error) {
			// Don't forget to validate the alg is what you expect:
			if _, ok := token.Method.(*jwt.SigningMethodHMAC); !ok {
				return nil, fmt.Errorf("Unexpected signing method: %v", token.Header["alg"])
			}

			// hmacSampleSecret is a []byte containing your secret, e.g. []byte("my_secret_key")
			return []byte("key"), nil
		})

		if claims, ok := token.Claims.(jwt.MapClaims); ok && token.Valid {
			fmt.Println(claims["foo"], claims["nbf"])
		} else {
			fmt.Println(err)
		}

		if err != nil {
			httpError := models.NewHTTPError(http.StatusUnauthorized, err)
			c.AbortWithStatusJSON(http.StatusUnauthorized, httpError)
		}
		c.Next()
	}
}
