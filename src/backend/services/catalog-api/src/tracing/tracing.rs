// use http::{HeaderName, HeaderValue};
// use opentelemetry::global::{tracer, ObjectSafeSpan};
// use opentelemetry::propagation::TextMapPropagator;
// use opentelemetry::trace::{get_active_span, SpanBuilder, SpanKind, TraceContextExt, Tracer, mark_span_as_active, Status, Span};
// use opentelemetry::{global, Context, Key, KeyValue, OrderMap, StringValue, Value};
// use opentelemetry_http::{HeaderExtractor, HeaderInjector};
// use opentelemetry_contrib::trace::propagator::trace_context_response::TraceContextResponsePropagator;
// use rocket::fairing::{Fairing, Info, Kind};
// use rocket::{Data, Request, Response};
// use std::borrow::Cow;
// use std::str::FromStr;
// use uuid::Uuid;

// pub(crate) struct Tracing;

// #[rocket::async_trait]
// impl Fairing for Tracing {
//     fn info(&self) -> Info {
//         Info {
//             name: "OTEL tracing fairing",
//             kind: Kind::Request | Kind::Response,
//         }
//     }

//     async fn on_request(&self, _req: &mut Request<'_>, _data: &mut Data<'_>) {
//     }

//     async fn on_response<'r>(&self, _req: &'r Request<'_>, _res: &mut Response<'r>) {
//         println!("on_request");
//         let req_headers = _req.headers();

//         let parent_cx = global::get_text_map_propagator(|propagator| {
//             let mut headers = http::HeaderMap::new();
//             for h in req_headers.iter() {
//                 let header_name = HeaderName::from_str(h.name().as_str()).unwrap();
//                 let header_value = HeaderValue::from_str(h.value()).unwrap();
//                 headers.append(header_name, header_value);
//             }

//             return propagator.extract(&HeaderExtractor(&headers));
//         });

//         let _cx_guard = parent_cx.attach();
//         let user_agent = req_headers
//             .get_one("User-Agent")
//             .map(|s| s.to_owned())
//             .unwrap_or_default(); // Use `unwrap_or_default` to handle the case when the header is not present.

//         let request_id = req_headers
//             .get_one("X-Request-Id")
//             .map(|s| s.to_owned())
//             .unwrap_or_else(|| Uuid::new_v4().to_string());

//         let req_uri_path = _req.uri().path().to_string();
//         let trace_attr: Vec<KeyValue> = vec![
//             KeyValue::new("http.user_agent", user_agent),
//             KeyValue::new("http.request_id", request_id),
//             KeyValue::new("otel.name", format!("{} {}", _req.method(), req_uri_path)),
//             KeyValue::new("http.method", _req.method().as_str()),
//             KeyValue::new("http.uri", req_uri_path),
//             KeyValue::new("http.status", _res.status().to_string())
//         ];
//         let mut status= Status::Ok;
//         if _res.status().code/100 == 5 {
//             status = Status::error(_res.status().to_string());
//         }
//         let tracer = global::tracer("rocket/response_handler");
//         let span = tracer
//             .span_builder(format!("{} {}", _req.method(), _req.uri().path()))
//             .with_attributes(trace_attr)
//             .with_kind(SpanKind::Server)
//             .with_status(status)
//             .start(&tracer);
//         let cx = Context::current_with_span(span);
//         cx.span().add_event("handling request", Vec::new());

//         // let response_propagator: &dyn TextMapPropagator = &TraceContextResponsePropagator::new();
//         // response_propagator.inject_context(&cx, &mut HeaderInjector(res.headers_mut()));

//     }
// }
