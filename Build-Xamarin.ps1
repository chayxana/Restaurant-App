param(
    [string]$SolutionName = "Restaurant.Client.sln",
    [string]$Configuration = "Release",
    [string]$TestProjectFile = "Client/Tests/Restaurant.Core.UnitTests/Restaurant.Core.UnitTests.csproj"
)

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

Invoke-Restore -SolutionName $SolutionName

Invoke-Build -SolutionName $SolutionName -Configuration $Configuration

Invoke-Test -TestProjectFile $TestProjectFile -Configuration $Configuration