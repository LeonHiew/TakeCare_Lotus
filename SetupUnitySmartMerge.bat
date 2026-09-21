@echo off

set "UNITYMERGE=C:\Program Files\Unity\Hub\Editor\2022.3.62f3\Editor\Data\Tools\UnityYAMLMerge.exe"

if not exist "%UNITYMERGE%" (
    echo UnityYAMLMerge was not found.
    echo Make sure Unity 2022.3.62f3 is installed through Unity Hub.
    pause
    exit /b 1
)

git config --local merge.tool unityyamlmerge
git config --local mergetool.unityyamlmerge.trustExitCode false
git config --local mergetool.unityyamlmerge.cmd "'C:/Program Files/Unity/Hub/Editor/2022.3.62f3/Editor/Data/Tools/UnityYAMLMerge.exe' merge -p \"$BASE\" \"$REMOTE\" \"$LOCAL\" \"$MERGED\""

echo.
echo Unity Smart Merge configured successfully for this repository.
echo When a Unity scene or prefab conflicts, run:
echo     git mergetool
echo.
pause