Framework "4.0"

properties {
	$baseDirectoryPath = "./.."
	$sourceDirectoryPath = "$baseDirectoryPath/source"
	$packageDirectoryPath = "$baseDirectoryPath/source/packages"
	$projectName = "MvcNavigation"

	# Build
	$solutionFilePath = "$sourceDirectoryPath/$projectName.sln"
	$buildVerbosity = "Minimal"
	$buildConfiguration = "Release"

	# Test
	$mspecExePath = "$packageDirectoryPath/Machine.Specifications.0.5.3.0/tools/mspec-clr4"
	$mspecAssemblyPath = "$sourceDirectoryPath/$projectName.Specifications/bin/$buildConfiguration/$projectName.Specifications.dll"

	# CreateNuGetPackage
	$buildOutputDirectoryPath = "$baseDirectoryPath/build_output"
	$packageRootDirectoryPath = "$buildOutputDirectoryPath/packageRoot"
	$contentDirectoryPath = "$packageRootDirectoryPath/content"
	$packageDisplayTemplateDirectoryPath = "$contentDirectoryPath/Views/Shared/DisplayTemplates"
	$frameworkDirectoryPath = "$packageRootDirectoryPath/lib/net40"
	$nuGetExePath = "$packageDirectoryPath/NuGet.CommandLine.1.7.0/tools/nuget"
}

formatTaskName "`n##------ {0} ------##`n"

task default -depends CreateNuGetPackage

task CreateNuGetPackage -depends Test {
	write-host `n-------- Preparing temp directories`n -foregroundColor yellow
	# create convention based working directory
	new-item $packageRootDirectoryPath -type directory
	new-item $packageDisplayTemplateDirectoryPath -type directory
	new-item $frameworkDirectoryPath -type directory

	write-host `n-------- Preparing files`n -foregroundColor yellow
	# copy package specification
	copy-item $sourceDirectoryPath/$projectName.nuspec $packageRootDirectoryPath

	# copy display templates
	copy-item $sourceDirectoryPath/MvcNavigation.DisplayTemplates/Views/Shared/DisplayTemplates/* $packageDisplayTemplateDirectoryPath

	# copy transformations
	copy-item $sourceDirectoryPath/$projectName/Web.config.transform $contentDirectoryPath

	# copy build output
	copy-item $sourceDirectoryPath/$projectName/bin/$buildConfiguration/$projectName.dll $frameworkDirectoryPath

	# set package version
	$packageVersion = (Get-Command $frameworkDirectoryPath/$projectName.dll).FileVersionInfo.ProductVersion # from AssemblyInformationalVersionAttribute
	$xml = New-Object XML
	$xml.Load("$packageRootDirectoryPath/$projectName.nuspec")
	$xml.package.metadata.version = $packageVersion
	$xml.Save("$packageRootDirectoryPath/$projectName.nuspec")
	write-host Package version is: $packageVersion
	
	# create NuGet package
	write-host `n-------- Creating package`n -foregroundColor yellow
	Exec { & $nuGetExePath pack $packageRootDirectoryPath/$projectName.nuspec -OutputDirectory $buildOutputDirectoryPath }
}

task Test -depends Build {
	Exec { & $mspecExePath $mspecAssemblyPath }
}

task Build -depends Clean {
	Exec { msbuild $solutionFilePath /m /v:Minimal /t:Rebuild /p:Configuration=Release /nologo }
}

task Clean {
	if (Test-Path $buildOutputDirectoryPath) {
		write-host Cleaning $buildOutputDirectoryPath -foregroundColor yellow
		Remove-Item $buildOutputDirectoryPath -Recurse
	}
	else {
		write-host Nothing to clean -foregroundColor gray
	}
}