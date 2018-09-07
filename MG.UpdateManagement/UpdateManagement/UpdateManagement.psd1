@{
    GUID = '15740cb3-98ac-49f2-b49c-333740a629f0'
    Author = 'Mike Garvey'
    Description = 'This module is designed to be a total revamp of the "UpdateServices" native WSUS PSModule.'
    CompanyName = 'Yevrag35, LLC.'
    Copyright = "© 2018 Yevrag35, LLC.  All rights reserved."
    ModuleVersion = '0.2.0'
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
	RequiredModules = ''
	FunctionsToExport = ''
	AliasesToExport = ''
	CmdletsToExport = @(
		'Approve-UMUpdate',
		'Connect-UMServer',
		'Decline-UMUpdate',
		'Download-UMUpdate',
		'Get-UMUpdate'
	)
	FormatsToProcess = @(
		'MG.UpdateManagement.Objects.UMUpdate.Format.ps1xml',
		'Microsoft.UpdateServices.Administration.IComputerTarget.Format.ps1xml'
	)
	VariablesToExport = @(
		'appup', 'conums',
		'deup', 'downup', 'getup'
	)
	FileList = @(
		'Dynamic.Parameter.dll',
		'MG.Attributes.dll',
		'MG.UpdateManagement.dll',
		'MG.UpdateManagement.Objects.UMUpdate.Format.ps1xml',
		'Microsoft.UpdateServices.Administration.IComputerTarget.Format.ps1xml',
		'UpdateManagement.psd1'
	)
	# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
	PrivateData = @{

		PSData = @{

			# Tags applied to this module. These help with module discovery in online galleries.
			Tags = 'Wsus', 'Update', 'Management', 'Module', 'Windows', 'Server', '10',
				'8.1', '7', '1607', '1703', '1709', '1803', 'Download', 'Get', 'Approve',
				'Decline', 'Collection', 'format', 'computer', 'target', 'updateservices',
				'office', '2010', '2013', '2016', 'architecuture', 'kbarticle', 'id', 'query',
				'product', 'info', 'information', 'group', 'context', 'filter', 'progress', 'bar',
				'equality', 'parameter', 'exception', 'enum', 'attribute', 'pscmdlet', 'connect',
				'disconnect', 'UM'

			# A URL to the license for this module.
			# LicenseUri = ''

			# A URL to the main website for this project.
			# ProjectUri = ''

			# A URL to an icon representing this module.
			# IconUri = ''

			# ReleaseNotes of this module
			ReleaseNotes = 'So far only getting, approving, declining, and downloading of updates is supported.  Only the following products are filterable: "Windows 7", "Windows 8.1", "Window 10 1607", "Windows 10 1703", "Windows 10 1709", "Windows 1803", "Server 2008 R2", "Server 2012", "Server 2012 R2", "Server 2016", "Server 1709", "Server 1803", "Office 2010", "Office 2013", and "Office 2016".  This is a work in progress; comment if you would like to see a product included.'

			# External dependent modules of this module
			ExternalModuleDependencies = 'UpdateServices'

		} # End of PSData hashtable

	} # End of PrivateData hashtable
}
