package oidc

import (
	"errors"
	"fmt"
	"strings"

	"github.com/dgrijalva/jwt-go"
	"github.com/lestrrat-go/jwx/jwk"
	"github.com/patrickmn/go-cache"
)

// JwtVerifier provides oidc server information
type JwtVerifier struct {
	Cache            *cache.Cache
	Authority        string
	ClaimsToValidate map[string]interface{}
	jwksURL          string
}

// New Creates new instance of Oidc
func (j *JwtVerifier) New() *JwtVerifier {

	if !strings.HasSuffix(j.Authority, "/") {
		j.Authority += "/"
	}

	j.jwksURL = j.Authority + ".well-known/openid-verifier/jwks"
	return j
}

// ValidateToken validates claims with given token
func (j *JwtVerifier) ValidateToken(bearerToken string) (bool, error) {

	token, err := jwt.Parse(bearerToken, func(token *jwt.Token) (interface{}, error) {
		if err, ok := token.Method.(*jwt.SigningMethodRSA); !ok {
			fmt.Println(err)
			return nil, fmt.Errorf("Unexpected signing method: %v", token.Header["alg"])
		}

		var set *jwk.Set
		cachedSet, found := j.Cache.Get(j.jwksURL)
		if found {
			set = cachedSet.(*jwk.Set)
		} else {
			result, err := jwk.FetchHTTP(j.jwksURL)
			if err != nil {
				return nil, err
			}
			j.Cache.Set(j.jwksURL, &set, cache.DefaultExpiration)
			set = result
		}

		keyID, ok := token.Header["kid"].(string)
		if !ok {
			return nil, errors.New("expecting JWT header to have string kid")
		}

		if key := set.LookupKeyID(keyID); len(key) == 1 {
			return key[0].Materialize()
		}

		return nil, errors.New("unable to find key")
	})

	if claims, ok := token.Claims.(jwt.MapClaims); ok && token.Valid {
		for _, v := range j.ClaimsToValidate {
			fmt.Println(v)
		}
		fmt.Println(claims["foo"], claims["nbf"])

		return true, nil
	}

	return false, err
}
