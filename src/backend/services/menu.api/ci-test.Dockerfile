FROM jurabek/dotnet-sdk:3.1-alpine

WORKDIR /src
COPY Menu.API/Menu.API.csproj src/Menu.API/Menu.API.csproj