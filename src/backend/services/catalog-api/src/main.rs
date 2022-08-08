#[macro_use]
extern crate rocket;

use crate::models::{CatalogsData, CategoriesData};

mod catalog;
mod models;
mod paste_id;
mod seed;
mod upload;

#[launch]
async fn rocket() -> _ {
    let categories = seed::Seed::get_categories().await;
    // print!("{:?}", categories);

    let dishes = seed::Seed::get_dishes(&categories).await;
    print!("Len {:?}", dishes);

    let catalogs = CatalogsData::new();
    let category_data = CategoriesData::new(categories);

    rocket::build()
        .manage(catalogs)
        .manage(category_data)
        .mount("/", routes![catalog::index])
        .mount(
            "/catalog",
            routes![catalog::create, catalog::update, catalog::delete],
        )
        .mount("/catalogs", routes![catalog::get_catalogs])
        .mount("/upload", routes![upload::upload])
        .mount("/retrieve", routes![upload::retrieve])
}
