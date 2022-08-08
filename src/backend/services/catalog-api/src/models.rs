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
pub struct Catalog {
    pub id: String,
    pub name: String,
    pub description: String,
    pub image: String,
    pub price: f32,
    pub currency: String,
    pub category: Option<String>,
}
pub type Catalogs = Vec<Catalog>;

#[derive(Serialize, Deserialize, Debug)]
pub struct Category {
    pub id: String,
    pub name: String,
}

pub type Categories = Vec<Category>;

pub struct CategoriesData {
    pub data: Arc<Mutex<Categories>>,
}

impl CategoriesData {
    pub fn new(items: Vec<String>) -> CategoriesData {
        let mut vec: Vec<Category> = Categories::new();
        vec.extend(items.into_iter().map(|x| Category {
            id: "".to_string(),
            name: x,
        }));

        CategoriesData {
            data: Arc::new(Mutex::new(vec))
        }
    }
}

pub struct CatalogsData {
    pub data: Arc<Mutex<Catalogs>>,
}

impl CatalogsData {
    pub fn new(items: Vec<Catalog>) -> CatalogsData {
        let mut vec: Vec<Catalog> = Catalogs::new();
        vec.extend(items);
        CatalogsData {
            data: Arc::new(Mutex::new(vec))
        }
    }
}

