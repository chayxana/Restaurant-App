package grpc

import (
	"context"
	"errors"
	"strconv"

	pbv1 "github.com/chayxana/payment-api/pb/v1"
	"github.com/google/uuid"
	"github.com/rs/zerolog/log"
	"github.com/sgumirov/go-cards-validation"
)

var _ pbv1.PaymentServiceServer = (*PaymentServiceGrpc)(nil)

type PaymentServiceGrpc struct {
	enableTestCards bool
}

func NewPaymentServiceGrpc(enableTestCards bool) *PaymentServiceGrpc {
	return &PaymentServiceGrpc{
		enableTestCards: enableTestCards,
	}
}

var ErrUnsupportedCardType = errors.New("card type unsupported")
var ErrInvalidCardInfo = errors.New("invalid card information")

// Payment implements v1.PaymentServiceServer
func (p *PaymentServiceGrpc) Payment(ctx context.Context, req *pbv1.PaymentRequest) (*pbv1.PaymentResponse, error) {
	card := cards.Card{
		Number: req.GetCreditCard().CreditCardNumber,
		Cvv:    strconv.Itoa(int(req.GetCreditCard().CreditCardCvv)),
		Month:  strconv.Itoa(int(req.CreditCard.CreditCardExpirationMonth)),
		Year:   strconv.Itoa(int(req.CreditCard.CreditCardExpirationYear)),
	}
	err := card.Brand()
	if err != nil {
		return nil, ErrInvalidCardInfo
	}
	cardType := card.Company.Code
	if !(cardType == "visa" || cardType == "mastercard") {
		return nil, ErrUnsupportedCardType
	}

	if err := card.Validate(p.enableTestCards); err != nil {
		return nil, ErrInvalidCardInfo
	}

	lastFour, _ := card.LastFour()

	log.Info().
		Str("card_type", cardType).
		Float32("amount", req.Amount).
		Str("order_id", req.OrderId).
		Str("user_id", req.UserId).
		Str("ending", lastFour).
		Msg("Transaction processed")

	return &pbv1.PaymentResponse{
		TransactionId: uuid.New().String(),
	}, nil
}
