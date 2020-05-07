# Getting started 

### Prerequisites
- Docker Desktop
- or separate Development SDK's `.NET Core 3.1`, `openjdk64-1.8`, `go v1.13.5`, `postgresql`, `redis`, `node v13.11.0`

### How to Run
Use Docker Compose for running. Run following command from the root directory `cd src/backend` and `docker-compose up`

### Exposed endpoints
When you are using `docker` all services url's will be exposed only via API Gateway using `Netflix Zuul` it can be accessed from `http://localhost:8080`

**Swagger API for Menu.API**

```http://localhost:8080/menu/swagger/index.html```

**Swagger API for Order.API**

```http://localhost:8080/order/swagger-ui.html```

**Swagger API for Basket.API**

```http://localhost:8080/basket/swagger/index.html```
