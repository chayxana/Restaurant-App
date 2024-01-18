use rocket::tokio::fs;

use std::path::{Path, PathBuf};

use crate::models::catalog::NewCatalog;
use crate::schema;
use crate::models;

use diesel::prelude::*;

pub struct Seed;

impl Seed {
    pub async fn seed_catalogs(connection: &mut PgConnection, base_url: &String) {
        use schema::catalog::dsl::*;
    
        let categories = Seed::get_categories().await;
        let dishes = Seed::get_dishes(&categories, base_url).await;
    
        let catalogs = catalog
            .limit(1)
            .load::<models::catalog::Catalog>(connection)
            .expect("failed to loading catalogs");
    
        if catalogs.len() > 0 {
            return;
        }
    
        for cat in dishes {
            diesel::insert_into(catalog)
                .values(&cat)
                .execute(connection)
                .expect("error saving catalog");
        }
    }
    
    pub async fn seed_categories(connection: &mut PgConnection) {
        use schema::category::dsl::*;
    
        let existing_categories = category
            .limit(1)
            .load::<models::category::Category>(connection)
            .expect("failed to load categories");
    
        if existing_categories.len() > 0 {
            return;
        }
    
        let categories = Seed::get_categories().await;
        for name_str in categories {
            diesel::insert_into(category)
                .values(models::category::NewCategory { name: name_str })
                .execute(connection)
                .expect("error saving category");
        }
    }

    async fn get_categories() -> Vec<String> {
        let contents = fs::read(file_path(String::from("categories.csv")))
            .await
            .unwrap();

        let mut v = String::from_utf8_lossy(&contents)
            .split(",")
            .collect::<Vec<_>>()
            .iter()
            .map(|x| x.to_string())
            .collect::<Vec<_>>();
        let mut result: Vec<String> = vec![];
        result.extend(v.drain(..));
        result
    }

    async fn get_dishes(categories: &Vec<String>, base_url: &String) -> Vec<NewCatalog> {
        let contents = fs::read(file_path(String::from("dishes.csv")))
            .await
            .unwrap();

        let mut v = String::from_utf8_lossy(&contents)
            .lines()
            .skip(1)
            .map(|x| x.split(","))
            .map(|i| get_dish(i.collect::<Vec<_>>(), categories, base_url))
            .collect::<Vec<_>>();

        let mut result: Vec<NewCatalog> = vec![];
        result.extend(v.drain(..));
        result
    }
}

fn get_dish(columns: Vec<&str>, categories: &Vec<String>, base_url: &String) -> NewCatalog {
    NewCatalog {
        name: columns[2].to_string(),
        description: Some(columns[3].to_string().replace(":", ",")),
        image: format!("{}/pictures/{}.jpg", base_url, columns[0].to_string()),
        price: columns[4].parse::<bigdecimal::BigDecimal>().unwrap(),
        currency: "USD".to_string(),
        category: categories
            .iter()
            .find(|x| x.to_string() == columns[1])
            .map(|x| x.to_string()),
            updated_at: chrono::Utc::now().naive_utc(),
            created_at: chrono::Utc::now().naive_utc(), 
    }
}

pub fn file_path(file: String) -> PathBuf {
    let root = concat!(env!("CARGO_MANIFEST_DIR"), "/", "data");
    Path::new(root).join(file)
}
