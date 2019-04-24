package mock

import "github.com/stretchr/testify/mock"

// JwtTokenVerifierMock mocking of TokenVerifier interface
type JwtTokenVerifierMock struct {
	mock.Mock
}

func (j *JwtTokenVerifierMock) ValidateToken(bearerToken string) (bool, error) {
	args := j.Called(bearerToken)
	return args.Get(0).(bool), args.Error(1)
}