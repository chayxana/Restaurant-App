package oidc

import "net/http"

// JWKHttpClient is implementation of jwk.HTTPClient
type JWKHttpClient struct {
}

// Get returns jwk response
func (c *JWKHttpClient) Get(url string) (*http.Response, error) {
	return http.DefaultClient.Get(url)
}
