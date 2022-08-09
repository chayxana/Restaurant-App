use rocket::tokio::fs;
use uuid::Uuid;
use std::path::{Path, PathBuf};

use crate::models::{Catalog};

pub struct Seed;

impl Seed {
    pub async fn get_categories() -> Vec<String> {
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

    pub async fn get_dishes(categories: &Vec<String>) -> Vec<Catalog> {
        let contents = fs::read(file_path(String::from("dishes.csv")))
            .await
            .unwrap();

        let mut v = String::from_utf8_lossy(&contents)
            .lines()
            .skip(1)
            .map(|x| x.split(","))
            .map(|i| get_dish(i.collect::<Vec<_>>(), categories))
            .collect::<Vec<_>>();

        let mut result: Vec<Catalog> = vec![];
        result.extend(v.drain(..));
        result
    }
}

fn get_dish(columns: Vec<&str>, categories: &Vec<String>) -> Catalog {
    Catalog {
        id: Uuid::new_v4().to_string(),
        name: columns[2].to_string(),
        description: columns[3].to_string().replace(":", ","),
        image: format!("/pictures/{}.jpg", columns[0].to_string()),
        price: columns[4].parse::<f32>().unwrap(),
        currency: "USD".to_string(),
        category: categories
            .iter()
            .find(|x| x.to_string() == columns[1])
            .map(|x| x.to_string()),
    }
}

pub fn file_path(file: String) -> PathBuf {
    let root = concat!(env!("CARGO_MANIFEST_DIR"), "/", "data");
    Path::new(root).join(file)
}
