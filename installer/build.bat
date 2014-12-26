:: version MUST be passed in
SET version=%1


IF "%version%"=="" GOTO :missing
GOTO :continue
:missing
  echo version is required 
  pause
  EXIT /B 0 
:continue


makensis /DPRODUCT_VERSION=%version% BrowseMonkey.nsi