param(
    [string]$SolutionName = "Restaurant.Client.sln",
    [string]$Framework = "netcoreapp2.0",      
    [string]$Configuration = "Release",
    [string]$TestProjectFile = "Client/Tests/Restaurant.Core.UnitTests/Restaurant.Core.UnitTests.csproj"
)

$CoverageFolder = "coveragexamarin/"
$OpenCover = "packages/OpenCover.4.6.519/tools/OpenCover.Console.exe"
$CodeCov = "packages/Codecov.1.0.3/tools/codecov.exe"
$CodeCovToken = "077aea61-7f42-49aa-b825-56c1e7c7095b"

function Invoke-InstallOpenCover () {
    Write-Host "Installing OpenCover and Codecov via nuget"
    ./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
    ./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 1.0.3 Codecov

    CheckLastExitCode
}

function CheckLastExitCode {
    param ([int[]]$SuccessCodes = @(0), [scriptblock]$CleanupScript = $null)

    if ($SuccessCodes -notcontains $LastExitCode) {
        if ($CleanupScript) {
            "Executing cleanup script: $CleanupScript"
            &$CleanupScript
        }
        $msg = 
        @"
            EXE RETURNED EXIT CODE $LastExitCode
            CALLSTACK:$(Get-PSCallStack | Out-String)
"@
        throw $msg
    }
}

function Resolve-MsBuild {
    $msb2017 = Resolve-Path "${env:ProgramFiles(x86)}\Microsoft Visual Studio\*\*\MSBuild\*\bin\msbuild.exe" -ErrorAction SilentlyContinue
    if ($msb2017) {
        return $msb2017
    }

    $msBuild2015 = "${env:ProgramFiles(x86)}\MSBuild\14.0\bin\msbuild.exe"

    if (-not (Test-Path $msBuild2015)) {
        throw 'Could not find MSBuild 2015 or later.'
    }

    return $msBuild2015
}

function Invoke-RemoveCoverageFolder ($CoverageFolder) {   
    Write-Host "Removing coverage folder" -ForegroundColor Green
    Remove-Item -Recurse -Force $CoverageFolder -ErrorAction SilentlyContinue
    New-Item -ItemType Directory -Path $CoverageFolder 

    CheckLastExitCode
}

function Invoke-Restore([string]$SolutionName) {
    Write-Host -ForegroundColor Green "Restoring nuget packages for $SolutionName"
    & ./.nuget/nuget.exe restore $SolutionName

    CheckLastExitCode
}

function Invoke-Build([string]$SolutionName, $Configuration) {
    Write-Host -ForegroundColor Green "Building $SolutionName"
    $msBuild = Resolve-MsBuild
    & $msBuild $SolutionName /verbosity:minimal /p:Configuration=$Configuration /t:Rebuild
    
    CheckLastExitCode
    
}

function Invoke-Test([string]$Configuration, $TestProjectFile) {
    & dotnet test -f "netcoreapp2.0" -c $Configuration $TestProjectFile

    CheckLastExitCode    
}

function Invoke-CalculateCodeCoverage ($OpenCover, $Framework, $Config, $TestProjectFile, $CoverageFolder) {
    Write-Host "Calculating code coverage with OpenCover" -ForegroundColor Green
    & $OpenCover -target:"c:\Program Files\dotnet\dotnet.exe" -targetargs:"test -f $Framework -c $Config $TestProjectFile" -mergeoutput -hideskipped:File -output:"$CoverageFolder/coverage.xml" -oldStyle -filter:"+[Restaurant.Core*]* -[Restaurant.Core.UnitTests*]*" -searchdirs:"Client/Tests/Restaurant.Core.UnitTests/bin/$Config/$Framework" -register:user -excludebyattribute:*.ExcludeFromCodeCoverage*
    
    CheckLastExitCode
}

function Invoke-UploadCodeCoverage ($CodeCov, $CoverageFolder, $CodeCovToken) {
    Write-Host "Uploading coverage results to CodeCov" -ForegroundColor Green
    & $CodeCov -f "$CoverageFolder/coverage.xml" -t $CodeCovToken
    
    CheckLastExitCode
}


Invoke-InstallOpenCover

Invoke-Restore -SolutionName $SolutionName

Invoke-Build -SolutionName $SolutionName -Configuration $Configuration

Invoke-Test -TestProjectFile $TestProjectFile -Configuration $Configuration

Invoke-RemoveCoverageFolder -CoverageFolder $CoverageFolder

Invoke-CalculateCodeCoverage -OpenCover $OpenCover -Framework $Framework -Config $Configuration -TestProjectFile $TestProjectFile -CoverageFolder $CoverageFolder

Invoke-UploadCodeCoverage -CodeCov $CodeCov -CoverageFolder $CoverageFolder -CodeCovToken $CodeCovToken
