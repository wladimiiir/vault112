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
@del "./fonline_test.dll"
cl.exe /nologo /MT /W3 /O2 /Gd /D "__SERVER" /Fo"./fonline_test.obj" /FD /c "./fonline_test.cpp"
link.exe /nologo /dll /incremental:no /machine:I386 "./fonline_test.obj" /out:"./fonline_test.dll"

@: Client
@del "./fonline_test_client.dll"
cl.exe /nologo /MT /W3 /O2 /Gd /D "__CLIENT" /Fo"./fonline_test_client.obj" /FD /c "./fonline_test.cpp"
link.exe /nologo /dll /incremental:no /machine:I386 "./fonline_test_client.obj" /out:"./fonline_test_client.dll"

@: Delete unnecessary stuff
@del "./fonline_test.obj"
@del "./fonline_test.exp"
@del "./fonline_test.lib"
@del "./fonline_test_client.obj"
@del "./fonline_test_client.exp"
@del "./fonline_test_client.lib"
@del "./vc100.idb"

@pause