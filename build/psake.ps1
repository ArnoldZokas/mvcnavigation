Import-Module .\..\source\packages\psake.4.1.0\tools\psake.psm1;
Invoke-psake .\build.ps1;

if ($Error.count > 0)
{
	Write-Host "ERROR: $error" -fore RED; 
	exit 1
}