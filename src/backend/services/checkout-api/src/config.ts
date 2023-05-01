import dotenv from 'dotenv';

dotenv.config();

export interface Config {
    host: string
    port: number
    checkoutKafkaBroker: string
    checkoutTopic: string
    paymentAPIGrpcUrl: string
    cartAPIGrpcUrl: string
    baseUrl: string
}

export function getConfig(): Config {
    const host = process.env.HOST || "127.0.0.1";
    const port = process.env.PORT ? Number(process.env.PORT) : 8080;
    const checkoutKafkaBroker = process.env.KAFKA_BROKER || ""
    const checkoutTopic = process.env.CHECKOUT_TOPIC || ""
    const paymentAPIGrpcUrl = process.env.PAYMENT_API_URL || "localhost:50004"
    const cartAPIGrpcUrl = process.env.CART_URL || "localhost:50003"
    const baseUrl = process.env.BASE_URL || "/"
    return {
        host,
        port,
        checkoutKafkaBroker,
        checkoutTopic,
        paymentAPIGrpcUrl,
        cartAPIGrpcUrl,
        baseUrl
    }
}

export const config = getConfig()