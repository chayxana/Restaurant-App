package docs

import "strings"

// OverrideAuthURL Initializes docs for custom Auth Url
func OverrideAuthURL(authURL string) {
	doc = strings.ReplaceAll(doc, "{{.AuthUrl}}", authURL)
}
