use crate::schema::catalog;
use bigdecimal::BigDecimal;
use rocket::serde::{Deserialize, Serialize};
use chrono;
use rocket_okapi::{JsonSchema};

#[derive(Debug, Clone, PartialEq, Queryable, AsChangeset)]
#[diesel(table_name = catalog)]
pub struct Catalog {
    pub id: i32,
    pub name: String,
    pub description: Option<String>,
    pub image: String,
    pub price: BigDecimal,
    pub currency: String,
    pub category: String,
    pub created_at: chrono::NaiveDateTime,
    pub updated_at: chrono::NaiveDateTime,
}

#[derive(Debug, Clone, AsChangeset)]
#[diesel(table_name = catalog)]
pub struct UpdateCatalog {
    pub name: String,
    pub description: Option<String>,
    pub image: String,
    pub price: BigDecimal,
    pub currency: String,
    pub category: Option<String>,
    pub updated_at: chrono::NaiveDateTime,
}

#[derive(Insertable)]
#[diesel(table_name = catalog)]
pub struct NewCatalog {
    pub name: String,
    pub description: Option<String>,
    pub image: String,
    pub price: BigDecimal,
    pub currency: String,
    pub category: Option<String>,
    pub created_at: chrono::NaiveDateTime,
    pub updated_at: chrono::NaiveDateTime,
}

#[derive(Serialize, Deserialize, Clone, JsonSchema)]
#[serde(crate = "rocket::serde")]
pub struct CatalogRequest {
    pub name: String,
    pub description: Option<String>,
    pub image: String,
    pub price: f64,
    pub currency: String,
    pub category: Option<String>,
}

#[derive(Serialize, Deserialize, JsonSchema)]
#[serde(crate = "rocket::serde")]
pub struct CatalogResponse {
    pub id: i32,
    pub name: String,
    pub description: Option<String>,
    pub image: String,
    pub price: f64,
    pub currency: String,
    pub category: String,
}
