@echo off
rem call "c:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\\vcvarsall.bat" x86 
set
@echo Run EditBin to increase available memory
cd C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\
"c:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\\bin\EditBin.exe" "C:\ProFast\CPApps\DotNetAppsV3\TestprogDataViewer\TestprogDataViewer\bin\Release\TestprogDataViewer.exe"  /LARGEADDRESSAWARE

:exit
pause