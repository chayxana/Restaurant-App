FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Menu.API/ ./api
COPY Menu.API.UnitTests/ ./test

RUN dotnet restore api/Menu.API.csproj
RUN dotnet restore test/Menu.API.UnitTests.csproj

COPY . .

WORKDIR /src
RUN dotnet build api/Menu.API.csproj -c Release -o /app
RUN dotnet build test/Menu.API.UnitTests.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish api/Menu.API.csproj -c Release -o /app
RUN dotnet publish test/Menu.API.UnitTests.csproj -c Release -o /apptest

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=publish /apptest .
ENTRYPOINT ["dotnet"] 
CMD ["Menu.API.dll"]

EXPOSE 5100
