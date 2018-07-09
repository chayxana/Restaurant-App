FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50423
EXPOSE 44304

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Server/Services/Ordering/Ordering.API/Ordering.API.csproj Server/Services/Ordering/Ordering.API/
RUN dotnet restore Server/Services/Ordering/Ordering.API/Ordering.API.csproj
COPY . .
WORKDIR /src/Server/Services/Ordering/Ordering.API
RUN dotnet build Ordering.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Ordering.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
