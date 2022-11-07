use rocket::serde::{Serialize, Deserialize};
use diesel::{prelude::*};
use crate::schema::category;
use rocket_okapi::{JsonSchema};

#[derive(Serialize, Deserialize, Debug, Queryable, JsonSchema)]
#[diesel(table_name = category)]
pub struct Category {
    pub id: i32,
    pub name: String,
}

#[derive(Serialize, Deserialize, Debug, Insertable, JsonSchema)]
#[diesel(table_name = category)]
pub struct NewCategory {
    pub name: String
}