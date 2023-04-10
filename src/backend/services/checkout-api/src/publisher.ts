import { Kafka, Producer, ProducerRecord } from 'kafkajs'
import { config } from './config'

export class Publisher {
  private producer: Producer

  constructor(private topic: string, private brokers: Array<string>, private clientId: string) {
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

  public async Publish(message: any): Promise<void> {
    const record: ProducerRecord = {
     topic: this.topic,
     messages: [{value: JSON.stringify(message)}],
    }
    await this.producer.send(record)
  }

  private createProducer() : Producer {
    const kafka = new Kafka({
      clientId: this.clientId,
      brokers: this.brokers,
    })
    return kafka.producer()
  }
}


const checkoutPublisher = new Publisher(config.checkoutTopic, [config.checkoutKafkaBroker], "checkout-api")
checkoutPublisher.start().catch(console.error)

export default checkoutPublisher;