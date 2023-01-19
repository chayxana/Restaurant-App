grpcurl -plaintext -d @ localhost:8080 payments.PaymentService.Payment <<EOM
{
    "amount": 1,
    "credit_card": {
        "credit_card_cvv": 123,
        "credit_card_expiration_month": 10,
        "credit_card_expiration_year": 27,
        "credit_card_number": "4242424242424242"
    },
    "order_id": "sed",
    "user_id": "laboris magna velit"
}
EOM