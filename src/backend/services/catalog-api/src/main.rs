#[macro_use]
extern crate rocket;

#[macro_use]
extern crate diesel;

use std::error::Error;

use crate::handlers::catalog;
use crate::handlers::category;
use crate::handlers::upload;
use crate::seeder::seed::Seed;
use diesel::pg::Pg;
use diesel_migrations::{embed_migrations, EmbeddedMigrations, MigrationHarness};
use dotenvy::dotenv;
use okapi::openapi3::Info;
use okapi::openapi3::OpenApi;
use rocket::fairing::AdHoc;
use rocket::fs::{relative, FileServer};
use rocket::Build;
use rocket::Rocket;
use rocket_okapi::mount_endpoints_and_merged_docs;
use rocket_okapi::openapi_get_routes_spec;
use rocket_okapi::settings::OpenApiSettings;
use std::env;

use rocket_okapi::swagger_ui::{make_swagger_ui, SwaggerUIConfig};

mod db;
mod handlers;
mod models;
mod paste_id;
mod schema;
mod seeder;

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

    let base_url = env::var("BASE_URL").unwrap_or_else(|_| "/".to_string());
    
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

    rocket
        .attach(AdHoc::on_ignite("Diesel Migrations", run_migrations))
        .mount(&base_url, routes![catalog::index])
        .mount(
            format!("{}{}", &base_url, "/swagger/"),
            make_swagger_ui(&SwaggerUIConfig {
                url: open_api_json_url,
                ..Default::default()
            }),
        )
        .mount(format!("{}{}", &base_url, "/file"), routes![upload::upload, upload::retrieve])
        .mount(format!("{}{}", &base_url, "/pictures"), FileServer::from(relative!("pictures")))
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
