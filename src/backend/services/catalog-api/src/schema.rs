table! {
    catalog (id) {
        id -> Int4,
        name -> Varchar,
        description -> Nullable<Varchar>,
        image -> Varchar,
        price -> Numeric,
        currency -> Varchar,
        category -> Varchar,
        created_at -> Timestamp,
        updated_at -> Timestamp,
    }
}

table! {
    category (id) {
        id -> Int4,
        name -> Varchar,
    }
}