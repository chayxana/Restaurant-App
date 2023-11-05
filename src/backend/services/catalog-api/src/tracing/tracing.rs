use http::header::InvalidHeaderValue;
use http::{HeaderName, HeaderValue};
use opentelemetry::global::{tracer, ObjectSafeSpan};
use opentelemetry::trace::{SpanBuilder, SpanKind, TraceContextExt, Tracer};
use opentelemetry::{global, Context, Key, KeyValue, OrderMap, StringValue, Value};
use opentelemetry_http::{HeaderExtractor, HeaderInjector};
use rand::distributions::uniform::SampleBorrow;
use rocket::fairing::{Fairing, Info, Kind};
use rocket::{Data, Request, Response};
use std::fmt::Display;
use std::str::FromStr;
use uuid::Uuid;

pub(crate) struct Tracing;

#[rocket::async_trait]
impl Fairing for Tracing {
    fn info(&self) -> Info {
        Info {
            name: "OTEL tracing fairing",
            kind: Kind::Request | Kind::Response,
        }
    }

    async fn on_request(&self, _req: &mut Request<'_>, _data: &mut Data<'_>) {
        let req_headers = _req.headers();

        let parent_cx = global::get_text_map_propagator(|propagator| {
            let mut headers = http::HeaderMap::new();
            for h in req_headers.iter() {
                let header_name = HeaderName::from_str(h.name().as_str()).unwrap();
                let header_value = HeaderValue::from_str(h.value()).unwrap();
                headers.append(header_name, header_value);
            }

            return propagator.extract(&HeaderExtractor(&headers));
        });

        let user_agent = req_headers
            .get_one("User-Agent")
            .map(|s| s.to_owned())
            .unwrap_or_default(); // Use `unwrap_or_default` to handle the case when the header is not present.

        let request_id = req_headers
            .get_one("X-Request-Id")
            .map(|s| s.to_owned())
            .unwrap_or_else(|| Uuid::new_v4().to_string());

        let req_uri_path = _req.uri().path().to_string();
        let trace_attr: Vec<KeyValue> = vec![
            KeyValue {
                key: Key::from("http.user_agent"),
                value: Value::from(user_agent),
            },
            KeyValue {
                key: Key::from("http.request_id"),
                value: Value::from(request_id),
            },
            KeyValue {
                key: Key::from("otel.name"),
                value: Value::from(format!("{} {}", _req.method(), req_uri_path)),
            },
            KeyValue {
                key: Key::from("http.method"),
                value: Value::from(_req.method().as_str()),
            },
            KeyValue {
                key: Key::from("http.uri"),
                value: Value::from(req_uri_path),
            },
        ];

        // let _cx_guard = parent_cx.attach();

        let tracer = global::tracer("example/server");
        let span_builder = tracer
            .span_builder(format!("{} {}", _req.method(), _req.uri().path()))
            .with_attributes(trace_attr)
            .with_kind(SpanKind::Server);

        let mut span = tracer.build_with_context(span_builder, &parent_cx);
    }

    // fn on_response<'r>(&self, _req: &'r Request<'_>, _res: &mut Response<'r>) {
    //     todo!()
    // }
}
