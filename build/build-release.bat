@echo off

cls
powershell -Command "& { Import-Module .\..\source\packages\psake.4.1.0\tools\psake.psm1; Invoke-psake .\build.ps1 }"