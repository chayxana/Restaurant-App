#[macro_use]
extern crate rocket;

use crate::models::{CatalogsData};

mod catalog;
mod models;

#[launch]
fn rocket() -> _ {
    let catalogs = CatalogsData::new();
    rocket::build()
        .manage(catalogs)
        .mount("/", routes![catalog::index])
        .mount("/catalog", routes![catalog::create, catalog::update, catalog::delete])
        .mount("/catalogs", routes![catalog::get_catalogs])
}
