$ErrorActionPreference='Stop';

# Get-ChildItem output\bin -Recurse | Remove-Item -recurse -force -confirm:$false
dotnet publish "src\MunCode.munERP.sln"

if ($lastexitcode -ne 0) {
   throw "Build failed."
}

msbuild "src\MunCode.munERP.Client.Win.sln" /p:Configuration=Debug

if ($lastexitcode -ne 0) {
   throw "Build failed."
}