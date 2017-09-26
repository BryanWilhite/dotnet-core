$envVar = "Path"
Write-Output "Getting $envVar from the specified Environment target..."

$target = [System.EnvironmentVariableTarget]::Machine
$envPath = [System.Environment]::GetEnvironmentVariable($envVar, $target)
$hasEnvPath = ($envPath -ne $null)
if ($hasEnvPath) {
    $paths = {$envPath.Split(';')}.Invoke()
}
else {
    $paths = {, ""}.Invoke()
}
Write-Output "$($envVar):"
$paths

$newPath = "$env:ProgramData\chocolatey\lib\dotnet.script\Dotnet.Script"
if(-Not $paths.Contains($newPath))
{
    Write-Output "Adding new path to $envVar..."
    $paths.Add($newPath)
    $s = [System.String]::Join(";", $paths)
    [System.Environment]::SetEnvironmentVariable($envVar, $s, $target)
}

$paths = {[System.Environment]::GetEnvironmentVariable($envVar, $target).Split(';')}.Invoke()

Write-Output "$($envVar):"
$paths
