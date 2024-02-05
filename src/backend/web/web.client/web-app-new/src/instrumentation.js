"use strict";
var _a;
Object.defineProperty(exports, "__esModule", { value: true });
var sdk_node_1 = require("@opentelemetry/sdk-node");
// import { ConsoleSpanExporter } from '@opentelemetry/sdk-trace-node';
var sdk_metrics_1 = require("@opentelemetry/sdk-metrics");
var resources_1 = require("@opentelemetry/resources");
var semantic_conventions_1 = require("@opentelemetry/semantic-conventions");
var exporter_trace_otlp_http_1 = require("@opentelemetry/exporter-trace-otlp-http");
// import { diag, DiagConsoleLogger, DiagLogLevel } from '@opentelemetry/api';
// diag.setLogger(new DiagConsoleLogger(), DiagLogLevel.DEBUG);
var sdk = new sdk_node_1.NodeSDK({
    resource: new resources_1.Resource((_a = {},
        _a[semantic_conventions_1.SemanticResourceAttributes.SERVICE_NAME] = 'web-app',
        _a[semantic_conventions_1.SemanticResourceAttributes.SERVICE_VERSION] = '1.0',
        _a)),
    traceExporter: new exporter_trace_otlp_http_1.OTLPTraceExporter({
        url: 'http://localhost:4318/v1/traces',
    }),
    metricReader: new sdk_metrics_1.PeriodicExportingMetricReader({
        exporter: new sdk_metrics_1.ConsoleMetricExporter(),
    }),
});
sdk.start();
