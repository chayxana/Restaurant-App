FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51353
EXPOSE 44336

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Server/Services/Menu/Menu.API/Menu.API.csproj Server/Services/Menu/Menu.API/
RUN dotnet restore Server/Services/Menu/Menu.API/Menu.API.csproj
COPY . .
WORKDIR /src/Server/Services/Menu/Menu.API
RUN dotnet build Menu.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Menu.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Menu.API.dll"]
