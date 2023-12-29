use crate::models::catalog::{Catalog, CatalogRequest, CatalogResponse, NewCatalog, UpdateCatalog};

use crate::db::db::establish_connection;
use crate::schema::{self};
use bigdecimal::ToPrimitive;
use diesel::prelude::*;
use opentelemetry::global::{self, ObjectSafeSpan};
use opentelemetry::trace::Tracer;
use rocket::response::Debug;
use rocket::serde::json::Json;
use rocket_okapi::openapi;

type Result<T, E = Debug<diesel::result::Error>> = std::result::Result<T, E>;

#[openapi]
#[post("/", format = "json", data = "<catalog_create>")]
pub async fn create(catalog_create: Json<CatalogRequest>) {
    use schema::catalog::dsl::*;
    let catalog_value = catalog_create.into_inner();
    let cat = NewCatalog {
        name: catalog_value.name,
        description: catalog_value.description,
        image: catalog_value.image,
        price: bigdecimal::BigDecimal::try_from(catalog_value.price)
            .expect("failed converting f64 into BigDecimal"),
        currency: catalog_value.currency,
        category: catalog_value.category,
        updated_at: chrono::Utc::now().naive_utc(),
        created_at: chrono::Utc::now().naive_utc(),
    };

    let connection = &mut establish_connection();
    diesel::insert_into(catalog)
        .values(&cat)
        .execute(connection)
        .expect("error saving catalog");
}

#[openapi]
#[put("/<catalog_id>", format = "json", data = "<catalog_update>")]
pub fn update(catalog_id: i32, catalog_update: Json<CatalogRequest>) {
    use schema::catalog::dsl::*;

    let val = catalog_update.into_inner();
    let catalog_upd = UpdateCatalog {
        name: val.name,
        description: val.description,
        image: val.image,
        price: bigdecimal::BigDecimal::try_from(val.price)
            .expect("failed converting f64 into BigDecimal"),
        currency: val.currency,
        category: val.category,
        updated_at: chrono::Utc::now().naive_utc(),
    };

    let connection = &mut establish_connection();
    diesel::update(catalog.find(catalog_id))
        .set(&catalog_upd)
        .execute(connection)
        .expect("error updating catalog");
}

#[openapi]
#[delete("/<catalog_id>")]
pub fn delete(catalog_id: i32) {
    use schema::catalog::dsl::*;

    let connection = &mut establish_connection();
    diesel::delete(catalog.filter(id.eq(catalog_id)))
        .execute(connection)
        .expect("deleting catalog failed");
}

#[openapi]
#[get("/all?<category_name>", format = "json")]
pub fn get_catalogs(category_name: Option<String>) -> Result<Json<Vec<CatalogResponse>>> {
    use schema::catalog::dsl::*;
    let connection = &mut establish_connection();

    let mut query = catalog.into_boxed();
    if let Some(ref cat_name) = category_name {
        query = query.filter(category.eq(cat_name));
    }
    let res = query
        .load::<Catalog>(connection)
        .expect("failed to loading catalogs")
        .into_iter()
        .map(|c| CatalogResponse {
            id: c.id,
            name: c.name,
            description: c.description,
            image: c.image,
            price: c.price.to_f64().unwrap(),
            currency: c.currency,
            category: c.category,
        })
        .collect();
    return Ok(Json(res));
}

#[openapi]
#[get("/<catalog_id>", format = "json")]
pub fn get_catalog(catalog_id: i32) -> Result<Json<CatalogResponse>> {
    use schema::catalog::dsl::*;

    let connection = &mut establish_connection();
    let c = catalog
        .filter(id.eq(catalog_id))
        .first::<Catalog>(connection)
        .expect("failed to loading catalogs");

    let res = CatalogResponse {
        id: c.id,
        name: c.name,
        description: c.description,
        image: c.image,
        price: c.price.to_f64().unwrap(),
        currency: c.currency,
        category: c.category,
    };

    return Ok(Json(res));
}
