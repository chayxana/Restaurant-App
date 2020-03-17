FROM jurabek/dotnet-sdk:2.2-alpine

WORKDIR /src
COPY Menu.API/Menu.API.csproj src/Menu.API/Menu.API.csproj
COPY Menu.API/Menu.API.csproj src/Menu.API/Menu.API.csproj