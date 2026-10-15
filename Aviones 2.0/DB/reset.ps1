# Borra la base, la vuelve a crear con las migraciones del programa y carga los datos de prueba.
# Usa el programa compilado (bin\Debug), así las migraciones son exactamente las que aplica él.
#   .\reset.ps1                                   -> base Aviones en el SQL Server local, con datos de prueba
#   .\reset.ps1 -Database Aviones_Test -NoSeed
#   .\reset.ps1 -Server '(localdb)\MSSQLLocalDB'   -> la base de LocalDB (como la del usuario)
param(
    [string]$Server = 'localhost',
    [string]$Database = 'Aviones',
    [switch]$NoSeed
)

$ErrorActionPreference = 'Stop'
$bin = Join-Path $PSScriptRoot '..\bin\Debug'
if (-not (Test-Path "$bin\Aviones 2.0.exe")) { throw "Compile el programa primero (falta $bin\Aviones 2.0.exe)." }

$cs = "Data Source=$Server;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=60"
$cn = New-Object System.Data.SqlClient.SqlConnection "$cs;Initial Catalog=master"
$cn.Open()
$cmd = $cn.CreateCommand()
$cmd.CommandText = "IF DB_ID(@db) IS NOT NULL BEGIN DECLARE @sql NVARCHAR(MAX) = N'ALTER DATABASE ' + QUOTENAME(@db) + N' SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE ' + QUOTENAME(@db); EXEC (@sql); END"
[void]$cmd.Parameters.AddWithValue('@db', $Database)
[void]$cmd.ExecuteNonQuery()
$cn.Close()

[void][System.Reflection.Assembly]::LoadFrom("$bin\Dapper.dll")
$asm = [System.Reflection.Assembly]::LoadFrom("$bin\Aviones 2.0.exe")
$migrador = $asm.GetType('Aviones_2._0.Data.Migrador')
$resultado = $migrador::Actualizar("$cs;Initial Catalog=$Database", $null, $null)
Write-Host "Migraciones aplicadas: $($resultado.Aplicadas.Count)"

if (-not $NoSeed) {
    $cn = New-Object System.Data.SqlClient.SqlConnection "$cs;Initial Catalog=$Database"
    $cn.Open()
    foreach ($lote in $migrador::Lotes([System.IO.File]::ReadAllText((Join-Path $PSScriptRoot 'seed.sql')))) {
        $cmd = $cn.CreateCommand(); $cmd.CommandText = $lote; [void]$cmd.ExecuteNonQuery()
    }
    $cn.Close()
}
Write-Host "Base '$Database' lista."
