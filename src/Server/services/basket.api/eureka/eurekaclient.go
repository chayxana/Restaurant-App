package eureka

import (
	"bytes"
	"fmt"
	"net/http"
	"os"
	"time"

	"github.com/gin-gonic/gin"

	"github.com/jurabek/basket.api/models"
	"github.com/jurabek/basket.api/util"
)

var appName = "basket-service"
var eurekaBaseURL = "http://localhost:8761/eureka"
var eurekaAppServerURL string
var instanceID string

// Register will send POST request to Eureka  server
func Register() bool {

	ipAddress := util.GetLocalIP()
	port := util.GetRunningPort()
	hostName := util.GetLocalHostName()

	if serviceURL, ok := os.LookupEnv("EUREKA_CLIENT_SERVICE_URL"); ok {
		eurekaAppServerURL = fmt.Sprintf("%s/apps/%s", serviceURL, appName)
	} else {
		eurekaAppServerURL = eurekaBaseURL + "/apps/" + appName
	}

	eurekaRegisterBody := models.CreateEurekaRegistryBody(appName, hostName, ipAddress, port)
	eurekaInstanceBodyJSON, _ := eurekaRegisterBody.Parse2Json()

	instanceID = eurekaRegisterBody.Instance.InstanceID

	if gin.Mode() == gin.DebugMode {
		fmt.Println("Response Body:", string(eurekaInstanceBodyJSON))
	}

	req, _ := http.NewRequest("POST", eurekaAppServerURL, bytes.NewBuffer(eurekaInstanceBodyJSON))
	req.Header.Set("Content-Type", "application/json")

	client := &http.Client{}

	// Simulating resilience, the reason Eureka service become available later when we run docker compose
	for i := 0; i < 10; i++ {

		respose, err := client.Do(req)
		if err != nil {
			fmt.Println("Eureka register error", err)
			time.Sleep(10 * time.Second)
			continue
		}

		defer respose.Body.Close()
		if respose.StatusCode < 300 {
			break
		}
	}
	go StartHeartbeat()

	return false
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
		fmt.Printf("Hearbeat to eureka: %s \r\n", heartBeatURL)
	}

	defer resp.Body.Close()
}

// UnRegister removes instance from Eureka Server
func UnRegister() {

	unRegisterURL := fmt.Sprintf("%s/%s", eurekaAppServerURL, instanceID)
	req, err := http.NewRequest("DELETE", unRegisterURL, nil)
	client := &http.Client{}
	resp, err := client.Do(req)

	fmt.Println("Trying to un register application...")

	if err != nil {
		fmt.Printf("Eureka un register error: %v", err)
	}
	defer resp.Body.Close()

	fmt.Println("Unregistered response status code: ", resp.StatusCode)
	fmt.Println("Unregistered application, exiting. Check Eureka...")
}
