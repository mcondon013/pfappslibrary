@echo on
rem call "c:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\\vcvarsall.bat" x86 
set
cd C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\
"c:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\\bin\EditBin.exe" "C:\ProFast\Projects\DotNetAppsV3\pfDataViewer\pfDataViewer\bin\Release\pfDataViewer.exe"  /LARGEADDRESSAWARE
:exit
@echo If this did not work, you may have to run vcvarsall.bat to set the environment.
pause