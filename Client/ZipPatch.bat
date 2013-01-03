SET FName=fonline.zip
IF NOT EXIST %FName% GOTO work
IF EXIST %FName% GOTO circle

:circle
FOR /L %%i IN (0,1,9) DO IF EXIST fonline00%%i.zip set Num=%%i
FOR /L %%i IN (10,1,99) DO IF EXIST fonline0%%i.zip set Num=%%i
FOR /L %%i IN (100,1,999) DO IF EXIST fonline%%i.zip set Num=%%i
set /a Num=%Num%+1
SET FName=fonline%Num%.zip
if %Num% lss 100 SET FName=fonline0%Num%.zip
if %Num% lss 10 SET FName=fonline00%Num%.zip
:work
echo *.cache > 7zexc.txt
echo *\.svn >> 7zexc.txt
echo *\*\.svn >> 7zexc.txt
echo *\*\*\.svn >> 7zexc.txt
echo .svn\* >> 7zexc.txt
echo .svn >> 7zexc.txt
echo save >> 7zexc.txt
echo video >> 7zexc.txt
7za.exe a -tzip %FName% .\data\* -x@7zexc.txt -mx1
del 7zexc.txt
del data\art\* /a:-H /s /q
del data\effects\* /a:-H /s /q
del data\sound\* /a:-H /s /q
del data\textures\* /a:-H /s /q
del data\terrain\* /a:-H /s /q
del data\maps\* /a:-H /s /q