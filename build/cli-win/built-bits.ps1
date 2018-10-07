$ErrorActionPreference='Stop';

# Get-ChildItem output\bin -Recurse | Remove-Item -recurse -force -confirm:$false
dotnet publish "src\MunCode.munERP.sln"  -c Debug -v detailed

# if ($lastexitcode -ne 0) {
  # throw "Build failed."
# }

# $msbuild = "c:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"
# $sln = "src\MunCode.munERP.Client.Win.sln"
# $params = "/p:Configuration=Debug"

# & $msbuild $sln $params