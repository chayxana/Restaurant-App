package producer

import (
	"context"

	"github.com/Shopify/sarama"
	"github.com/rs/zerolog/log"
	"go.opentelemetry.io/contrib/instrumentation/github.com/Shopify/sarama/otelsarama"
	"go.opentelemetry.io/otel"
)

type KafkaEventProducer struct {
	producer sarama.SyncProducer
	topic    string
}

func NewKafkaEventProducer(producer sarama.SyncProducer, topic string) *KafkaEventProducer {
	return &KafkaEventProducer{
		producer: producer,
		topic:    topic,
	}
}

func (k *KafkaEventProducer) Publish(ctx context.Context, data []byte) error {
	msg := &sarama.ProducerMessage{
		Topic: k.topic,
		Value: sarama.ByteEncoder(data),
	}
	otel.GetTextMapPropagator().Inject(ctx, otelsarama.NewProducerMessageCarrier(msg))

	partition, offset, err := k.producer.SendMessage(msg)
	if err != nil {
		log.Printf("FAILED to send message: %s\n", err)
	} else {
		log.Printf("> message sent to partition %d at offset %d\n", partition, offset)
	}

	return nil
}
