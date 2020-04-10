param(
    [string]$apiName   
)

function Build-Gateway-Api {
    cd .\gateway\restaurant-gateway\
    .\build.ps1
}

function Build-Order-Api {
    cd .\services\order.api\
    .\build.ps1
}