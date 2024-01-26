# Checkout API

This service provides an API endpoint for checkout operation, which allows users to checkout their cart items and process payment.

## API

The API endpoint can be accessed through the following path:
```
POST /api/v1/checkout
```

The endpoint accepts a JSON payload in the request body in the following format:
```
{
    "address": {
        "city": "New York",
        "country": "US",
        "state": "NY",
        "street_address": "123 Main St",
        "zip_code": 10001
    },
    "credit_card": {
        "credit_card_cvv": 123,
        "credit_card_expiration_month": 1,
        "credit_card_expiration_year": 2023,
        "credit_card_number": "1234567890123456"
    },
    "customer_id": "12345",
    "email": "johndoe@example.com",
    "user_currency": "USD"
}
```

The response will be a plain text message, either "Checkout OK" or an error message with HTTP status code 500.

## Implementation Details

When a request is made to the API endpoint, the `Checkout()` function is called, which initiates the checkout process. 
- The checkout process involves retrieving the customer's cart items using the grpc by calling cart-api.
- After that it calculates amount that user has to pay and processes payment using the payment-api.
- When payment is successful it creates a checkout event and publishes to a message broker (e.g Kafka).

If any errors occur during the checkout process, the Checkout() function throws an error and the API endpoint returns a 500 HTTP status code with an error message.

# Developer Documentation for Checkout API

## Overview
The Checkout API is a Node.js-based API that allows customers to check out items from their shopping cart and make payments using a credit card. The API uses gRPC to communicate with other microservices and Kafka for asynchronous messaging.

## Dependencies
The API has several dependencies, including:

- `express`: A Node.js web application framework that is used for handling HTTP requests and responses.
- `kafkajs`: A Kafka client for Node.js that provides an interface for publishing and consuming messages from Kafka.
- `opentelemetry`: An observability framework that is used for distributed tracing and metrics collection.
- `swagger`: A tool for generating and serving API documentation.

## Project Structure
The project structure is as follows:

- `index.ts`: The entry point of the application that starts the Express server and defines the API routes.
- `config.ts`: The configuration module that loads environment variables and sets default values.
- `logger.ts`: The logger module that sets up a logging system for the application.
- `tracer.ts`: The tracing module that sets up distributed tracing using OpenTelemetry.
- `swagger.ts`: The Swagger module that generates API documentation.
- `routes.ts`: The module that defines the API routes.
- `cartService.ts:` The module that defines the cart service and provides methods for interacting with the cart microservice via gRPC.
- `paymentService.ts`: The module that defines the payment service and provides methods for processing payments via gRPC.
- `checkout.ts`: The module that handles the checkout process and publishes a checkout event to Kafka.
- `publisher.ts`: The module that defines the publisher and provides methods for publishing events to Kafka.
- `model.ts`: The module that defines the data models used in the API.
- `gen`: The directory that contains generated gRPC code for interacting with the cart and payment microservices.

## Scripts
The following scripts are available in the package.json file:

- `start`: Starts the application using nodemon for automatic restarts on changes.
- `build`: Transpiles the TypeScript code to JavaScript and outputs it to the dist directory.
- `tslint`: Lints the TypeScript code using TSLint.

## Environment Variables
The following environment variables are used in the application:

- `PORT`: The port on which the API server listens. Defaults to 3000.
- `KAFKA_BROKERS`: A comma-separated list of Kafka brokers. Required for Kafka integration.
- `GRPC_CART_SERVICE_URL`: The URL of the cart service gRPC endpoint. Required for cart service integration.
- `GRPC_PAYMENT_SERVICE_URL`: The URL of the payment service gRPC endpoint. Required for payment service integration.
- `OTLP_ENDPOINT`: The OpenTelemetry Collector endpoint for sending trace data. Optional.
- `LOG_LEVEL`: The logging level for the application. Defaults to info.

Testing
The API includes unit tests. Tests can be run using the `npx mocha --require ts-node/register src/**/*.spec.ts` command.

## API Documentation
API documentation is generated using Swagger and is available at /api-docs. The Swagger configuration is located in the swagger.ts module.

## License
The Checkout API is released under the MIT License. See the LICENSE file for more information.