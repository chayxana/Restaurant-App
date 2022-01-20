# Getting started 

### Prerequisites
- Docker Desktop
- or separate Development SDK's `.NET Core 3.1`, `openjdk64-1.8`, `go v1.13.5`, `postgresql`, `redis`, `node v13.11.0`

### How to Run
Use Docker Compose for running. Run following command from the root directory `cd src/backend` and `sh docker-compose-up.sh`

### Exposed endpoints
When you are using `docker` all services url's will be exposed only via `nginx` reverse proxy, it can be accessed via [localhost](http://localhost:8080)

- [Dashboard App](http://localhost:8080/dashboard)
- [Swagger API for Menu.API](http://localhost:8080/menu/swagger/index.html)
- [Swagger API for Order.API](http://localhost:8080/order/swagger-ui.html)
- [Swagger API for Basket.API](http://localhost:8080/basket/swagger/index.html)



## ELK
- [Kibana dashboard URL](http://localhost:5601)

### Setup Kibana log indexing

Browse to [Index Patterns](http://localhost:5601/app/management/kibana/indexPatterns) and set up the index name pattern for Kibana. Specify fluentd-* to Index name or pattern and click Create.

For more detailed information check it out [here](https://docs.fluentd.org/container-deployment/docker-compose)
