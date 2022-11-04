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
use rocket::fairing::AdHoc;
use rocket::fs::{relative, FileServer};
use rocket::Build;
use rocket::Rocket;

use rocket_okapi::swagger_ui::{make_swagger_ui, SwaggerUIConfig};
use rocket_okapi::{openapi, openapi_get_routes, JsonSchema};

mod db;
mod handlers;
mod models;
mod paste_id;
mod schema;
mod seeder;

pub const MIGRATIONS: EmbeddedMigrations = embed_migrations!("migrations");

async fn run_migrations(rocket: Rocket<Build>) -> Rocket<Build> {
    let connection = &mut crate::db::db::establish_connection();
    run_migrations_pending_migrations(connection).unwrap();
    Seed::seed_categories(connection).await;
    Seed::seed_catalogs(connection).await;
    rocket
}

fn run_migrations_pending_migrations(
    connection: &mut impl MigrationHarness<Pg>,
) -> Result<(), Box<dyn Error + Send + Sync + 'static>> {
    connection.run_pending_migrations(MIGRATIONS)?;
    Ok(())
}

fn get_docs() -> SwaggerUIConfig {
    use rocket_okapi::settings::UrlObject;

    SwaggerUIConfig {
        urls: vec![
            UrlObject {
                name: "catalog".to_string(),
                url: "/catalog/openapi.json".to_string(),
            },
            UrlObject {
                name: "categories".to_string(),
                url: "/categories/openapi.json".to_string(),
            },
        ],
        ..Default::default()
    }
}

#[launch]
async fn rocket() -> _ {
    println!("manifest dir: {}", env!("CARGO_MANIFEST_DIR"));

    rocket::build()
        .attach(AdHoc::on_ignite("Diesel Migrations", run_migrations))
        .mount("/", routes![catalog::index])
        .mount("/swagger", make_swagger_ui(&get_docs()))
        .mount("/categories", openapi_get_routes![category::get_categories])
        .mount(
            "/catalog",
            openapi_get_routes![
                catalog::get_catalogs,
                catalog::get_catalog,
                catalog::create,
                catalog::update,
                catalog::delete
            ],
        )
        .mount("/file", routes![upload::upload, upload::retrieve])
        .mount("/pictures", FileServer::from(relative!("pictures")))
}
