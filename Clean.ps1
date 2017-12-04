param(
    [string]$Path = "src/Client"
)

Get-ChildItem $Path -Include bin, obj -Recurse -Force | Remove-Item -Recurse -Force