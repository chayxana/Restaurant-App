
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS build
WORKDIR /src
COPY Identity.API/ ./api

RUN dotnet restore api/Identity.API.csproj

COPY . .

WORKDIR /src
RUN dotnet build api/Identity.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish api/Identity.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS final
WORKDIR /app

COPY --from=publish /app .
EXPOSE 80

CMD ["dotnet", "Identity.API.dll"] 