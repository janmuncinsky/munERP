#!/bin/bash
set -e
# echo -e "\e[33m\tClearing output"
# rm -rf --verbose "docker/images/output"
echo -e "\e[33m\tBuilding the solution"

declare -a netcoreProjects=(
    "src\MunCode.Core.Messaging.Plugin.Castle\MunCode.Core.Messaging.Plugin.Castle.csproj"
    "src\MunCode.Core.Messaging.Http.Plugin.Castle\MunCode.Core.Messaging.Http.Plugin.Castle.csproj"
    "src\MunCode.Core.Caching.Memory.Plugin.Castle\MunCode.Core.Caching.Memory.Plugin.Castle.csproj"
    "src\MunCode.Core.Log4net.Plugin.Castle\MunCode.Core.Log4net.Plugin.Castle.csproj"
    "src\MunCode.Core.AppHosting.Plugin.Castle\MunCode.Core.AppHosting.Plugin.Castle.csproj"
    "src\MunCode.Core.Ioc.Castle\MunCode.Core.Ioc.Castle.csproj"
    "src\MunCode.Core.EFCore.Plugin.Castle\MunCode.Core.EFCore.Plugin.Castle.csproj"
    "src\MunCode.Core.Messaging.AspNetCore.Plugin.Castle\MunCode.Core.Messaging.AspNetCore.Plugin.Castle.csproj"
    "src\MunCode.Core.Messaging.EasyNetQ.Plugin.Castle\MunCode.Core.Messaging.EasyNetQ.Plugin.Castle.csproj"
    "src\MunCode.Core.Messaging.Api.Plugin.Castle\MunCode.Core.Messaging.Api.Plugin.Castle.csproj"
    "src\MunCode.Core.Messaging.Gateway.Plugin.Castle\MunCode.Core.Messaging.Gateway.Plugin.Castle.csproj"
    "src\MunCode.munERP.Accounting.Model.EFCore.Plugin.Castle\MunCode.munERP.Accounting.Model.EFCore.Plugin.Castle.csproj"
    "src\MunCode.munERP.Sales.Model.EFCore.Plugin.Castle\MunCode.munERP.Sales.Model.EFCore.Plugin.Castle.csproj"
    "src\MunCode.munERP.Accounting.Api\MunCode.munERP.Accounting.Api.csproj"
    "src\MunCode.munERP.Sales.Api\MunCode.munERP.Sales.Api.csproj"
    "src\MunCode.munERP.Accounting.Model\MunCode.munERP.Accounting.Model.csproj"
    "src\MunCode.munERP.Sales.Model\MunCode.munERP.Sales.Model.csproj"
    "src\MunCode.Core.AspNetCore.Host\MunCode.Core.AspNetCore.Host.csproj"
    "src\MunCode.Core.NetCore.Host\MunCode.Core.NetCore.Host.csproj"
)
	
for project in "${netcoreProjects[@]}"
do
	dotnet publish $project --framework netcoreapp2.1
done