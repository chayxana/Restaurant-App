package reciever

import (
	"context"

	"github.com/IBM/sarama"
	"github.com/rs/zerolog/log"
)

type MessageReciever struct {
	consumer sarama.Consumer
	topic    string
}

func NewMessageReciever(consumer sarama.Consumer, topic string) *MessageReciever {
	return &MessageReciever{
		consumer: consumer,
		topic:    topic,
	}
}

type Message struct {
	Value      []byte
	Attributes map[string]string
}

type MessageHandler interface {
	Handle(ctx context.Context, message *Message) error
}

// Recieve starts consuming messages from all partitions and sends message as channel
func (k *MessageReciever) Recieve(handler MessageHandler) error {
	partitionList, err := k.consumer.Partitions(k.topic)
	if err != nil {
		log.Error().Err(err).Msg("failed to get partions")
		return err
	}

	for partition := range partitionList {
		pc, err := k.consumer.ConsumePartition(k.topic, int32(partition), sarama.OffsetNewest)
		if err != nil {
			log.Error().Err(err).Msg("failed to get consume partion")
			return err
		}
		defer pc.AsyncClose()
		go func(pc sarama.PartitionConsumer, partition int) {
			log.Info().Str("topic", k.topic).Int("partition", partition).Msg("starting to consume messages")
			for msg := range pc.Messages() {
				if err := handler.Handle(context.Background(), &Message{Value: msg.Value}); err != nil {
					log.Error().Err(err).Str("topic", k.topic).Msg("failed to consume message")
				}
			}
		}(pc, partition)
	}
	select {}
}
