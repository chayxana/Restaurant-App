use crate::models::{Catalog, Catalogs, CatalogsData};

use rocket::serde::json::Json;
use rocket::State;
use std::sync::Arc;

#[get("/")]
pub fn index() -> &'static str {
    "Hello, world!"
}

#[post("/", format = "json", data = "<catalog>")]
pub async fn create(catalog: Json<Catalog>, state: &State<CatalogsData>) {
    let lock = state.inner();
    lock.data.lock().await.push(catalog.into_inner());
}

#[put("/catalogs/<id>", format = "json", data = "<catalog>")]
pub async fn update(id: &str, catalog: Json<Catalog>, state: &State<CatalogsData>) {
    let mut catalogs = state.inner().data.lock().await;
    let index = catalogs.iter().position(|x| x.id == id).unwrap();
    catalogs[index] = catalog.into_inner();
}

#[delete("/<id>")]
pub async fn delete(id: &str, state: &State<CatalogsData>) {
    let mut catalogs = state.inner().data.lock().await;

    let index = catalogs.iter().position(|x| x.id == id).unwrap();
    catalogs.remove(index);
}

#[get("/", format = "json")]
pub async fn get_catalogs(state: &State<CatalogsData>) -> Json<Catalogs> {
    let catalogs = Arc::clone(&state.data);
    let mut locked = catalogs.lock().await;
    let mut result: Vec<Catalog> = vec![];
    result.extend(locked.drain(..));
    Json(result)
}
