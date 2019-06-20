param
(
    [parameter(Mandatory = $true, Position = 0)]
    [string] $DebugDirectory,

    [parameter(Mandatory = $true, Position = 1)]
    [string] $ModuleFileDirectory,

    [parameter(Mandatory = $true, Position = 2)]
    [string] $AssemblyInfo,

    [parameter(Mandatory = $true, Position = 3)]
    [string] $TargetFileName
)

## Clear out files
Get-ChildItem -Path $DebugDirectory -Include *.ps1xml -Recurse | Remove-Item -Force;

## Get Module Version
$assInfo = Get-Content -Path $AssemblyInfo;
foreach ($line in $assInfo)
{
    if ($line -like "*AssemblyFileVersion(*")
    {
        $vers = $line -replace '^\s*\[assembly\:\sAssemblyFileVersion\(\"(.*?)\"\)\]$', '$1';
    }
}
$allFiles = Get-ChildItem $ModuleFileDirectory -Include * -Exclude *.old -Recurse;
$References = Join-Path "$ModuleFileDirectory\.." "Assemblies";

[string[]]$verbs = Get-Verb | Select-Object -ExpandProperty Verb;
[string[]]$verbs += @("Download");
$patFormat = '^({0})(\S{{1,}})\.cs';
$pattern = $patFormat -f ($verbs -join '|')
$cmdletFormat = "{0}-{1}";

$baseCmdletDir = Join-Path "$ModuleFileDirectory\.." "Cmdlets"
[string[]]$folders = [System.IO.Directory]::EnumerateDirectories($baseCmdletDir, "*", [System.IO.SearchOption]::TopDirectoryOnly) | Where-Object { -not $_.EndsWith('Bases') };

[string[]]$Cmdlets = foreach ($cs in $(Get-ChildItem -Path $folders *.cs -File))
{
	$match = [regex]::Match($cs.Name, $pattern)
	$cmdletFormat -f $match.Groups[1].Value, $match.Groups[2].Value;
}

[string[]]$allDlls = Get-ChildItem $References -Include *.dll -Exclude 'SusNativeCommon.dll', 'System.Management.Automation.dll' -Recurse | Select-Object -ExpandProperty Name;
Get-ChildItem $References 'SusNativeCommon.dll' -File | Copy-Item -Destination $DebugDirectory -Force;
[string[]]$allFormats = $allFiles | Where-Object -FilterScript { $_.Extension -eq ".ps1xml" } | Select-Object -ExpandProperty Name;

$manifestFile = "UpdateManagement.psd1"

$allFiles | Copy-Item -Destination $DebugDirectory -Force;
$modPath = Join-Path $DebugDirectory $manifestFile

$manifest = @{
    Path                   = $modPath
    Guid                   = '15740cb3-98ac-49f2-b49c-333740a629f0';
    Description            = 'This module is designed to be a total revamp of the "UpdateServices" native WSUS PSModule.'
    Author                 = 'Mike Garvey'
    CompanyName            = 'Yevrag35, LLC.'
    Copyright              = '(c) 2019 Yevrag35, LLC.  All rights reserved.'
	CompatiblePSEditions   = 'Desktop'
    ModuleVersion          = $($vers.Trim() -split '\.' | Select-Object -First 3) -join '.'
    PowerShellVersion      = '5.1'
    DotNetFrameworkVersion = '4.7'
    RootModule             = $TargetFileName
    DefaultCommandPrefix   = "Um"
    RequiredAssemblies     = $allDlls
	CmdletsToExport		   = $Cmdlets
	FunctionsToExport	   = @()
	AliasesToExport		   = @()
	ScriptsToProcess	   = 'UpdateManagement.ps1'
    FormatsToProcess       = if ($allFormats.Length -gt 0) { $allFormats } else { @() };
    ProjectUri             = 'https://github.com/Yevrag35/UpdateManagement'
	HelpInfoUri			   = 'https://github.com/Yevrag35/UpdateManagement/issues'
	LicenseUri			   = 'https://raw.githubusercontent.com/Yevrag35/UpdateManagement/master/LICENSE'
    Tags                   = @( 'Wsus', 'Update', 'Management', 'Module', 'Windows', 'Server', '10', '2019',
								'8.1', '7', '1607', '1703', '1709', '1803', '1809', 'Download', 'Get', 'Approve',
								'Decline', 'Collection', 'format', 'computer', 'target', 'updateservices', 'knowledge', 
								'base', 'office', '2010', '2013', '2016', 'architecuture', 'kbarticle', 'id', 'query',
								'product', 'info', 'information', 'group', 'context', 'filter', 'progress', 'bar',
								'equality', 'parameter', 'exception', 'enum', 'attribute', 'pscmdlet', 'connect',
								'disconnect', 'UM', 'remove', 'delete' )
};

New-ModuleManifest @manifest;
Update-ModuleManifest -Path $modPath -Prerelease 'alpha'