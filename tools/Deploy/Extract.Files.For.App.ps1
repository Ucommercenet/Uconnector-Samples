properties {
  $TargetDirectory = $TargetDirectory;
  $SourceDirectory = $SourceDirectory;
}

function FileExtensionBlackList {
  return "*.xsd", "*sln*", "*.cd","*.cs","*.dll","*.xml","*obj*","*.pdb","*.csproj*","*.cache","*.orig", "app.config", "packages.config";  
}

function DllExtensionBlackList {
  return 
  "UCommerce.dll",
  "UCommerce.Infrastructure.dll",
  "Castle.Core.dll",
  "Castle.Windsor.dll",
  "ClientDependency.Core.dll",
  "Esent.Interop.dll",
  "FluentNHibernate.dll",
  "ICSharpCode.NRefactory.dll",
  "ICSharpCode.NRefactory.CSharp.dll",
  "Iesi.Collections.dll",
  "Jint.Raven.dll",
  "log4net.dll",
  "Lucene.Net.dll",
  "Lucene.Net.Contrib.Spatial.NTS.dll",
  "Microsoft.CompilerServices.AsyncTargetingPack.Net4.dll",
  "Microsoft.WindowsAzure.Storage.dll",
  "NHibernate.dll",
  "Raven.Abstractions.dll",
  "Raven.Client.Embedded.dll",
  "Raven.Client.Lightweight.dll",
  "Raven.Database.dll",
  "ServiceStack.Common.dll",
  "ServiceStack.dll",
  "ServiceStack.Interfaces.dll",
  "ServiceStack.ServiceInterface.dll",
  "ServiceStack.Text.dll",
  "Spatial4n.Core.NTS.dll";  
}

function GetFilesToCopy($path){
	return Get-ChildItem $path -name -recurse -include *.* -exclude (FileExtensionBlackList);
}

function CopyFiles ($appDirectory) {	
	$filesToCopy = GetFilesToCopy($SourceDirectory);
	
	foreach($fileToCopy in $filesToCopy)
	{
	if($fileToCopy -notlike '*packages*'){
      write-host "****************"
      write-host $fileToCopy
      $sourceFile = $SourceDirectory + "\" + $fileToCopy;
      $targetFile = $appDirectory + "\" + $fileToCopy;
      
      # Create the folder structure and empty destination file,
      New-Item -ItemType File -Path $targetFile -Force
      Copy-Item $sourceFile $targetFile -Force
    }
	}
}

function GetDllesToCopy($path){
	return Get-ChildItem $path -name -recurse -include "*.dll*","*.pdb*"  -exclude (DllExtensionBlackList);
}

function CopyDllToBin ($appDirectory) {    
	$filesToCopy = GetDllesToCopy($SourceDirectory);
	
	foreach($fileToCopy in $filesToCopy)
	{
    if($fileToCopy -notlike '*obj*' -And $fileToCopy -notlike '*packages*'){
      $sourceFile = $SourceDirectory + $fileToCopy;
      $targetFile = $appDirectory + "\" + $fileToCopy;	
      		
      # Create the folder structure and empty destination file,
      New-Item -ItemType File -Path $targetFile -Force	
      Copy-Item $sourceFile $targetFile -Force   
    }
	}
}

task Run-It {        
	write-host 'Extracting app to' + $TargetDirectory;
    
  #Creates app directory
  If (!(Test-Path $TargetDirectory)) {
		write-host 'Creating directory: ' + $TargetDirectory;
    New-Item $TargetDirectory -type directory 
  }	
	
	CopyFiles($TargetDirectory);

	CopyDllToBin($TargetDirectory);
    
  write-host 'Extracted app to' + $TargetDirectory;   
}
