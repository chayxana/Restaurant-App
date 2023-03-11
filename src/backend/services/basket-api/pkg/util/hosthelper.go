package util

import (
	"os"
)

func GetLocalHostName() string {
	var hostName string

	if hostname, err := os.Hostname(); err != nil {
		hostName = ""
	} else {
		hostName = hostname
	}

	return hostName
}
