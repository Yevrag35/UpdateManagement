@{
    GUID = '15740cb3-98ac-49f2-b49c-333740a629f0'
    Author = 'Mike Garvey'
    Description = 'This module is designed to be a total revamp of the "UpdateServices" native WSUS PSModule.'
    CompanyName = 'Yevrag35, LLC.'
    Copyright = "© 2018 Yevrag35, LLC.  All rights reserved."
    ModuleVersion = '0.2.1'
	PowerShellVersion = '4.0'
	ProcessorArchitecture = 'Amd64'
	RootModule = 'MG.UpdateManagement.dll'
	RequiredAssemblies = @(
		'Dynamic.Parameter.dll',
		'MG.Attributes.dll',
		'Microsoft.UpdateServices.AdminDataAccessProxy.dll',
		'Microsoft.UpdateServices.Administration.dll',
		'Microsoft.UpdateServices.BaseApi.dll',
		'Microsoft.UpdateServices.ClientServicing.dll',
		'Microsoft.UpdateServices.CoreCommon.dll',
		'Microsoft.UpdateServices.DBlayer.dll',
		'Microsoft.UpdateServices.StringResources.dll',
		'Microsoft.UpdateServices.StringResources.Resources.dll',
		'Microsoft.UpdateServices.Utils.dll'
	)
	# RequiredModules = ''
	FunctionsToExport = ''
	AliasesToExport = @('appup', 'conums', 'deup', 'downup', 'getup', 'remup', 
		'Delete-UMUpdate')
	CmdletsToExport = @(
		'Approve-UMUpdate',
		'Connect-UMServer',
		'Decline-UMUpdate',
		'Download-UMUpdate',
		'Get-UMUpdate',
		'Remove-UMUpdate'
	)
	ScriptsToProcess = "UpdateManagement.ps1"
	FormatsToProcess = @(
		'MG.UpdateManagement.Objects.UMUpdate.Format.ps1xml',
		'Microsoft.UpdateServices.Administration.IComputerTarget.Format.ps1xml'
	)
	VariablesToExport = ''
	FileList = @(
		'Dynamic.Parameter.dll',
		'MG.Attributes.dll',
		'MG.UpdateManagement.dll',
		'MG.UpdateManagement.Objects.UMUpdate.Format.ps1xml',
		'Microsoft.UpdateServices.Administration.IComputerTarget.Format.ps1xml',
		'UpdateManagement.ps1',
		'UpdateManagement.psd1'
	)
	# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
	PrivateData = @{

		PSData = @{

			# Tags applied to this module. These help with module discovery in online galleries.
			Tags = 'Wsus', 'Update', 'Management', 'Module', 'Windows', 'Server', '10', '2019',
				'8.1', '7', '1607', '1703', '1709', '1803', '1809', 'Download', 'Get', 'Approve',
				'Decline', 'Collection', 'format', 'computer', 'target', 'updateservices', 'knowledge', 
				'base', 'office', '2010', '2013', '2016', 'architecuture', 'kbarticle', 'id', 'query',
				'product', 'info', 'information', 'group', 'context', 'filter', 'progress', 'bar',
				'equality', 'parameter', 'exception', 'enum', 'attribute', 'pscmdlet', 'connect',
				'disconnect', 'UM', 'remove', 'delete'

			# A URL to the license for this module.
			# LicenseUri = ''

			# A URL to the main website for this project.
			ProjectUri = 'https://github.com/Yevrag35/UpdateManagement'

			# A URL to an icon representing this module.
			# IconUri = ''

			# ReleaseNotes of this module
			ReleaseNotes = 'Added Server 2019 and Windows 10 1809 as valid products.  ''All'' office product updates now display in the results.  New cmdlet for ''deleting'' updates from the Wsus database.  Updates now refresh themselves after taking actions.'

			# External dependent modules of this module
			# ExternalModuleDependencies = ''

		} # End of PSData hashtable

	} # End of PrivateData hashtable
}
