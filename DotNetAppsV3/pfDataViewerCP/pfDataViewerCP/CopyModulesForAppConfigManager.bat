@echo off
rem copy app options files
set AppConfigManagerPath=..\..\..\..\..\DotNetAppsV3\pfAppConfigManagerPlusRestore\pfAppConfigManager\bin
set ConnectionStringManagerPath=..\..\..\..\..\DotNetAppsV3\PFConnectionStringManager\PFConnectionStringManager\bin
set RandomDataManagerPath=..\..\..\..\..\DotNetAppsV3\pfRandomDataSources\pfRandomDataSources\bin
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
@echo %AppConfigManagerPath%\%appConfiguration%\
@echo on
copy %AppConfigManagerPath%\%appConfiguration%\pfAppConfigManager.exe %appOutputFolder%pfAppConfigManager.exe
copy %AppConfigManagerPath%\%appConfiguration%\pfAppConfigManager.exe.config %appOutputFolder%pfAppConfigManager.exe.config

copy %ConnectionStringManagerPath%\%appConfiguration%\PFConnectionStringManager.exe %appOutputFolder%PFConnectionStringManager.exe
copy %ConnectionStringManagerPath%\%appConfiguration%\PFConnectionStringManager.exe.config %appOutputFolder%PFConnectionStringManager.exe.config

copy %RandomDataManagerPath%\%appConfiguration%\pfRandomDataSources.exe %appOutputFolder%pfRandomDataSources.exe
copy %RandomDataManagerPath%\%appConfiguration%\pfRandomDataSources.exe.config %appOutputFolder%pfRandomDataSources.exe.config

copy ..\..\..\..\..\DotNetAppsV3\PFConnectionStringManager\PFConnectionStringManager\bin\Release\ConnectionStringsManager.chm %appOutputFolder%ConnectionStringsManager.chm
copy ..\..\..\..\..\DotNetAppsV3\pfRandomDataSources\pfRandomDataSources\bin\Release\RandomDataMasks.chm %appOutputFolder%RandomDataMasks.chm
copy ..\..\..\..\..\DotNetClassesV3\AppDataComponents\PFDataOutputGrid\bin\Release\DataOutputGrid.chm %appOutputFolder%DataOutputGrid.chm 

@echo off

set dbdllname=PFDataAccessObjects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y

set dbdllname=PFDB2Objects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFInformixObjects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFMySQLObjects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFOracleObjects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLAnywhereObjects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLAnywhereULObjects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLServerCE35Objects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSQLServerCE40Objects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
set dbdllname=PFSybaseObjects.dll
@echo %dbdllname%
copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y


REM set dbdllname=PFSQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFSQLServerCE40SQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFOracleSQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFMySQLSQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFSybaseSQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFDB2SQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFInformixSQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFSQLAnywhereSQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y
REM set dbdllname=PFSQLAnywhereULSQLBuilderObjects.dll
REM @echo %dbdllname%
REM copy ..\..\..\..\..\Binaries\ProFast\ClassLibraries\Release\%dbdllname% %appOutputFolder%%dbdllname% /y

@echo off
rem set
rem @echo Run EditBin to increase available memory
rem cd C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\
rem "c:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\\bin\EditBin.exe" "C:\ProFast\CPApps\DotNetAppsV3\pfDataViewerCP\pfDataViewerCP\bin\Release\pfDataViewerCP.exe"  /LARGEADDRESSAWARE

:procexit
exit