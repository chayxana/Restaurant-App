
FROM microsoft/dotnet:2.2-sdk-alpine AS build
WORKDIR /src
COPY Identity.API/ ./api
COPY Identity.API.UnitTests/ ./test

RUN dotnet restore api/Identity.API.csproj
RUN dotnet restore test/Identity.API.UnitTests.csproj

COPY . .

WORKDIR /src
RUN dotnet build api/Identity.API.csproj -c Release -o /app
RUN dotnet build test/Identity.API.UnitTests.csproj -c Release -o /apptest

FROM build AS publish
RUN dotnet publish api/Identity.API.csproj -c Release -o /app
RUN dotnet publish test/Identity.API.UnitTests.csproj -c Release -o /apptest

FROM build AS final
WORKDIR /app

COPY --from=publish /app .
COPY --from=publish /apptest .
EXPOSE 80

CMD ["dotnet", "Identity.API.dll"] 