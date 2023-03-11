package producer

import (
	"context"

	"github.com/Shopify/sarama"
	"github.com/rs/zerolog/log"
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
	partition, offset, err := k.producer.SendMessage(&sarama.ProducerMessage{
		Topic: k.topic,
		Value: sarama.ByteEncoder(data),
	})
	if err != nil {
		log.Printf("FAILED to send message: %s\n", err)
	} else {
		log.Printf("> message sent to partition %d at offset %d\n", partition, offset)
	}

	return nil
}
