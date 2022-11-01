use crate::models::category::{Category};

use rocket::serde::json::Json;
use rocket::response::{Debug};
use rocket_okapi::{openapi};

use crate::db::db::establish_connection;
use crate::schema::{self};

use diesel::{prelude::*};

type Result<T, E = Debug<diesel::result::Error>> = std::result::Result<T, E>;

#[openapi]
#[get("/", format = "json")]
pub fn get_categories() -> Result<Json<Vec<Category>>> {
    use schema::category::dsl::*;

    let connection = &mut establish_connection();
    let res = category
    .load::<Category>(connection)
    .expect("failed to loading catalogs");

    return Ok(Json(res));
}