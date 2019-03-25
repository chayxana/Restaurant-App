FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /src

COPY Menu.API/ ./api
# COPY Menu.API.UnitTests/ ./test
RUN dotnet restore api/Menu.API.csproj
# RUN dotnet restore test/Menu.API.UnitTests.csproj

COPY . .

RUN dotnet build api/Menu.API.csproj -c Release -o /app
# RUN dotnet build test/Menu.API.UnitTests.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish api/Menu.API.csproj -c Release -o /app
# RUN dotnet publish test/Menu.API.UnitTests.csproj -c Release -o /apptest
# RUN cp test/code_coverage.sh /apptest

FROM build AS final
WORKDIR /app
# ENV PATH="${PATH}:/root/.dotnet/tools"
# RUN dotnet tool install --global coverlet.console --version 1.2.1.0
# RUN dotnet tool install --global dotnet-reportgenerator-globaltool

COPY --from=publish /app .
# COPY --from=publish /apptest .
EXPOSE 80

#RUN ["chmod", "+x", "code_coverage.sh"]

CMD ["dotnet", "Menu.API.dll"]