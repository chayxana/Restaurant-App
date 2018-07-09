FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50369
EXPOSE 44300

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Server/Services/Identity/Identity.API/Identity.API.csproj Server/Services/Identity/Identity.API/
RUN dotnet restore Server/Services/Identity/Identity.API/Identity.API.csproj
COPY . .
WORKDIR /src/Server/Services/Identity/Identity.API
RUN dotnet build Identity.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Identity.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
