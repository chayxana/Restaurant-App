package handlers

import (
	"context"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/basket.api/models"
)

type CustomerBasketGetter interface {
	Get(customerID string) (*models.CustomerBasket, error)
}

type EventPublisher interface {
	Publish(ctx context.Context, data any) error
}

type CheckOutHandler struct {
	customerBasketGetter CustomerBasketCreateDeleteGetter
	publisher            EventPublisher
}

func NewCheckOutHandler() *CheckOutHandler {
	return &CheckOutHandler{}
}

func (h *CheckOutHandler) Checkout(c *gin.Context) {
	var checkout models.Checkout
	c.BindJSON(&checkout)

	customerBasket, err := h.customerBasketGetter.Get(checkout.UserId)
	if err != nil {
		httpError := models.NewHTTPError(http.StatusNotFound, err)
		c.JSON(http.StatusNotFound, httpError)
		return
	}

	event := models.UserCheckoutEvent{
		CustomerBasket: customerBasket,
		CheckOutInfo:   &checkout,
	}

	err = h.publisher.Publish(c.Request.Context(), event)
	if err != nil {
		httpError := models.NewHTTPError(http.StatusInternalServerError, err)
		c.JSON(http.StatusInternalServerError, httpError)
		return
	}
}
