[CmdletBinding()]
Param(
	[Parameter(Mandatory=$False)]
	[string]$OutputDirectory="c:\tmp\uConnector NuGet Package",
    
  [Parameter(Mandatory=$False)]
  [string]$SourceDirectory, 
  
  [Parameter(Mandatory=$False)]
  [string]$RebuildSolution = "True", 
	
	[Parameter(Mandatory=$False)]
	[string]$NuspecFile="uConnector.Manifest.nuspec"
)

function GetScriptDirectory { 
    Split-Path -parent $PSCommandPath 
}

function GetSolutionFile { 
   $scriptPath = GetScriptDirectory;
   $srcFolder = "$scriptPath\..\..\src";
   return Get-ChildItem -Path $srcFolder -Filter *.sln -Recurse;
}

function GetProjectFolder {
	$scriptPath = GetScriptDirectory;
	
	return "$scriptPath\..\..\src\uConnector\"
}

function MoveNuspecFile {
  $scriptPath = GetScriptDirectory;
  $nugetPath = $scriptPath + "\..\NuGet\" + $NuspecFile

  Copy-Item -Path $nugetPath -Destination $OutputDirectory
}

function GetVersion {
  $scriptPath = GetScriptDirectory;
  $nuspecFile = "$scriptPath\..\NuGet\" + $NuspecFile;

  [xml]$fileContents = Get-Content -Path $nuspecFile
  return $fileContents.package.metadata.version;
}


function UpdateAssemblyInfos ($project){
  $version = GetVersion;
  $version = GetUpdatedVersionString $version;

  $newVersion = 'AssemblyVersion("' + $version + '")';
  $newFileVersion = 'AssemblyFileVersion("' + $version + '")';
   
  foreach ($file in Get-ChildItem $project AssemblyInfo.cs -Recurse)  
  {      
    $TmpFile = $file.FullName + ".tmp"

    get-content $file.FullName | 
      %{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $newVersion } |
      %{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $newFileVersion } |
    set-content $TmpFile -force
    move-item $TmpFile $file.FullName -force
  }
}

function UpdateNuspecVersion {
    $scriptPath = GetScriptDirectory;
    $nuspecFile = $nuspecFilePath;

    [xml]$fileContents = Get-Content -Path $nuspecFile
    $fileContents.package.metadata.version = GetUpdatedVersionString $fileContents.package.metadata.version;
    
    $fileContents.Save($nuspecFile);
}

function GetUpdatedVersionString($version) {
    $versionDateNumberPart = (Get-Date).Year.ToString().Substring(2) + "" + (Get-Date).DayOfYear.ToString("000");
    return $version.Substring(0, ($version.LastIndexOf(".") + 1)) + $versionDateNumberPart;
}

function Create-Package() {
	try {
    $scriptPath = GetScriptDirectory;
    Import-Module "$scriptPath\..\psake\4.3.0.0\psake.psm1"
    
    #Step 01: Maintain dependencies    
    $maintainDependenciesProperties = @{
      "projects" = @(
        "$scriptPath\..\..\src\uConnector",
        "$scriptPath\..\..\src\uConnector.Framework"
      );
      "nuspecFile" = "$scriptPath\..\NuGet\$NuspecFile";
      "uConnectorCore" = $true;
    };
        
    Invoke-PSake "$ScriptPath\Maintain.Nuget.Dependencies.ps1" "Run-It" -parameters $maintainDependenciesProperties
    
    #Step 02: rebuild solution
    if($RebuildSolution.Equals("True")){
      $SolutionFile = GetSolutionFile;
      $rebuildProperties = @{
        "solution_file" = $SolutionFile;
        "srcDir" = Resolve-Path "$scriptPath\..\..\src";
        "configuration" = "Release";
        "visualStudioVersion" = 14;
      };    
    
      Invoke-PSake "$ScriptPath\Rebuild.Solution.ps1" "Rebuild" -parameters $rebuildProperties
    }
    
    #Step 03 update assembly version on projects in sln. 
    UpdateAssemblyInfos "$scriptPath\..\..\src\uConnector";
    UpdateAssemblyInfos "$scriptPath\..\..\src\uConnector.Framework";
    
    #Step 04: Extract files
    $extractProperties = @{
      "TargetDirectory" = $OutputDirectory;
      "projects" = @(
        "$scriptPath\..\..\src\uConnector",
        "$scriptPath\..\..\src\uConnector.Framework"
      );
    };

    Invoke-PSake "$ScriptPath\Extract.Files.For.App.ps1" "Run-It" -parameters $extractProperties
    
    #Step 05: Create Nuget package
    MoveNuspecFile;
    $nuget = $scriptPath + "\..\NuGet";
    $nuspecFilePath = $OutputDirectory + "\" + $NuspecFile;

    #Step 06 Update the version in the nuspec file#
    UpdateNuspecVersion

     #Step 07: Create Nuget package
    & "$nuget\nuget.exe" pack $nuspecFilePath -OutputDirectory $OutputDirectory;
    
    #Step 06: Clean up
    Remove-Item $OutputDirectory\* -exclude *.nupkg -recurse
	} catch {
		Write-Error $_.Exception.Message -ErrorAction Stop
	}
}

Create-Package