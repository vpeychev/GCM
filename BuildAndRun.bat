REM Build solution
@msbuild /t:rebuild GCM.sln

REM Run console application
Solution\UI\ConsoleApp\bin\Debug\GCM.UI.exe
