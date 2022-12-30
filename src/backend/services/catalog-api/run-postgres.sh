docker run -d -p 5433:5432 \
    -e POSTGRES_USER=admin \
    -e POSTGRES_PASSWORD=Passw0rd! \
    -e POSTGRES_DB=catalogdb \
    postgres:alpine