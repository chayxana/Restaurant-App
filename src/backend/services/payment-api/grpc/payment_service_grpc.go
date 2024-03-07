package grpc

import (
	"context"
	"strconv"

	pbv1 "github.com/chayxana/payment-api/pb/v1"
	"github.com/google/uuid"
	"github.com/pkg/errors"
	"github.com/rs/zerolog/log"
	"github.com/sgumirov/go-cards-validation"
	"go.opentelemetry.io/otel"
	"go.opentelemetry.io/otel/attribute"
	"go.opentelemetry.io/otel/trace"
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

func (p *PaymentServiceGrpc) GetPaymentMethods(context.Context, *pbv1.GetPaymentMethodsRequest) (*pbv1.GetPaymentMethodsResponse, error) {
	return nil, nil
}

// Payment implements v1.PaymentServiceServer
func (p *PaymentServiceGrpc) Payment(ctx context.Context, req *pbv1.PaymentRequest) (res *pbv1.PaymentResponse, err error) {
	var tracer = otel.Tracer("payment-api/grpc")
	_, span := tracer.Start(ctx, "PaymentServiceGrpc",
		trace.WithAttributes(
			attribute.String("order_id", req.OrderId),
			attribute.String("user_id", req.UserId),
			attribute.Float64("amount", float64(req.Amount)),
		),
	)
	defer span.End()

	card := cards.Card{
		Number: req.GetCreditCard().CreditCardNumber,
		Cvv:    strconv.Itoa(int(req.GetCreditCard().CreditCardCvv)),
		Month:  strconv.Itoa(int(req.CreditCard.CreditCardExpirationMonth)),
		Year:   strconv.Itoa(int(req.CreditCard.CreditCardExpirationYear)),
	}
	err = card.Brand()
	if err != nil {
		// span.RecordError(err)
		return nil, errors.Wrap(err, ErrInvalidCardInfo.Error())
	}
	cardType := card.Company.Code
	if !(cardType == "visa" || cardType == "mastercard") {
		// span.RecordError(ErrUnsupportedCardType)
		return nil, ErrUnsupportedCardType
	}

	if err := card.Validate(p.enableTestCards); err != nil {
		// span.RecordError(ErrInvalidCardInfo)
		return nil, errors.Wrap(err, ErrInvalidCardInfo.Error())
	}

	lastFour, _ := card.LastFour()
	// traceID := span.SpanContext().TraceID().String()
	// spanID := span.SpanContext().SpanID().String()

	log.Info().
		Str("card_type", cardType).
		Float32("amount", req.Amount).
		Str("order_id", req.OrderId).
		Str("user_id", req.UserId).
		Str("ending", lastFour).
		// Str("trace_id", traceID).
		// Str("span_id", spanID).
		Msg("Transaction processed")

	transactionID := uuid.New().String()
	// span.SetAttributes(attribute.String("transactionID", transactionID))

	return &pbv1.PaymentResponse{
		TransactionId: transactionID,
	}, nil
}
