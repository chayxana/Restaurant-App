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
- The checkout process involves retrieving the customer's cart items using the grpc by calling basket-apis `getCustomerCartItems` function.
- After that it calculates amount that user has to pay and processes payment using the payment-apis `Payment()` function.
- When payment is successful it creates a checkout event and publishes to a message broker (e.g Kafka) using the `checkoutPublisher.Publish()` function.

If any errors occur during the checkout process, the Checkout() function throws an error and the API endpoint returns a 500 HTTP status code with an error message.