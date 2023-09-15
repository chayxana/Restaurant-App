FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY ./identity-api-release ./

EXPOSE 80
CMD ["dotnet", "Identity.API.dll"]