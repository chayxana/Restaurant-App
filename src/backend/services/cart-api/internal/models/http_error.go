package models

import "fmt"

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

// Error implements error.
func (e *HTTPError) Error() string {
	return fmt.Sprintf("code: %v message:%v", e.Code, e.Message)
}

var _ error = (*HTTPError)(nil)
