# Specify the .NET version
$version = "9.0"

# Check if .NET 9.0 is installed
$installed = dotnet --list-sdks | Where-Object { $_ -match "^$version" }

if ($installed) {
    Write-Output "Required .NET version $version is already installed."
} else {
    Write-Output "Installing .NET $version..."
    
    # Download and install .NET
    Invoke-WebRequest -Uri "https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.ps1" -OutFile "dotnet-install.ps1"
    .\dotnet-install.ps1 -Version $version
}

# Confirm installation
Write-Output "Done. Verify with 'dotnet --list-sdks'."
