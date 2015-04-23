@if exist "%programfiles%\Microsoft Visual Studio 14.0\Common7\Tools" (
	call "%programfiles%\Microsoft Visual Studio 14.0\Common7\Tools\VSVARS32.BAT"
) else (
	if exist "%ProgramFiles(x86)%\Microsoft Visual Studio 14.0\Common7\Tools" (
		call "%ProgramFiles(x86)%\Microsoft Visual Studio 14.0\Common7\Tools\VSVARS32.BAT"
	) else goto end
)

REM Build solution
msbuild /t:rebuild GCM.sln

REM Run test
"%ProgramFiles(x86)%\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" UnitTestProject\bin\Debug\GCM.UnitTest.dll

:end