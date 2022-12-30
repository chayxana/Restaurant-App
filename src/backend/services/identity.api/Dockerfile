
FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/core/sdk:3.1.426-buster-arm64v8 AS build
WORKDIR /src
COPY Identity.API/ ./api

RUN dotnet restore api/Identity.API.csproj

COPY . .

WORKDIR /src
RUN dotnet build api/Identity.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish api/Identity.API.csproj -c Release -o /app

FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine-arm64v8 AS final
WORKDIR /app

COPY --from=publish /app .
EXPOSE 80

CMD ["dotnet", "Identity.API.dll"] 