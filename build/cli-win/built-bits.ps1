function Check-ExitCode { 
	if ($lastexitcode -ne 0) {
		throw "Build failed."
	}
}

$ErrorActionPreference='Stop';

# Get-ChildItem output\bin -Recurse | Remove-Item -recurse -force -confirm:$false

$allPlatformsProjects = 
    @{Path="src\MunCode.Core.Messaging.Plugin.Castle\MunCode.Core.Messaging.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Messaging.Http.Plugin.Castle\MunCode.Core.Messaging.Http.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Caching.Memory.Plugin.Castle\MunCode.Core.Caching.Memory.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Log4net.Plugin.Castle\MunCode.Core.Log4net.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.AppHosting.Plugin.Castle\MunCode.Core.AppHosting.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Ioc.Castle\MunCode.Core.Ioc.Castle.csproj"}
	
$netcoreProjects = 
    @{Path="src\MunCode.Core.EFCore.Plugin.Castle\MunCode.Core.EFCore.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Messaging.AspNetCore.Plugin.Castle\MunCode.Core.Messaging.AspNetCore.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Messaging.EasyNetQ.Plugin.Castle\MunCode.Core.Messaging.EasyNetQ.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Messaging.Api.Plugin.Castle\MunCode.Core.Messaging.Api.Plugin.Castle.csproj"},
    @{Path="src\MunCode.Core.Messaging.Gateway.Plugin.Castle\MunCode.Core.Messaging.Gateway.Plugin.Castle.csproj"},
    @{Path="src\MunCode.munERP.Accounting.Model.EFCore.Plugin.Castle\MunCode.munERP.Accounting.Model.EFCore.Plugin.Castle.csproj"},
    @{Path="src\MunCode.munERP.Sales.Model.EFCore.Plugin.Castle\MunCode.munERP.Sales.Model.EFCore.Plugin.Castle.csproj"},
    @{Path="src\MunCode.munERP.Accounting.Api\MunCode.munERP.Accounting.Api.csproj"},
    @{Path="src\MunCode.munERP.Sales.Api\MunCode.munERP.Sales.Api.csproj"},
    @{Path="src\MunCode.munERP.Accounting.Model\MunCode.munERP.Accounting.Model.csproj"},
    @{Path="src\MunCode.munERP.Sales.Model\MunCode.munERP.Sales.Model.csproj"},
    @{Path="src\MunCode.Core.AspNetCore.Host\MunCode.Core.AspNetCore.Host.csproj"},
    @{Path="src\MunCode.Core.NetCore.Host\MunCode.Core.NetCore.Host.csproj"}

	
foreach ($item in $allPlatformsProjects) {
	dotnet publish $item.Path --framework net461
	Check-ExitCode
	dotnet publish $item.Path --framework netcoreapp2.1
	Check-ExitCode
}

foreach ($item in $netcoreProjects) {
	dotnet publish $item.Path --framework netcoreapp2.1
	Check-ExitCode
}

$sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$targetNugetExe = "build\cli-win\nuget.exe"
Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
Set-Alias nuget $targetNugetExe -Scope Global -Verbose

$sln="src\MunCode.munERP.Client.Win.sln";
nuget restore $sln
Check-ExitCode
msbuild $sln /p:Configuration=Debug
Check-ExitCode

