use rocket::serde::{Serialize, Deserialize};
use diesel::{prelude::*};
use crate::schema::category;
use rocket_okapi::{JsonSchema};

#[diesel(table_name = category)]
#[derive(Serialize, Deserialize, Debug, Queryable, JsonSchema)]
pub struct Category {
    pub id: i32,
    pub name: String,
}

#[diesel(table_name = category)]
#[derive(Serialize, Deserialize, Debug, Insertable, JsonSchema)]
pub struct NewCategory {
    pub name: String
}