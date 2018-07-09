FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51424
EXPOSE 44311

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Server/Services/Basket/Basket.API/Basket.API.csproj Server/Services/Basket/Basket.API/
RUN dotnet restore Server/Services/Basket/Basket.API/Basket.API.csproj
COPY . .
WORKDIR /src/Server/Services/Basket/Basket.API
RUN dotnet build Basket.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Basket.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Basket.API.dll"]
