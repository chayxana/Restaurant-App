package oauth2

// Oidc provides oidc server information
type Oidc struct {
	wellKnownURL string
}

// New Creates new instance of Oidc
func (d Oidc) New() {
	d.wellKnownURL = "/.well-known/openid-configuration"
}

// GetWellKnownURL returns oidc well known url
func (d Oidc) GetWellKnownURL() string {
	return d.wellKnownURL
}
