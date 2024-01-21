package docs

import "strings"

// OverrideAuthURL Initializes docs for custom Auth Url
func OverrideAuthURL(authURL string) string {
	return strings.ReplaceAll(docTemplate, "{{.AuthUrl}}", authURL)
}
