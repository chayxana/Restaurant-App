#[macro_use]
extern crate rocket;

use rocket::fs::{FileServer, relative};
use crate::models::{CatalogsData, CategoriesData};

mod catalog;
mod models;
mod paste_id;
mod seed;
mod upload;
mod category;

#[launch]
async fn rocket() -> _ {
    println!("manifest dir: {}", env!("CARGO_MANIFEST_DIR"));

    let categories = seed::Seed::get_categories().await;
    let dishes = seed::Seed::get_dishes(&categories).await;
    let catalogs = CatalogsData::new(dishes);
    let category_data = CategoriesData::new(categories);
    
    rocket::build()
        .manage(catalogs)
        .manage(category_data)
        .mount("/", routes![catalog::index])
        .mount("/categories", routes![category::get_categories])
        .mount(
            "/catalog",
            routes![catalog::create, catalog::update, catalog::delete],
        )
        .mount("/catalogs", routes![catalog::get_catalogs])
        .mount("/file", routes![upload::upload, upload::retrieve])
        .mount("/pictures", FileServer::from(relative!("pictures")))
}