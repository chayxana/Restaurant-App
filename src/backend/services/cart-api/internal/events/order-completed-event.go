package events

import (
	"context"
	"encoding/json"

	"github.com/jurabek/cart-api/internal/models"
	"github.com/jurabek/cart-api/pkg/reciever"
	"github.com/rs/zerolog/log"
)

type CartGetterUpdater interface {
	Get(ctx context.Context, cartID string) (*models.Cart, error)
	Update(ctx context.Context, cart *models.Cart) error
}

type OrderCompletedEventHandler struct {
	cartGetterUpdater CartGetterUpdater
}

func NewOrderCompletedEventHandler(cartGetterUpdater CartGetterUpdater) *OrderCompletedEventHandler {
	return &OrderCompletedEventHandler{cartGetterUpdater: cartGetterUpdater}
}

type OrderCompletedEvent struct {
	OrderID       string `json:"orderId"`
	CartID        string `json:"cartId"`
	UserID        string `json:"userId"`
	TransactionID string `json:"transactionId"`
	OrderDate     string `json:"orderDate"`
}

var _ reciever.MessageHandler = (*OrderCompletedEventHandler)(nil)

// Handle implements consumer.ConsumerMessageHandler.
func (h *OrderCompletedEventHandler) Handle(ctx context.Context, message *reciever.Message) error {
	log.Info().Msgf("OrderCompletedEvent received: %s", string(message.Value))

	orderCompletedEvent := &OrderCompletedEvent{}
	if err := json.Unmarshal(message.Value, orderCompletedEvent); err != nil {
		return err
	}

	cart, err := h.cartGetterUpdater.Get(ctx, orderCompletedEvent.CartID)
	if err != nil {
		log.Error().Err(err)
		return err
	}
	cart.Status = models.CartStatusCompleted
	cart.OrderID = &orderCompletedEvent.OrderID
	cart.UserID = &orderCompletedEvent.UserID
	cart.TransactionID = &orderCompletedEvent.TransactionID

	if err := h.cartGetterUpdater.Update(ctx, cart); err != nil {
		log.Error().Err(err)
		return err
	}
	return nil
}
