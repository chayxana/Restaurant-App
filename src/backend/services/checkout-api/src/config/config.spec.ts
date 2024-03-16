import { expect } from "chai";
import { Config, config, getConfig } from "./config";

describe("getConfig", () => {
  it("should return a Config object with default values when environment variables are not set", () => {
    const expectedConfig: Config = {
      host: "127.0.0.1",
      port: 8080,
      checkoutKafkaBroker: "",
      checkoutTopic: "",
      paymentAPIGrpcUrl: "localhost:50004",
      cartAPIGrpcUrl: "localhost:50003",
      baseUrl: "/"
    };
    const actualConfig = config;
    expect(actualConfig).to.deep.equal(expectedConfig);
  });

  it("should return a Config object with values from environment variables when they are set", () => {
    process.env.HOST = "example.com";
    process.env.PORT = "1234";
    process.env.KAFKA_BROKER = "kafka.example.com:9092";
    process.env.CHECKOUT_TOPIC = "checkout";
    process.env.PAYMENT_API_URL = "payment.example.com:50004";
    process.env.CART_URL = "cart.example.com:50003";
    process.env.BASE_URL = "/api";
    const expectedConfig: Config = {
      host: "example.com",
      port: 1234,
      checkoutKafkaBroker: "kafka.example.com:9092",
      checkoutTopic: "checkout",
      paymentAPIGrpcUrl: "payment.example.com:50004",
      cartAPIGrpcUrl: "cart.example.com:50003",
      baseUrl: "/api"
    };
    const actualConfig = getConfig();
    expect(actualConfig).to.deep.equal(expectedConfig);
    // Reset environment variables to their original values
    delete process.env.HOST;
    delete process.env.PORT;
    delete process.env.KAFKA_BROKER;
    delete process.env.CHECKOUT_TOPIC;
    delete process.env.PAYMENT_API_URL;
    delete process.env.CART_URL;
    delete process.env.BASE_URL;
  });
});
