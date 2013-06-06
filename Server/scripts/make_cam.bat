@echo off

@: Environment
@set PATH=C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE;%PATH%
@set PATH=C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\BIN;%PATH%
@set LIB=C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\LIB;%LIB%
@set LIB=C:\Program Files (x86)\Microsoft SDKs\Windows\v6.0A\Lib;%LIB%
@set LIB=C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Lib;%LIB%
@set LIB=.\StlPort;%LIB%
@set INCLUDE=C:\Program Files (x86)\Microsoft Visual Studio 10.0\VC\include;%INCLUDE%
@set INCLUDE=.\StlPort;%INCLUDE%

@: Server
@del "./cam.dll"
cl.exe /nologo /MT /W3 /O2 /Gd /D "__SERVER" /Fo"./cam.obj" /FD /c "./cam.cpp"
link.exe /nologo /dll /incremental:no /machine:I386 "./cam.obj" /out:"./cam.dll"

@: Client
@del "./cam_client.dll"
cl.exe /nologo /MT /W3 /O2 /Gd /D "__CLIENT" /Fo"./cam_client.obj" /Fd"./" /FD /c "./cam.cpp"
link.exe /nologo /dll /incremental:no /machine:I386 "./cam_client.obj" /out:"./cam_client.dll"

@: Delete unnecessary stuff
@del "./cam.obj"
@del "./cam.exp"
@del "./cam.lib"
@del "./cam_client.obj"
@del "./cam_client.exp"
@del "./cam_client.lib"
@del "./vc100.idb"

@pause