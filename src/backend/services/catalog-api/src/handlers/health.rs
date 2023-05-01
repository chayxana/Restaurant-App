use diesel::Connection;
use crate::db::db::establish_connection;

#[get("/ready")]
pub fn ready() -> &'static str {
    let connection = &mut establish_connection();
    let res = connection.begin_test_transaction();
    res.expect("begin transaction failed!");
    return "Ok";
}

#[get("/live")]
pub fn live() -> &'static str {
    return "Live"
}