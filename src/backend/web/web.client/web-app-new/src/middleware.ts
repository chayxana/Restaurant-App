import { Exception, SpanKind, SpanStatusCode, context, trace } from '@opentelemetry/api';
import { SemanticAttributes } from '@opentelemetry/semantic-conventions';
import { NextResponse, type NextRequest } from 'next/server'

export async function middleware(request: NextRequest) {
    console.log("middleware: " + request.url);

    const { method, url = '' } = request;
    const [target] = url.split('?');


    // const baggage = propagation.getBaggage(context.active());

    // if synthetic_request baggage is set, create a new trace linked to the span in context
    // this span will look similar to the auto-instrumented HTTP span
    const tracer = trace.getTracer(process.env.OTEL_SERVICE_NAME as string);
    const span = tracer.startSpan(`HTTP ${method}`, {
        root: true,
        kind: SpanKind.SERVER,
        attributes: {
            'app.synthetic_request': true,
            [SemanticAttributes.HTTP_TARGET]: target,
            [SemanticAttributes.HTTP_METHOD]: method,
            //   [SemanticAttributes.HTTP_USER_AGENT]: headers['user-agent'] || '',
            //   [SemanticAttributes.HTTP_URL]: `${headers.host}${url}`,
            //   [SemanticAttributes.HTTP_FLAVOR]: httpVersion,
        },
    });

    let httpStatus = 200;
    try {
        const ctx = trace.setSpan(context.active(), span);
        const response = await context.with(ctx, () => {
            return NextResponse.next();
        });
        httpStatus = response.status;
    } catch (error) {
        span.recordException(error as Exception);
        span.setStatus({ code: SpanStatusCode.ERROR });
        httpStatus = 500;
        throw error;
    } finally {
        span.setAttribute(SemanticAttributes.HTTP_STATUS_CODE, httpStatus);
        span.end();
    }

}