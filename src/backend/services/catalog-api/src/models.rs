use std::sync::Arc;
use rocket::tokio::sync::{Mutex};
use rocket::serde::{Serialize, Deserialize};

#[derive(Serialize, Deserialize)]
pub struct CatalogReq {
    pub name: String,
    pub description: Option<String>,
    pub image: String,
    pub price: f32,
    pub currency: String,
}

#[derive(Serialize, Deserialize, Debug)]
#[serde(crate = "rocket::serde")]
pub struct CatalogIn {
    pub id: String,
    pub name: String,
}
pub type Catalogs = Vec<CatalogIn>;

pub struct CatalogsData {
    pub data: Arc<Mutex<Catalogs>>,
}

impl CatalogsData {
    pub fn new() -> CatalogsData {
        let vec: Vec<CatalogIn> = Catalogs::new();
        CatalogsData {
            data: Arc::new(Mutex::new(vec))
        }
    }
}