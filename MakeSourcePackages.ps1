# Patching all sources with internal attribute for use in source packages

$files=get-childitem . *.cs -rec
foreach ($file in $files)
{
(Get-Content $file.PSPath) |
Foreach-Object {$_ -replace "public static class", "internal static class"} |
Foreach-Object {$_ -replace "public sealed class", "internal sealed class"} |
Foreach-Object {$_ -replace "public class", "internal class"} |
Foreach-Object {$_ -replace "public interface", "internal interface"} |
Set-Content -Encoding UTF8 $file.PSPath
}

# Getting packages versions

$xmlVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/src/Simplify.Xml/bin/Release/Simplify.Xml.dll").FileVersion
$stringVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/src/Simplify.String/bin/Release/Simplify.String.dll").FileVersion
$systemVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/src/Simplify.System/bin/Release/Simplify.System.dll").FileVersion

# Packing source packages

src\.nuget\NuGet.exe pack src/Simplify.Xml/Simplify.Xml.Sources.nuspec -Version $xmlVersion
src\.nuget\NuGet.exe pack src/Simplify.String/Simplify.String.Sources.nuspec -Version $stringVersion
src\.nuget\NuGet.exe pack src/Simplify.System/Simplify.System.Sources.nuspec -Version $systemVersion

# Publishing to Appveyor artifacts

Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
