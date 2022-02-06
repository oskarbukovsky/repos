#!/usr/bin/bash
cd BroadCast/
dotnet add package Newtonsoft.Json --version 13.0.1
cd ..
msbuild -consoleLoggerParameters:Summary -v:q
