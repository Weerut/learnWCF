param($installPath, $toolsPath, $package, $project)

#If we are installing to an AppServer app, set "CopyLocal" to false
if($project.Kind -eq '{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}'){
	$asms = $package.AssemblyReferences | %{$_.Name} 
	foreach ($reference in $project.Object.References) 
	{
		if ($asms -contains $reference.Name + ".dll") 
		{
			$reference.CopyLocal = $false;
		}
	}
}