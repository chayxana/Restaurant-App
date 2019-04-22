package oidc

import (
	"errors"
	"fmt"
	"reflect"
	"strings"

	"github.com/dgrijalva/jwt-go"
	"github.com/lestrrat-go/jwx/jwk"
	"github.com/patrickmn/go-cache"
)



// JwtVerifier provides oidc server information
type JwtVerifier struct {
	HTTPClient       jwk.HTTPClient
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
	j.jwksURL = j.Authority + ".well-known/openid-configuration/jwks"
	return j
}

// ValidateToken validates claims with given token
func (j *JwtVerifier) ValidateToken(bearerToken string) (bool, error) {

	token, err := jwt.Parse(bearerToken, func(token *jwt.Token) (interface{}, error) {
		if err, ok := token.Method.(*jwt.SigningMethodRSA); !ok {
			fmt.Println(err)
			return nil, fmt.Errorf("unexpected signing method: %v", token.Header["alg"])
		}

		var set *jwk.Set
		cachedSet, found := j.Cache.Get(j.jwksURL)
		if found {
			set = cachedSet.(*jwk.Set)
		} else {
			option := jwk.WithHTTPClient(j.HTTPClient)
			result, err := jwk.FetchHTTP(j.jwksURL, option)
			if err != nil {
				return nil, err
			}
			j.Cache.Set(j.jwksURL, &set, cache.NoExpiration)
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
		for k, v := range j.ClaimsToValidate {
			claim := claims[k]
			switch reflect.TypeOf(claim).Kind() {
			case reflect.String:
				if claim != v {
					return false, fmt.Errorf("claims validate failed, invalid claim: %v", k)
				}
			case reflect.Slice:
				var itemFound bool
				for _, c := range claim.([]interface{}) {
					if c == v {
						itemFound = true
						break
					}
				}
				if !itemFound {
					return false, fmt.Errorf("claims validate failed, invalid claim: %v", k)
				}
			}
		}
		return true, nil
	}

	return false, err
}
