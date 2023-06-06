@echo off
setlocal

rem Funktion zum Löschen der Ordner
:DeleteFolders
for /d /r %%G in (*) do (
    if /i "%%~nxG"=="bin" (
        echo Lösche Ordner "%%G"
        rd /s /q "%%G"
    ) else if /i "%%~nxG"=="obj" (
        echo Lösche Ordner "%%G"
        rd /s /q "%%G"
    )
)

rem Startpunkt: Aktuelles Verzeichnis
call :DeleteFolders "%CD%"

endlocal
