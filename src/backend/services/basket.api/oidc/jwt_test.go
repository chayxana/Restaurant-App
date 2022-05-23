package oidc

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
	"testing"
	"time"

	"github.com/lestrrat-go/jwx/jwk"

	"github.com/golang-jwt/jwt"
	"github.com/jurabek/basket.api/mock"
	"github.com/stretchr/testify/assert"
)

const (
	privateKey = `
-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAz/XOr4wUl0baOL4eMkXG+utj8H0AwIiuG3jSpBPh8kfJe55S
PMQ3bPJQfCums3kzmKXS0bYYcIWlKwr8uCcWVqmavaovKMiSYSiWoHOQmHmOJt+4
UEW2Pe/EoJUFEmV5IgRNgURwOOTHXmWoeaQ4rVADLqnM/XDm6SJO8aGMq6J/9Wof
c37DgRgeMZ6HLkIlCpUu95LRx28rBq/qWwoS94u63IHvDg5Wj1BtjLt2Dv77+DJS
1OPzASfwzps8pQE8wBxZtwsNWB8Kl5hdMEttwD7/Yv/v1aWGnvDUt7KpeVqxyZHF
3ir9j2YMhw3EvGwJWSYCKzj7G89ESp37Y1BaWwIDAQABAoIBAQCbyXF/MnoOZWZ+
kXW/hWQtfn8MnigdE4cST23EupxNypdWQuEqYnd+5vuCOZUU59vOI5MNxNMEICn+
V6nALo/edgnUwZO4gqCdpjFIm6obfxwNZRUHFWITffWXsmrtQBUBdaW2C9Xh2Qi9
X3xZO7u1obXwlAVbauOgjDIFc1cVDzl90y4zgdf/zu9f8JxwY92oihVWeOPNV9Vh
sU/H8UtQ2M4a/SB2VX5CmgUNVS8xk85x6OZ2YA5wvqvWHrG5GpDv+BhtoYb/1g9s
orjkABdKxmia78HXqPEioUWg4v1S7ldg8IqE0Ifo9ir+XJR+xYAVZuqJUSXeYGfE
ZGQwAFGRAoGBAPP4OYN4zNwAVO5MqBf4QWQXL55zJR4aLKXJ1hA1CDspLmICYG20
rrcSuSz3aN8zbrQhIi6Kq4ugo73u/M37d8yQW9ts60PRqJennwTqtPi79o/2wUyt
lXEEpDSuBDGZp+iw2boj1fNXm4JhuoG/UbzfggPmfTlyEMLmXHeFkyG5AoGBANo3
A4vmVneusj6e1aJNKzl+pJS476c5QJEGULzPoBkcOvgK/7QLz1KFnY7qxk8tRIlS
4cYHM9DtczkPESTtP9JG00ytF+UCIxXXaIXo1vJwXOuqAH0ALd1iLhagrSMyqoeQ
49QdWQkazdSEB/InTX/eXI/PoWmYO5usVAiXSPazAoGBALmHNMWEIdXT8sJdTR8d
TSz+bNoEGl/v67AP81fT3HSQ9pLV19rVol/aPzOw2fGSvUcCQ2o9TgMaoCqaCWnj
J9FKnnAZPjgOwjTB5a3phsH4vqHwNkNHZfPSYcUl6E6H2SadBpYFFni9UKcwBpMQ
mOoW1lp89xGuaoysffjufVsRAoGASWbqBXw0p8uW36OUHyUwHgXwnKpcyvZiNqZW
MDzzEJ9DYr5oZwr18T9K0ZE9pdKHVF4R3gf8MRI+iPn9fVtk9XmniApNmFYQsT8l
RK4e56xORWOJCIMv6mElOa1QsB9R54ogaPB6S6q9g/fpqFX6JsIIhsOD+z4fBu1/
0uf4visCgYAO5TKj/47NUexG7bdM5Poh8voQXJGaxbXeLVuWVms7vVsmv99CGZ0A
rRJogpUQHQyBgz3ZXAf1z3qeNJLGQmifGPh5XwLTBTVMs+W2ByamNp9rcNlWCEwW
Uwkvxpg2lLoadEqJGO/8ayulmHtQJ4CYyVD1ER57DUl5UILcG9PUXw==
-----END RSA PRIVATE KEY-----
`
	publicKey = `
-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAz/XOr4wUl0baOL4eMkXG
+utj8H0AwIiuG3jSpBPh8kfJe55SPMQ3bPJQfCums3kzmKXS0bYYcIWlKwr8uCcW
VqmavaovKMiSYSiWoHOQmHmOJt+4UEW2Pe/EoJUFEmV5IgRNgURwOOTHXmWoeaQ4
rVADLqnM/XDm6SJO8aGMq6J/9Wofc37DgRgeMZ6HLkIlCpUu95LRx28rBq/qWwoS
94u63IHvDg5Wj1BtjLt2Dv77+DJS1OPzASfwzps8pQE8wBxZtwsNWB8Kl5hdMEtt
wD7/Yv/v1aWGnvDUt7KpeVqxyZHF3ir9j2YMhw3EvGwJWSYCKzj7G89ESp37Y1Ba
WwIDAQAB
-----END PUBLIC KEY-----`
)

func getJWKSResponse(skipKid bool) []byte {
	verifyKey, _ := jwt.ParseRSAPublicKeyFromPEM([]byte(publicKey)) // openssl rsa -in app.rsa -pubout > app.rsa.pub
	key, _ := jwk.New(verifyKey)
	publicKey, _ := key.(*jwk.RSAPublicKey)
	if !skipKid {
		_ = publicKey.Set("kid", "1234567890kid")
	}

	set := jwk.Set{
		Keys: []jwk.Key{
			publicKey,
		},
	}

	keysResponse, _ := json.Marshal(set)
	return keysResponse
}

func createTestJwtToken(audience string, issuer string) (string, error) {
	claims := jwt.StandardClaims{
		ExpiresAt: time.Now().Add(5 * time.Minute).Unix(),
		Audience:  audience,
		Issuer:    issuer,
	}
	signKey, _ := jwt.ParseRSAPrivateKeyFromPEM([]byte(privateKey)) // openssl genrsa -out app.rsa keysize

	token := jwt.NewWithClaims(jwt.SigningMethodRS256, claims)
	token.Header["kid"] = "1234567890kid"
	tokenString, err := token.SignedString(signKey)
	if err != nil {
		return "", err
	}

	return tokenString, nil
}

func TestJwt(t *testing.T) {
	token, _ := createTestJwtToken("menu-api", "restaurant-api")
	response := http.Response{
		StatusCode: 200,
		Body:       ioutil.NopCloser(bytes.NewBuffer(getJWKSResponse(false))),
		Header:     make(http.Header),
	}

	jwkMockHTTPClient := mock.JWKMockHTTPClient{}
	jwkMockHTTPClient.On("Get", "http://localhost/.well-known/openid-configuration/jwks").Return(&response, nil)

	jwtVerifier := JwtTokenVerifier{
		HTTPClient: &jwkMockHTTPClient,
		Authority:  "http://localhost",
	}

	t.Run("given valid token ValidateToken should be valid", func(t *testing.T) {
		result, err := jwtVerifier.ValidateToken(token)
		assert.True(t, result)
		assert.Nil(t, err)
		assert.NotEmpty(t, token)
	})

	t.Run("given invalid signing method validate should not pass", func(t *testing.T) {
		invalidToken := jwt.New(jwt.SigningMethodHS256)
		tokenString, _ := invalidToken.SignedString([]byte("123"))

		result, err := jwtVerifier.ValidateToken(tokenString)

		assert.Empty(t, result)
		assert.NotEmpty(t, err)
		assert.Equal(t, "unexpected signing method: HS256", err.Error())
	})

	t.Run("given wrong jwks url validate should not pass", func(t *testing.T) {
		jwkMockHTTPClient.On("Get", "http://wrong/.well-known/openid-configuration/jwks").Return(&response, fmt.Errorf("connection error")).Once()
		jwtVerifierWithWrongJwksURL := JwtTokenVerifier{
			HTTPClient: &jwkMockHTTPClient,
			Authority:  "http://wrong",
		}

		result, err := jwtVerifierWithWrongJwksURL.ValidateToken(token)

		assert.Empty(t, result)
		assert.NotEmpty(t, err)
		assert.Equal(t, "failed to fetch remote JWK: connection error", err.Error())
	})

	t.Run("given empty kid response validate should not find key", func(t *testing.T) {
		wrongResponse := http.Response{
			StatusCode: 200,
			Body:       ioutil.NopCloser(bytes.NewBuffer(getJWKSResponse(true))),
			Header:     make(http.Header),
		}

		mock := mock.JWKMockHTTPClient{}
		mock.On("Get", "http://localhost/.well-known/openid-configuration/jwks").Return(&wrongResponse, nil)

		newJwtVerifier := JwtTokenVerifier{
			HTTPClient: &mock,
			Authority:  "http://localhost",
		}

		result, err := newJwtVerifier.ValidateToken(token)

		assert.Empty(t, result)
		assert.NotEmpty(t, err)
		assert.Equal(t, "unable to find key", err.Error())
	})

	t.Run("claims test", func(t *testing.T) {
		claimsToValidate := map[string]interface{}{}
		claimsToValidate[""] = nil
	})
}
