cd ..\
SET PATH=%PATH%;C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\amd64\
Powershell.exe -executionpolicy remotesigned -File build\cli-win\built-bits.ps1
pause