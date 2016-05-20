@echo off
rem copy app options files
set AppConfigManagerPath=C:\ProFast\Projects\DotNetAppsV3\pfAppConfigManager\pfAppConfigManager\bin
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


:procexit
exit
