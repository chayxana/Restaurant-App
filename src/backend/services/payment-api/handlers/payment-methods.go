package handlers

import (
	"encoding/json"
	"net/http"
	"strconv"
)

type PaymentMethodsHandler struct {
}

type PaymentMethod struct {
	ID          int    `json:"id"`
	Name        string `json:"name"`
	Description string `json:"description"`
}

var paymentMethods = []PaymentMethod{
	{ID: 1, Name: "Visa", Description: "Credit card"},
	{ID: 2, Name: "Mastercard", Description: "Credit card"},
	{ID: 3, Name: "PayPal", Description: "Online payment"},
}

var paymentMethodsMap = map[int]PaymentMethod{
	1: {ID: 1, Name: "Visa", Description: "Credit card"},
	2: {ID: 2, Name: "Mastercard", Description: "Credit card"},
	3: {ID: 3, Name: "PayPal", Description: "Online payment"},
}

func (p *PaymentMethodsHandler) GetPaymentMethods(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	if err := json.NewEncoder(w).Encode(paymentMethods); err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	w.WriteHeader(http.StatusOK)
}

func (p *PaymentMethodsHandler) GetPaymentMethod(w http.ResponseWriter, r *http.Request) {
	id := r.PathValue("id")

	idInt, err := strconv.Atoi(id)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	paymentMethod, contains := paymentMethodsMap[idInt]
	if !contains {
		http.Error(w, "payment method not found", http.StatusNotFound)
		return
	}

	if err := json.NewEncoder(w).Encode(paymentMethod); err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
}
