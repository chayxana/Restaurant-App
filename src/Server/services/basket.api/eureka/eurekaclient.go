package eureka

import (
	"bytes"
	"fmt"
	"net/http"
	"os"
	"strconv"
	"time"

	"github.com/gin-gonic/gin"

	"github.com/jurabek/basket.api/models"
	"github.com/jurabek/basket.api/util"
)

var appName = "basket-api"
var eurekaAppServerURL string
var instanceID string

// Register will send POST request to Eureka  server
func Register() bool {

	eurekaBaseURL := "http://localhost:8761/eureka"

	var (
		port     int64
		hostName string
	)

	ipAddress := util.GetLocalIP()
	if osPort := os.Getenv("PORT"); osPort != "" {
		n, _ := strconv.ParseInt(osPort, 10, 10)
		port = n
	} else {
		port = 8080
	}
	if hostname, err := os.Hostname(); err != nil {
		fmt.Printf("Could not load hostname %v", err)
	} else {
		hostName = hostname
	}
	if serviceURL, isEmpty := os.LookupEnv("EUREKA_CLIENT_SERVICE_URL"); isEmpty {
		eurekaAppServerURL = fmt.Sprintf("%s/apps/%s", serviceURL, appName)
	} else {
		eurekaAppServerURL = eurekaBaseURL + "/apps/" + appName
	}

	eurekaRegisterBody := models.CreateEurekaRegistryBody(appName, hostName, ipAddress, port)
	eurekaInstanceBodyJSON, _ := eurekaRegisterBody.Parse2Json()

	instanceID = eurekaRegisterBody.Instance.InstanceID

	if gin.Mode() == gin.DebugMode {
		fmt.Println("response Body:", string(eurekaInstanceBodyJSON))
	}

	req, _ := http.NewRequest("POST", eurekaAppServerURL, bytes.NewBuffer(eurekaInstanceBodyJSON))
	req.Header.Set("Content-Type", "application/json")

	client := &http.Client{}

	var respose http.Response
	for i := 0; i < 3; i++ {
		respose, err := client.Do(req)
		if err != nil {
			fmt.Printf("Eureka register error: %v", err)
		}
		defer respose.Body.Close()

		if respose.StatusCode < 300 {
			break
		}
	}

	if respose.StatusCode > 300 {
		return false
	}

	return true
}

// StartHeartbeat starts sending heartbeat for Eureka Server
func StartHeartbeat() {
	for {
		time.Sleep(time.Second * 30)
		heartbeat()
	}
}

func heartbeat() {

	heartBeatURL := fmt.Sprintf("%s/%s", eurekaAppServerURL, instanceID)
	req, err := http.NewRequest("PUT", heartBeatURL, nil)

	client := &http.Client{}
	resp, err := client.Do(req)
	if err != nil {
		fmt.Printf("Eureka update error: %v", err)
	}

	if resp.StatusCode == http.StatusNotFound {
		Register()
	}

	if gin.Mode() == gin.DebugMode {
		fmt.Printf("Hearbeat to eureka: %s", heartBeatURL)
	}

	defer resp.Body.Close()
}

// UnRegister removes instance from Eureka Server
func UnRegister() {
	fmt.Println("Trying to un register application...")

	unRegisterURL := fmt.Sprintf("%s/%s", eurekaAppServerURL, instanceID)
	req, err := http.NewRequest("DELETE", unRegisterURL, nil)
	client := &http.Client{}
	resp, err := client.Do(req)

	if err != nil {
		fmt.Printf("Eureka un register error: %v", err)
	}
	defer resp.Body.Close()

	fmt.Println("Unregistered response status code: ", resp.StatusCode)
	fmt.Println("Unregistered application, exiting. Check Eureka...")
}
