package models

type UserCheckoutEvent struct {
	CheckOutInfo   *Checkout       `json:"checkout_info"`
	CustomerBasket *CustomerBasket `json:"customer_basket"`
}

type Checkout struct {
	CustomerID   string          `json:"customer_id"`
	UserCurrency string          `json:"user_currency"`
	Address      *Address        `json:"address"`
	Email        string          `json:"email"`
	CreditCard   *CreditCardInfo `json:"credit_card"`
}

type Address struct {
	StreetAddress string `json:"street_address"`
	City          string `json:"city"`
	State         string `json:"state"`
	Country       string `json:"country"`
	ZipCode       int32  `json:"zip_code"`
}

type CreditCardInfo struct {
	CreditCardNumber          string `json:"credit_card_number"`
	CreditCardCvv             int32  `json:"credit_card_cvv"`
	CreditCardExpirationYear  int32  `json:"credit_card_expiration_year"`
	CreditCardExpirationMonth int32  `json:"credit_card_expiration_month"`
}
