@echo off

set filename=Product_ServiceTestMock.cs

for /f "delims=" %%i in ('git branch --format="%%(refname:short)%"') do (
    git ls-tree --name-only -r %%i | findstr /c:%filename% >nul
    if %errorlevel% equ 0 (
        echo Die Datei %filename% ist im Branch %%i vorhanden.
    )
)

pause