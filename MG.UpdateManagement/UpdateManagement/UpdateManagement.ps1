# Verify necessary Registry Keys and Values are present.
$upServices = "HKLM:\SOFTWARE\Microsoft\Update Services\Server\Setup";

if (!(Test-Path $upServices))
{
	# Create Key
	New-Item $upServices -Force > $null

	# Create minimum necessary Values for WSUS
	New-ItemProperty -Path $upServices -Name "ConfigurationSource" -PropertyType DWORD -Value 0 > $null
	New-ItemProperty -Path $upServices -Name "EnableRemoting" -PropertyType DWORD -Value 1 > $null
	New-ItemProperty -Path $upServices -Name "TargetDir" -PropertyType String -Value '%Program Files%\Update Services\' > $null
}
