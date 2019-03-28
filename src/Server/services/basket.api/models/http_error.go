package models

// NewHTTPError creates new http error using Golang error
func NewHTTPError(status int, err error) *HTTPError {
	er := HTTPError{
		Code:    status,
		Message: err.Error(),
	}
	return &er
}

// HTTPError provides information for http error
type HTTPError struct {
	Code    int    `json:"code" example:"400"`
	Message string `json:"message" example:"status bad request"`
}
