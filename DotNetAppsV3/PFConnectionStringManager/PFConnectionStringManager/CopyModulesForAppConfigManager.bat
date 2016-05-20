@echo off
rem copy app options files
set AppConfigManagerPath=..\..\..\..\..\DotNetAppsV3\pfAppConfigManager\pfAppConfigManager\bin
set appTargetPath=%1
set appTargetFileName=%2
set appConfiguration=%3
@echo appTargetPath: %appTargetPath%
@echo appTargetFileName: %appTargetFileName%
@echo appConfiguration: %appConfiguration%
set "appExeName=%~nx1"
set "appOutputFolder=%~dp1"
@echo appExeName: %appExeName%
@echo appOutputFolder: %appOutputFolder%
if not exist %appOutputFolder% md %appOutputFolder%
@echo off
copy %AppConfigManagerPath%\%appConfiguration%\pfAppConfigManager.exe %appOutputFolder%pfAppConfigManager.exe
copy %AppConfigManagerPath%\%appConfiguration%\pfAppConfigManager.exe.config %appOutputFolder%pfAppConfigManager.exe.config

set dbdllname=PFDataAccessObjects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFDB2Objects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFInformixObjects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFMySQLObjects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFOracleObjects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLAnywhereObjects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLAnywhereULObjects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLServerCE35Objects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLServerCE40Objects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSybaseObjects.dll
@echo %dbdllname%
copy C:\ProFast\Projects\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y

@echo off

:procexit
exit
