param(
    [string]$SolutionName = "Restaurant.Client.sln",
    [string]$Configuration = "Release"
)

$NUnitConsole = "packages/NUnit.ConsoleRunner.3.7.0/tools/nunit3-console.exe"

& ./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 3.7.0 NUnit.ConsoleRunner

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

function Invoke-Test() {
    & $NUnitConsole "Client/Tests/Restaurant.UnitTests/bin/Release/Restaurant.UnitTests.dll"

    CheckLastExitCode    
}

Invoke-Restore -solutionName $SolutionName

Invoke-Build -solutionName $SolutionName -Configuration $Configuration

Invoke-Test