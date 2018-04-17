param([string]$ProjectName = "Restaurant.Server.Api.csproj")


dotnet restore $ProjectName

dotnet ef database update -p $ProjectName