FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Menu.API.csproj .
RUN dotnet restore Menu.API.csproj
COPY . .

WORKDIR /src
RUN dotnet build Menu.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Menu.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Menu.API.dll"]
