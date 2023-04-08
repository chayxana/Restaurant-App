import { Kafka, Message, Producer, ProducerBatch, TopicMessages } from 'kafkajs'


interface CustomMessageFormat { a: string }

export default class ProducerFactory {
  private producer: Producer

  constructor() {
    this.producer = this.createProducer()
  }

  public async start(): Promise<void> {
    try {
      await this.producer.connect()
    } catch (error) {
      console.log('Error connecting the producer: ', error)
    }
  }

  public async shutdown(): Promise<void> {
    await this.producer.disconnect()
  }

  public async sendBatch(messages: Array<CustomMessageFormat>): Promise<void> {
    const kafkaMessages: Array<Message> = messages.map((message) => {
      return {
        value: JSON.stringify(message)
      }
    })

    const topicMessages: TopicMessages = {
      topic: 'producer-topic',
      messages: kafkaMessages
    }

    const batch: ProducerBatch = {
      topicMessages: [topicMessages]
    }

    await this.producer.sendBatch(batch)
  }

  private createProducer() : Producer {
    const kafka = new Kafka({
      clientId: 'producer-client',
      brokers: ['localhost:9092'],
    })

    return kafka.producer()
  }
}