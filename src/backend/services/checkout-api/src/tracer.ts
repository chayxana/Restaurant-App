import { NodeTracerProvider } from '@opentelemetry/sdk-trace-node';
import { BatchSpanProcessor, SimpleSpanProcessor, ConsoleSpanExporter } from '@opentelemetry/sdk-trace-base';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { GrpcInstrumentation } from '@opentelemetry/instrumentation-grpc';
import { HttpInstrumentation } from '@opentelemetry/instrumentation-http';
import { ExpressInstrumentation } from '@opentelemetry/instrumentation-express';
import { trace } from '@opentelemetry/api';
import { OTLPTraceExporter } from '@opentelemetry/exporter-trace-otlp-grpc';
import { Resource } from "@opentelemetry/resources";
import { SemanticResourceAttributes } from "@opentelemetry/semantic-conventions";
import { diag, DiagConsoleLogger, DiagLogLevel } from '@opentelemetry/api';
import { KafkaJsInstrumentation } from 'opentelemetry-instrumentation-kafkajs';
import { NodeSDK } from '@opentelemetry/sdk-node';
import { getNodeAutoInstrumentations } from '@opentelemetry/auto-instrumentations-node';
import { ConsoleMetricExporter, PeriodicExportingMetricReader } from '@opentelemetry/sdk-metrics';
import { OTLPMetricExporter } from '@opentelemetry/exporter-metrics-otlp-proto';

export const OpenTelemetry = (serviceName: string) => {
  // diag.setLogger(new DiagConsoleLogger(), DiagLogLevel.DEBUG);

  const resource = Resource.default().merge(
    new Resource({
      [SemanticResourceAttributes.SERVICE_NAME]: serviceName,
      [SemanticResourceAttributes.SERVICE_VERSION]: "0.0.1",
    })
  );

  const processor = new BatchSpanProcessor(new OTLPTraceExporter({
    url: process.env.OTEL_EXPORTER_OTLP_ENDPOINT,
  }));

  return new NodeSDK({
    resource,
    spanProcessor: processor,
    instrumentations: [
      getNodeAutoInstrumentations({
        '@opentelemetry/instrumentation-fs': {
          enabled: false,
        }
      }),
      new KafkaJsInstrumentation()
    ],
    metricReader: new PeriodicExportingMetricReader({
      exporter: new OTLPMetricExporter({
        url: process.env.OTEL_EXPORTER_OTLP_ENDPOINT,
      }),
    }),
  })
}