@shift

set companyname=Svetomech Inc
set prname=gfxGC

if exist hl.exe goto PatchPass
if exist gunman.exe goto PatchPass
if exist gunmanR.exe goto PatchPass
if exist gunmanE.exe goto PatchPass
goto PatchError

:PatchPass
copy /y "%myfiles%\opengl32.dll" "%cd%\opengl32.dll"
copy /y "%myfiles%\QeffectsGL.ini" "%cd%\QeffectsGL.ini"
goto PatchFinish

:PatchFinish
REM !set g_place=%cd%
REM Текст должен быть на 20% круче!
"%myfiles%\gfxGC_msg.exe" "gfxGC_patcher by Svetomech" "Your game has been successfully patched."
REM !start "SvetomechInc" "%myfiles%\self-destruct.exe"
goto Exit

:PatchError
"%myfiles%\gfxGC_msg.exe" "gfxGC_patcher by Svetomech" "It is not correct game directory!"
goto Exit

:Exit
exit
