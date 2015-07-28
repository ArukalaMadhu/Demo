::IF EXIST "%PROGRAMFILES(X86)%" (GOTO 64BIT) ELSE (GOTO 32BIT)
:::64BIT
::java -d64 -version >nul 2>&1
::if errorlevel 1 goto maybe32bit


::goto EXIT
:::maybe32bit
::where java >nul 2>&1
::if errorlevel 1 goto nojava
::echo Java is 32 bit
::set JDK="%ProgramFiles(x86)%\Java\jdk*"
::for /d %%i in (%JDK%) do set JAVA_HOME=%%i
::echo %JAVA_HOME%

::set ANT_HOME=%CD%\nant
::set PATH=%PATH%;%ANT_HOME%\bin;%JAVA_HOME%\bin
::echo %PATH%

::pause 
::goto EXIT
:::nojava
::echo Java is not installed


::GOTO END

:::32BIT
::echo 32-bit...
::set JDK="%ProgramFiles%\Java\jdk*"
::for /d %%i in (%JDK%) do set JAVA_HOME=%%i
::echo %JAVA_HOME%

::set ANT_HOME=%CD%\nant
::set PATH=%PATH%;%ANT_HOME%\bin;%JAVA_HOME%\bin
::echo %PATH%


::GOTO END
:::EXIT
:::END
pushd %~dp0%
set PATH=%PATH%;%~dp0%Resources\nant\bin;
call nant
pause
