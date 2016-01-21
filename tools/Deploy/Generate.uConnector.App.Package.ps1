[CmdletBinding()]
Param(
  [Parameter(Mandatory=$False)]
  [string]$TargetDirectory = "C:\tmp\SampleApp",
    
  [Parameter(Mandatory=$False)]
  [string]$SourceDirectory, 
  
  [Parameter(Mandatory=$False)]
  [string]$RebuildSolution = "True"
)

function GetScriptDirectory { 
    Split-Path -parent $PSCommandPath 
}

function GetProjectFolder {
	$scriptPath = GetScriptDirectory;
	
	return "$scriptPath\..\..\src\uCommerce.uConnector"
}

function MoveNuspecFile {
  $scriptPath = GetScriptDirectory;
  $nugetPath = $scriptPath + "\..\NuGet"

  Copy-Item -Path $nugetPath\App.Manifest.nuspec -Destination $TargetDirectory
}

function GetVersion {
  $scriptPath = GetScriptDirectory;
  $nuspecFile = "$scriptPath\..\NuGet\App.Manifest.nuspec";

  [xml]$fileContents = Get-Content -Path $nuspecFile
  return $fileContents.package.metadata.version;
}

function GetSolutionFile { 
   $scriptPath = GetScriptDirectory;
   $srcFolder = "$scriptPath\..\..\src";
   return Get-ChildItem -Path $srcFolder -Filter *.sln -Recurse;
}

function UpdateAssemblyInfos {
  $version = GetVersion;
  $newVersion = 'AssemblyVersion("' + $version + '")';
  $newFileVersion = 'AssemblyFileVersion("' + $version + '")';
  
  foreach ($file in Get-ChildItem $SourceDirectory AssemblyInfo.cs -Recurse)  
  {      
    $TmpFile = $file.FullName + ".tmp"

    get-content $file.FullName | 
      %{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $newVersion } |
      %{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $newFileVersion } |
    set-content $TmpFile -force
    move-item $TmpFile $file.FullName -force
  }
}

function Run-It () {
  try {  
    $scriptPath = GetScriptDirectory;
    Import-Module "$scriptPath\..\psake\4.3.0.0\psake.psm1"
                   
    #Step 01 rebuild solution
    $SolutionFile = GetSolutionFile;
    $rebuildProperties = @{
      "Solution_file" = $SolutionFile;
      "srcDir" = $SourceDirectory;
		  "Configuration" = "Release"
    };
    
    if($RebuildSolution.Equals("True")){
      Invoke-PSake "$ScriptPath\Rebuild.App.Solution.ps1" "Rebuild" -parameters $rebuildProperties
    }
    
    #Step 02 update assembly version on projects in sln. 
    UpdateAssemblyInfos;
    
    #Step 03 Extract files
    if ($SourceDirectory.Equals(""))
		{
			$SourceDirectory = GetProjectFolder;
		}
		
    $extractProperties = @{
      "TargetDirectory" = $TargetDirectory + "\Content";
      "SourceDirectory" = $SourceDirectory;
    };

    Invoke-PSake "$ScriptPath\Extract.Files.For.App.ps1" "Run-It" -parameters $extractProperties
   
    #Step 04 bin to ..\lib
    $pathToTargetBinDir = $TargetDirectory+ "\Content\bin\"
    $pathToTargetLibDir = $TargetDirectory+ "\lib\net400"
    $pathToDlls = $pathToTargetBinDir+ "debug"            
        
    New-Item $pathToTargetLibDir -type directory -force
    Move-Item $pathToDlls\*.dll $pathToTargetLibDir
    Remove-Item $pathToTargetBinDir -recurse

    #Step 05 pack it up
    MoveNuspecFile;
    $nuget = $scriptPath + "\..\NuGet";
    $nuspecFilePath = $TargetDirectory + "\App.Manifest.nuspec";

    & "$nuget\nuget.exe" pack $nuspecFilePath -OutputDirectory $TargetDirectory;

    #Step 06 remove/delete files. 
    Remove-Item $TargetDirectory\* -exclude *.nupkg -recurse
  } catch {  
    Write-Error $_.Exception.Message -ErrorAction Stop  
  }
}

Run-It