@echo off
c:
cd C:\ProFast\Projects\DotNetAppsV3\pfDataExtractor\pfDataExtractor\bin\Release\
@echo Data extractor running...
pfDataExtractor.exe NOUI C:\Users\Mike\Documents\PFApps\pfDataExtractor\Definitions\AW_OrderHeader_DatesTest.exdef Testbatch.log
@echo Return code for extraction: %errorlevel%
if errorlevel 1 goto errexit
@echo ..DataExtractor finished.
goto exit
:errexit
@echo ..DataExtractor finished with error.
:exit
pause