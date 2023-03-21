package handlers

import (
	"context"
	"encoding/json"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/jurabek/basket.api/internal/models"
)

type CustomerBasketGetter interface {
	Get(customerID string) (*models.CustomerBasket, error)
}

type EventPublisher interface {
	Publish(ctx context.Context, data []byte) error
}

type CheckOutHandler struct {
	customerBasketGetter GetCreateDeleter
	publisher            EventPublisher
}

func NewCheckOutHandler(
	customerBasketGetter GetCreateDeleter,
	publisher EventPublisher,
) *CheckOutHandler {
	return &CheckOutHandler{
		customerBasketGetter: customerBasketGetter,
		publisher:            publisher,
	}
}

// Create go doc
//	@Summary		Starts checkout for the entered card and basket items
//	@Description	Start checkout
//	@Tags			Checkout
//	@Accept			json
//	@Produce		json
//	@Param			Checkout	body	models.Checkout	true	"Checkout"
//	@Success		200			""
//	@Failure		400			{object}	models.HTTPError
//	@Router			/checkout [post]
func (h *CheckOutHandler) Checkout(c *gin.Context) {
	var checkout models.Checkout
	err := c.BindJSON(&checkout)
	if err != nil {
		c.JSON(http.StatusBadRequest, models.NewHTTPError(http.StatusNotFound, err))
		return
	}

	customerBasket, err := h.customerBasketGetter.Get(c.Request.Context(), checkout.CustomerID)
	if err != nil {
		httpError := models.NewHTTPError(http.StatusNotFound, err)
		c.JSON(http.StatusNotFound, httpError)
		return
	}

	event := models.UserCheckoutEvent{
		CustomerBasket: customerBasket,
		CheckOutInfo:   &checkout,
	}

	jsonEvent, err := json.Marshal(event)
	if err != nil {
		c.JSON(http.StatusBadRequest, models.NewHTTPError(http.StatusNotFound, err))
		return
	}

	err = h.publisher.Publish(c.Request.Context(), jsonEvent)
	if err != nil {
		httpError := models.NewHTTPError(http.StatusInternalServerError, err)
		c.JSON(http.StatusInternalServerError, httpError)
		return
	}
}
