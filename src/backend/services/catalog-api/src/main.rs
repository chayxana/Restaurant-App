#[macro_use]
extern crate rocket;

#[macro_use]
extern crate diesel;

use std::error::Error;

use crate::handlers::category;
use crate::handlers::upload;
use crate::handlers::{catalog, health};
use crate::seeder::seed::Seed;
use diesel::pg::Pg;
use diesel_migrations::{embed_migrations, EmbeddedMigrations, MigrationHarness};
use dotenvy::dotenv;
use okapi::openapi3::Info;
use okapi::openapi3::OpenApi;
use opentelemetry::global;
use opentelemetry::sdk::propagation::TraceContextPropagator;
use opentelemetry::sdk::trace::Config;
use opentelemetry::sdk::trace::TracerProvider;
use opentelemetry::sdk::Resource;
use opentelemetry::KeyValue;
use opentelemetry_otlp::{ExportConfig, SpanExporter, TonicConfig};
use rocket::fairing::AdHoc;
use rocket::fs::{relative, FileServer};
use rocket::Build;
use rocket::Rocket;
use rocket_okapi::mount_endpoints_and_merged_docs;
use rocket_okapi::openapi_get_routes_spec;
use rocket_okapi::settings::OpenApiSettings;
use std::env;
// use opentelemetry_stdout::SpanExporter;

use rocket_okapi::swagger_ui::{make_swagger_ui, SwaggerUIConfig};

mod db;
mod handlers;
mod models;
mod paste_id;
mod schema;
mod seeder;
mod tracing;

pub const MIGRATIONS: EmbeddedMigrations = embed_migrations!("migrations");

async fn run_migrations(rocket: Rocket<Build>) -> Rocket<Build> {
    let base_url = env::var("BASE_URL").unwrap_or_default();
    let connection = &mut crate::db::db::establish_connection();
    
    run_migrations_pending_migrations(connection).unwrap();
    Seed::seed_categories(connection).await;
    Seed::seed_catalogs(connection, &base_url).await;
    rocket
}

fn run_migrations_pending_migrations(
    connection: &mut impl MigrationHarness<Pg>,
) -> Result<(), Box<dyn Error + Send + Sync + 'static>> {
    connection.run_pending_migrations(MIGRATIONS)?;
    Ok(())
}

#[launch]
async fn rocket() -> _ {
    println!("manifest dir: {}", env!("CARGO_MANIFEST_DIR"));
    dotenv().ok();

    init_tracer();

    let base_url = env::var("BASE_URL").unwrap_or_else(|_| "/".to_string());
    println!("base_url: {}", base_url);

    let open_api_json_url = if base_url == "/" {
        "../openapi.json".to_string()
    } else {
        format!("../..{}/openapi.json", base_url)
    };

    let mut rocket = rocket::build();
    let settings = OpenApiSettings::default();
    mount_endpoints_and_merged_docs! {
        rocket, base_url.to_owned(), settings,
        base_url.to_owned() => (vec![], custom_openapi_spec()),
        "/items".to_string() => openapi_get_routes_spec!(
            settings:
            catalog::get_catalogs,
            catalog::get_catalog,
            catalog::create,
            catalog::update,
            catalog::delete
        ),
        "/categories".to_string() => openapi_get_routes_spec!(
            settings:
            category::get_categories
        )
    };
    let tracing_fairing = tracing::tracing::Tracing {};

    rocket
        .attach(tracing_fairing)
        .attach(AdHoc::on_ignite("Diesel Migrations", run_migrations))
        .mount("/health", routes![health::ready, health::live])
        .mount(
            format!("{}{}", &base_url, "/swagger/"),
            make_swagger_ui(&SwaggerUIConfig {
                url: open_api_json_url,
                ..Default::default()
            }),
        )
        .mount(
            format!("{}{}", &base_url, "/file"),
            routes![upload::upload, upload::retrieve],
        )
        .mount(
            format!("{}{}", &base_url, "/pictures"),
            FileServer::from(relative!("pictures")),
        )
}

fn init_tracer() {
    global::set_text_map_propagator(TraceContextPropagator::new());
    let mut export_cfg = ExportConfig::default();
    export_cfg.endpoint =
        env::var("OTEL_EXPORTER_OTLP_ENDPOINT").unwrap_or_else(|_| export_cfg.endpoint);

    match SpanExporter::new_tonic(export_cfg, TonicConfig::default()) {
        Ok(exporter) => {
            let cfg = opentelemetry::sdk::trace::config().with_resource(Resource::new(vec![
                KeyValue::new(
                    opentelemetry_semantic_conventions::resource::SERVICE_NAME,
                    "catalog-api",
                ),
            ]));

            let provider = TracerProvider::builder()
                .with_config(cfg)
                .with_simple_exporter(exporter)
                .build();
            
            global::set_tracer_provider(provider);
        }
        Err(why) => panic!("{:?}", why),
    }
}

fn custom_openapi_spec() -> OpenApi {
    OpenApi {
        openapi: OpenApi::default_version(),
        info: Info {
            description: Some("Docs".to_string()),
            ..Default::default()
        },
        ..Default::default()
    }
}
