-- Your SQL goes here
CREATE TABLE catalog (
    id SERIAL PRIMARY KEY,
    name VARCHAR NOT NULL,
    description VARCHAR,
    image VARCHAR NOT NULL,
    price DECIMAL NOT NULL,
    currency VARCHAR NOT NULL,
    category VARCHAR NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP NOT NULL DEFAULT NOW()
);