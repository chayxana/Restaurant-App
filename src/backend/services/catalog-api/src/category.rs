use crate::models::{Category, Categories, CategoriesData};

use rocket::serde::json::Json;
use rocket::State;
use std::sync::Arc;

#[get("/", format = "json")]
pub async fn get_categories(state: &State<CategoriesData>) -> Json<Categories> {
    let categories = Arc::clone(&state.data);
    let mut locked = categories.lock().await;
    let mut result: Vec<Category> = vec![];
    result.extend(locked.drain(..));
    Json(result)
}