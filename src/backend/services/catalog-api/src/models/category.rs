use rocket::serde::{Serialize, Deserialize};
use diesel::{prelude::*};
use crate::schema::category;

#[diesel(table_name = category)]
#[derive(Serialize, Deserialize, Debug, Queryable)]
pub struct Category {
    pub id: i32,
    pub name: String,
}

#[diesel(table_name = category)]
#[derive(Serialize, Deserialize, Debug, Insertable)]
pub struct NewCategory {
    pub name: String
}