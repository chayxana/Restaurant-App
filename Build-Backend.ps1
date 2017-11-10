param(
    [string]$Config = "Release",  
    [string]$Framework = "netcoreapp2.0",  
    [string]$SolutionName = "Restaurant.Server.sln",
    [string]$TestProjectFile = "Server/Tests/Restaurant.Server.Api.UnitTests/Restaurant.Server.Api.UnitTests.csproj",
    [string]$OpenCover = "packages/OpenCover.4.6.519/tools/OpenCover.Console.exe",
    [string]$CodeCov = "packages/Codecov.1.0.3/tools/codecov.exe",
    [string]$CodeCovToken = "077aea61-7f42-49aa-b825-56c1e7c7095b"
)

$CoverageFolder = "coverage/"

Write-Host "Installing OpenCover and Codecov via nuget"
./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 1.0.3 Codecov

Write-Host "Restoring $SolutionName"
dotnet restore $SolutionName --no-cache

Write-Host "Building $SolutionName"
dotnet build $SolutionName -c $Config

Write-Host "Testing $SolutionName"
dotnet test -f $Framework -c $Config $TestProjectFile

Write-Host "Removing coverage folder"
Remove-Item $CoverageFolder -Recurse -ErrorAction Ignore
New-Item -ItemType Directory -Path $CoverageFolder

Write-Host "Calculating coverage with OpenCover"

& $OpenCover -target:"c:\Program Files\dotnet\dotnet.exe" -targetargs:"test -f $Framework -c $Config $TestProjectFile" -mergeoutput -hideskipped:File -output:"$CoverageFolder/coverage.xml" -oldStyle -filter:"+[Restaurant.Server.Api*]* -[Restaurant.Server.Api.UnitTests*]*" -searchdirs:"Server/Tests/Restaurant.Server.Api.UnitTests/bin/$Config/$Framework" -register:user -excludebyattribute:*.ExcludeFromCodeCoverage*

Write-Host "Uploading coverage results to CodeCov"
& $CodeCov -f "$CoverageFolder/coverage.xml" -t $CodeCovToken
