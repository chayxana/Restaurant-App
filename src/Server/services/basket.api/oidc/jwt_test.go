package oidc

import (
	"bytes"
	"crypto/rsa"
	"github.com/gin-gonic/gin/json"
	"github.com/lestrrat-go/jwx/jwk"
	"io/ioutil"
	"net/http"
	"testing"
	"time"

	"github.com/jurabek/basket.api/mock"
	"github.com/patrickmn/go-cache"

	"github.com/dgrijalva/jwt-go"
	"github.com/stretchr/testify/assert"
)

var (
	verifyKey     *rsa.PublicKey
	signKey       *rsa.PrivateKey
	keysResponse  []byte
)

func createTestJwtToken(audience string, issuer string) (string, error) {
	expirationTime := time.Now().Add(5 * time.Minute)
	claims := jwt.StandardClaims{
		ExpiresAt: expirationTime.Unix(),
		Audience:  audience,
		Issuer:    issuer,
	}
	signKey, _ = jwt.ParseRSAPrivateKeyFromPEM([]byte(PrivateKey)) // openssl genrsa -out app.rsa keysize
	verifyKey, _ = jwt.ParseRSAPublicKeyFromPEM([]byte(PublicKey))  // openssl rsa -in app.rsa -pubout > app.rsa.pub

	key, _ := jwk.New(verifyKey)
	publicKey, _ := key.(*jwk.RSAPublicKey)
	_ = publicKey.Set("kid", "1234567890kid")

	set := jwk.Set{
		Keys: []jwk.Key{
			publicKey,
		},
	}

	keysResponse, _ = json.Marshal(set)

	token := jwt.NewWithClaims(jwt.SigningMethodRS256, claims)
	token.Header["kid"] = "1234567890kid"
	tokenString, err := token.SignedString(signKey)
	if err != nil {
		return "", err
	}

	return tokenString, nil
}

func TestValidateTokenTest(t *testing.T) {
	token, err := createTestJwtToken("menu-api", "restaurant-api")

	response := http.Response{
		StatusCode: 200,
		Body:       ioutil.NopCloser(bytes.NewBuffer(keysResponse)),
		Header:     make(http.Header),
	}

	jwkMockHTTPClient := mock.JWKMockHTTPClient{}
	jwkMockHTTPClient.On("Get", "http://localhost/.well-known/openid-configuration/jwks").Return(&response, nil)

	jwtVerifier := JwtVerifier{
		HTTPClient: &jwkMockHTTPClient,
		Cache:      cache.New(5*time.Minute, 10*time.Minute),
		Authority:  "http://localhost",
	}

	jwtVerifier.New()
	result, _ := jwtVerifier.ValidateToken(token)

	assert.True(t, result)
	assert.Nil(t, err)
	assert.NotEmpty(t, token)
}

