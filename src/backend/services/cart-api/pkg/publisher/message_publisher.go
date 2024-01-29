package producer

import (
	"context"

	"github.com/IBM/sarama"
	"github.com/dnwe/otelsarama"
	"github.com/rs/zerolog/log"
	"go.opentelemetry.io/otel"
)

type MessagePublisher struct {
	producer sarama.SyncProducer
	topic    string
}

func NewMessagePublisher(producer sarama.SyncProducer, topic string) *MessagePublisher {
	return &MessagePublisher{
		producer: producer,
		topic:    topic,
	}
}

func (k *MessagePublisher) Publish(ctx context.Context, data []byte) error {
	msg := &sarama.ProducerMessage{
		Topic: k.topic,
		Value: sarama.ByteEncoder(data),
	}
	otel.GetTextMapPropagator().Inject(ctx, otelsarama.NewProducerMessageCarrier(msg))

	partition, offset, err := k.producer.SendMessage(msg)
	if err != nil {
		log.Error().Err(err).Str("topic", k.topic).Msg("failed to send message")
	} else {
		log.Info().Str("topic", k.topic).Msgf("> message sent to partition %d at offset %d\n", partition, offset)
	}

	return nil
}
