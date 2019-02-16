param(
    [string]$Config = "Release",  
    [string]$Framework = "netcoreapp2.1",  
    [string]$SolutionName = "src/Restaurant.Server.sln",
    [string]$TestProjectFile = "src/Server/Tests/Restaurant.Server.Api.UnitTests/Restaurant.Server.Api.UnitTests.csproj"
)

function CheckLastExitCode {
    param ([int[]]$SuccessCodes = @(0), [scriptblock]$CleanupScript = $null)

    if ($SuccessCodes -notcontains $LastExitCode) {
        if ($CleanupScript) {
            "Executing cleanup script: $CleanupScript"
            &$CleanupScript
        }
        $msg = @"
                EXE RETURNED EXIT CODE $LastExitCode
                CALLSTACK:$(Get-PSCallStack | Out-String)
"@
        throw $msg
    }
}

$CoverageFolder = "coverage/"
$OpenCover = "packages/OpenCover.4.6.519/tools/OpenCover.Console.exe"
$CodeCov = "packages/Codecov.1.0.3/tools/codecov.exe"
$CodeCovToken = "e4122624-6d85-4aa3-846b-91f212a8930f"

function Invoke-InstallOpenCover () {
    Write-Host "Installing OpenCover and Codecov via nuget"
    ./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
    ./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 1.0.3 Codecov

    CheckLastExitCode
}

function Invoke-Restore ([string]$SolutionName) {
    Write-Host "Restoring $SolutionName" -ForegroundColor Green
    dotnet restore $SolutionName --no-cache

    CheckLastExitCode    
}

function Invoke-Build ([string]$SolutionName, [string]$Config) {
    Write-Host "Building $SolutionName" -ForegroundColor Green
    dotnet build $SolutionName -c $Config

    CheckLastExitCode    
}

function Invoke-Test ($Framework, $Config, $TestProjectFile) {
    Write-Host "Testing $SolutionName" -ForegroundColor Green
    dotnet test -f $Framework -c $Config $TestProjectFile
    
    CheckLastExitCode
}

function Invoke-RemoveCoverageFolder ($CoverageFolder) {   
    Write-Host "Removing coverage folder" -ForegroundColor Green
    Remove-Item -Recurse -Force $CoverageFolder -ErrorAction SilentlyContinue
    New-Item -ItemType Directory -Path $CoverageFolder 

    CheckLastExitCode
}

function Invoke-CalculateCodeCoverage ($OpenCover, $Framework, $Config, $TestProjectFile, $CoverageFolder) {
    Write-Host "Calculating code coverage with OpenCover" -ForegroundColor Green
    & $OpenCover -target:"c:\Program Files\dotnet\dotnet.exe" `
        -targetargs:"test -f $Framework -c $Config $TestProjectFile" `
        -mergeoutput `
        -hideskipped:File `
        -output:"$CoverageFolder/coverage.xml" `
        -oldStyle `
        -filter:"+[Restaurant*]* -[Restaurant.Server.Api.UnitTests*]*" `
        -searchdirs:"src/Server/Tests/Restaurant.Server.Api.UnitTests/bin/$Config/$Framework" `
        -register:user `
        -excludebyattribute:*.ExcludeFromCodeCoverage*
    
    CheckLastExitCode
}

function Invoke-UploadCodeCoverage ($CodeCov, $CoverageFolder, $CodeCovToken) {
    Write-Host "Uploading coverage results to CodeCov" -ForegroundColor Green
    & $CodeCov -f "$CoverageFolder/coverage.xml" -t $CodeCovToken
    
    CheckLastExitCode
}

Invoke-InstallOpenCover

Invoke-Restore -SolutionName $SolutionName

Invoke-Build -SolutionName $SolutionName -Config $Config

Invoke-Test -Framework $Framework -Config $Config -TestProjectFile $TestProjectFile

Invoke-RemoveCoverageFolder -CoverageFolder $CoverageFolder

Invoke-CalculateCodeCoverage -OpenCover $OpenCover -Framework $Framework -Config $Config -TestProjectFile $TestProjectFile -CoverageFolder $CoverageFolder

Invoke-UploadCodeCoverage -CodeCov $CodeCov -CoverageFolder $CoverageFolder -CodeCovToken $CodeCovToken