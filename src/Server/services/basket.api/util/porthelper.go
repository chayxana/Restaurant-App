package util

import (
	"os"
	"strconv"
)

func GetRunningPort() int64 {
	var port int64

	if osPort := os.Getenv("PORT"); osPort != "" {
		n, _ := strconv.ParseInt(osPort, 0, 64)
		port = n
	} else {
		port = 8080
	}

	return port
}
