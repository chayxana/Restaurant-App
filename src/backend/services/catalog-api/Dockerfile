FROM rust:1.72.1 as builder

# this useed for caching dependencies 
# so basically it generates empty rust app with empty main.rs after that we copy catalog-api's Cargo.toml and download dependencies
# only after downloading we copy rest of files(*.rs and other files)
WORKDIR /usr/src

# Create blank project
RUN USER=root cargo new catalog-api

WORKDIR /usr/src/catalog-api

# We want dependencies cached, so copy those first.
COPY Cargo.toml Cargo.lock /usr/src/catalog-api/

RUN cargo build --release

COPY . .
RUN touch src/main.rs
RUN cargo build --release

FROM ubuntu:22.04

RUN apt update
RUN apt-get remove libpq5
RUN apt-get install libpq-dev -y

WORKDIR /usr/src/catalog-api

COPY --from=builder /usr/src/catalog-api/target/release/catalog-api ./catalog-api
COPY --from=builder /usr/src/catalog-api/data ./data
COPY --from=builder /usr/src/catalog-api/pictures ./pictures
COPY --from=builder /usr/src/catalog-api/migrations ./migrations

ENTRYPOINT ["./catalog-api"]
