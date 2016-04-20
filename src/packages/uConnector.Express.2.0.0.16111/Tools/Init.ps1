param($installPath, $toolsPath, $package, $project)

function HasStartAction ($item)
{
    foreach ($property in $item.Properties)
    {
       if ($property.Name -eq "StartAction")
       {
           return $true
       }            
    } 

    return $false
}

function ModifyConfigurations
{
    $configurationManager = $project.ConfigurationManager
    $fullpath = $project.properties.item("fullpath").Value

    foreach ($name in $configurationManager.ConfigurationRowNames)
    {
        $projectConfigurations = $configurationManager.ConfigurationRow($name)

        foreach ($projectConfiguration in $projectConfigurations)
        {                

            if (HasStartAction $projectConfiguration)
            {
                $newStartAction = 1
                $newStartProgram = $fullPath + "bin\" + $name + "\uConnector.Express.exe"
                $newStartArguments = "Tasks"

                $projectConfiguration.Properties.Item("StartAction").Value = [String] $newStartAction
                $projectConfiguration.Properties.Item("StartProgram").Value = [String] $newStartProgram
                $projectConfiguration.Properties.Item("StartArguments").Value = [String] $newStartArguments
            }
        }
    }

    #$project.Save
}

function Initialize {
    ModifyConfigurations
}