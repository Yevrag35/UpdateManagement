# UpdateManagement (WSUS)

This module is designed to be a total revamp of the "UpdateServices" native WSUS PSModule. However, for now, you still need the native Wsus powershell module installed.

## Current Limitations

Right now, it's still necessary to have the '__UpdateServices__' powershell module installed.  The reason being is because it's hardcoded into some of the prerequisite assemblies that they be present in the computer's GAC.

Because of the vast quantity of synchronizable product that Wsus caters to, I chose to focus on these products for the initial launch of this:

1. Windows 7
1. Windows 8.1
1. Windows 10
    - _versions 1607 through 1803_
1. Server 2008 R2
1. Server 2012
1. Server 2012 R2
1. Server 2016
1. Server 2016 1709
1. Server 2016 1803

...and then for __non-OS__ products, there's:

1. Office 2010
2. Office 2013
3. Office 2016

...now for the list of cmdlets.

---

## Connect-UMServer

This cmdlet connects the shell to a specified Wsus server.  Unlike the legacy 'UpdateServices' function _Get-WsusServer_, this command sets the context for the entire shell.  No more will you have to carry your Wsus server variable with you.

`Connect-UMServer mysusserver.contoso.com 8531`

* NOTE - _If you use ports 443 or 8531, the use of an SSL connection is implied._

Also, let's say that for this session you wanted to pull back __ALL__ updates and not just the ones that I've coded in.  Use the _-GatherAllUpdates_ switch:

`Connect-UMServer sus.windows.com -p 35535 -UseSSL -GatherAllUpdates`

Be warned though, this will pull __every__ update the Wsus server is synchronizing, so it could take a long time.

---

## Get-UMUpdate

This is the most dynamic command of the list and for good reason too.  Along with __2__ dynamic parameters, "Product" and "Architecture", this cmdlet makes finding specific updates a breeze.

### - "_I want to find 'KB4343909'."_

Simple...
`Get-UMUpdate -KBArticleId 4343903`

### - _"That includes updates for 32-bit too though..."_

Easy...
`Get-UMUpdate -KBArticleId 4343903 -Architecture x64`

### - _"Ok... but it's a Win10 update with the same KB for all versions."_

It shall be done...
`Get-UMUpdate Win1803 KB4343903 x64`

__By default, only updates that meet these criteria are pulled__:

1. Not Superseded
1. Not Yet Approved
1. Not Declined

So if you don't get any results in your initial query, try adding some more filters... like:

`Get-UMUpdate Office2013 -arc '32-bit' -IsApproved:$true -IsSuperseded:$true`

...or maybe you accidentally declined a current update by mistake:

`Get-UMUpdate Server2016 -IsDeclined:$true`

And possibly, you just want to get __ALL__ possible updates (regardless of product, architecture, status, etc.):

`Get-UMUpdate -All`

---

## Approve-UMUpdate

Piping in updates from the _Get-UMUpdate_ cmdlet, this command allows you to approve those retrieved updates to specific TargetComputerGroups.  Of course, those TargetComputerGroups will be dynamically retrieved for you.

`Get-UMUpdate Win1803 4343903 x64 | Approve-UMUpdate -ComputerGroup "All Finance Laptops"`

`Get-UMUpdate -Product Server2012R2 | Approve-UMUpdate "DataCenter Servers"`

---

## Decline-UMUpdate

Much like _Approve-UMUpdate_, 'decline' will simply decline updates that are piped into it.

`Get-UMUpdate -KBArticleId 2987543 | Decline-UMUpdate`

---

## Download-UMUpdate

Now for a cmdlet that _UpdateServices_ couldn't do (in an easy manner), 'Download' can individually download specific updates to a folder of your chosing.  Because your Wsus server may not have the content already downloaded, the download will be taken from Microsoft's Wsus servers directly.

![Download-UMUpdate in action](https://images.yevrag35.com/DownloadUMUpdate.gif)
