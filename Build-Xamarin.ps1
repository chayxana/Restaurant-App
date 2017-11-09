param(
    [string]$SolutionName = "Restaurant.Client.sln"
)
function Resolve-MsBuild {
    $msb2017 = Resolve-Path "${env:ProgramFiles(x86)}\Microsoft Visual Studio\*\*\MSBuild\*\bin\msbuild.exe" -ErrorAction SilentlyContinue
    if ($msb2017) {
        Write-Host "Found MSBuild 2017 (or later)."
        Write-Host $msb2017
        return $msb2017
    }

    $msBuild2015 = "${env:ProgramFiles(x86)}\MSBuild\14.0\bin\msbuild.exe"

    if (-not (Test-Path $msBuild2015)) {
        throw 'Could not find MSBuild 2015 or later.'
    }

    Write-Host "Found MSBuild 2015."
    Write-Host $msBuild2015

    return $msBuild2015
}

function Restore-Packages([string]$solutionName) {
    Write-Host "Restoring nuget packages"
    & ./.nuget/nuget.exe restore $solutionName
}

function Build([string]$solutionName) {
    Write-Host "Building $solutionName"     
    & Resolve-MsBuild $solutionName /verbosity:minimal
}

& Restore-Packages -solutionName $SolutionName
& Build -solutionName $SolutionName