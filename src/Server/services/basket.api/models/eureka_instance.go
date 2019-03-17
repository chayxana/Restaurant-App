package models

import (
	"encoding/json"
	"fmt"
)

// CreateEurekaRegistryBody creates new instance of object
func CreateEurekaRegistryBody(
	appName string,
	hostName string,
	ipAddress string,
	port int64) EurekaRegisterBody {

	var homeURL = fmt.Sprintf("http://%s:%v", hostName, port)
	var instanceID = fmt.Sprintf("%s:%s:%v", hostName, appName, port)

	var instance = Instance{
		InstanceID: instanceID,
		App:        appName,
		IPAddr:     ipAddress,
		Port: Port{
			Enabled: true,
			Value:   port,
		},
		HomePageURL:      homeURL + "/",
		StatusPageURL:    homeURL + "/info",
		HealthCheckURL:   homeURL + "/health",
		VipAddress:       appName,
		SecureVipAddress: appName,
		CountryID:        0,
		DataCenterInfo: DataCenterInfo{
			Class: "com.netflix.appinfo.InstanceInfo$DefaultDataCenterInfo",
			Name:  "MyOwn",
		},
		HostName:   hostName,
		Status:     "UP",
		ActionType: "ADDED",
	}

	var body = EurekaRegisterBody{
		Instance: instance,
	}
	return body
}

// Parse2Json converts object to json string
func (r *EurekaRegisterBody) Parse2Json() ([]byte, error) {
	return json.Marshal(r)
}

// EurekaRegisterBody is main request boyd
type EurekaRegisterBody struct {
	Instance Instance `json:"instance"`
}

// Instance Body of eureka client registry
type Instance struct {
	InstanceID       string         `json:"instanceId"`
	App              string         `json:"app"`
	IPAddr           string         `json:"ipAddr"`
	Port             Port           `json:"port"`
	HomePageURL      string         `json:"homePageUrl"`
	StatusPageURL    string         `json:"statusPageUrl"`
	HealthCheckURL   string         `json:"healthCheckUrl"`
	VipAddress       string         `json:"vipAddress"`
	SecureVipAddress string         `json:"secureVipAddress"`
	CountryID        int64          `json:"countryId"`
	DataCenterInfo   DataCenterInfo `json:"dataCenterInfo"`
	HostName         string         `json:"hostName"`
	Status           string         `json:"status"`
	ActionType       string         `json:"actionType"`
}

// DataCenterInfo reproduces data center info
type DataCenterInfo struct {
	Class string `json:"@class"`
	Name  string `json:"name"`
}

// Port reproduces port information
type Port struct {
	Enabled bool  `json:"@enabled"`
	Value   int64 `json:"$"`
}
