# Crea (si no existe) la base, aplica schema.sql y carga seed.sql.
#   .\reset.ps1                       -> base Aviones con datos de prueba
#   .\reset.ps1 -Database Aviones_Test -NoSeed
param(
    [string]$Server = 'localhost',
    [string]$Database = 'Aviones',
    [switch]$NoSeed
)

$ErrorActionPreference = 'Stop'
$sqlcmd = (Get-Command sqlcmd -ErrorAction SilentlyContinue).Source
if (-not $sqlcmd) { $sqlcmd = 'C:\Program Files\SqlCmd\sqlcmd.exe' }

function Invoke-Sql([string[]]$sqlArgs) {
    & $sqlcmd -S $Server -E -C -b @sqlArgs
    if ($LASTEXITCODE -ne 0) { throw "sqlcmd failed: $($sqlArgs -join ' ')" }
}

Invoke-Sql @('-Q', "IF DB_ID(N'$Database') IS NULL CREATE DATABASE [$Database];")
Invoke-Sql @('-d', $Database, '-i', (Join-Path $PSScriptRoot 'schema.sql'))
if (-not $NoSeed) {
    Invoke-Sql @('-d', $Database, '-i', (Join-Path $PSScriptRoot 'seed.sql'))
}
Write-Host "Base '$Database' lista."
