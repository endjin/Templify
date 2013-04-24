@echo off
setlocal
REM Ensure we know whether we are running on a 32it or 64bit system
REM IF "%PROCESSOR_ARCHITECTURE%" == "x86" SET FRAMEWORKDIR=Framework
REM IF "%PROCESSOR_ARCHITECTURE%" == "AMD64" SET FRAMEWORKDIR=Framework64
SET MSBUILDEXE=%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe

REM set msbuildemitsolution=1

%MSBUILDEXE% build.proj /m:1 /v:m /flp:verbosity=diagnostic;logfile=build.log /p:IsDesktopBuild=true

pause