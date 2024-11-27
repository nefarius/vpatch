# <img src="assets/favicon_128x128.png" align="left" />vpatch

[![.NET](https://github.com/nefarius/vpatch/actions/workflows/build.yml/badge.svg)](https://github.com/nefarius/vpatch/actions/workflows/build.yml)
![Requirements](https://img.shields.io/badge/Requires-.NET%209.0-blue.svg)
[![Nuget](https://img.shields.io/nuget/v/Nefarius.Tools.Vpatch)](https://www.nuget.org/packages/Nefarius.Tools.Vpatch/)
[![Nuget](https://img.shields.io/nuget/dt/Nefarius.Tools.Vpatch)](https://www.nuget.org/packages/Nefarius.Tools.Vpatch/)

Versioning helper command line utility.

## About

This command line tool can version-stamp the following project files and types:

- Legacy .NET Framework `.csproj` and `AssemblyInfo.cs` files
- Current (SDK-format) .NET Core `.csproj` files
- C/C++ Win32 [VERSIONINFO resource](https://learn.microsoft.com/en-us/windows/win32/menurc/versioninfo-resource) files
- Stamping INF version info
  in [driver projects](https://learn.microsoft.com/en-us/windows-hardware/drivers/develop/stampinf-properties-for-driver-projects)

It is most useful in Continuous Integration setups to avoid having to deal with custom scripts to bump the version
number with each build or release.

## Installation

```PowerShell
dotnet tool install --global Nefarius.Tools.Vpatch
```

## Usage examples

### Stamp `*.rc` files

Read version string from file `version.txt` and replace both `File Version` and `Product Version` attributes in resource
file `ExampleProject.rc`:

```cmd
vpatch --stamp-version-from-file version.txt --target-file "%solution%\ExampleProject\ExampleProject.rc" --resource.file-version --resource.product-version
```

Stamp with version from AppVeyor:

```cmd
vpatch --stamp-version "%APPVEYOR_BUILD_VERSION%" --target-file "%solution%\ExampleProject\ExampleProject.rc" --resource.file-version --resource.product-version
```

### Stamp .NET Framework `AssemblyInfo`

Read version string from file `version.txt` and replace both `AssemblyVersion` and `AssemblyFileVersion` attributes in
`AssemblyInfo.cs`:

```cmd
vpatch --stamp-version-from-file version.txt --target-file "%solution%\ExampleProject\Properties\AssemblyInfo.cs" --assembly.version --assembly.file-version
```

### Stamp Windows Driver Project

Read version string from file `version.txt` and replace `TimeStamp` attribute in `ExampleProject.vcxproj`:

```cmd
vpatch --stamp-version-from-file version.txt --target-file "%solution%\ExampleProject\ExampleProject.vcxproj" --vcxproj.inf-time-stamp
```

Stamp with version from AppVeyor:

```cmd
vpatch --stamp-version "%APPVEYOR_BUILD_VERSION%" --target-file "%solution%\ExampleProject\ExampleProject.vcxproj" --vcxproj.inf-time-stamp
```

### `appveyor.yml` example

Updates relevant version sections within a Windows Driver Project run on AppVeyor CI:

```yaml
before_build:
  - cmd: dotnet tool install --global Nefarius.Tools.Vpatch
  - cmd: vpatch --stamp-version "%APPVEYOR_BUILD_VERSION%" --target-file ".\sys\%APPVEYOR_PROJECT_NAME%.vcxproj" --vcxproj.inf-time-stamp
  - cmd: vpatch --stamp-version "%APPVEYOR_BUILD_VERSION%" --target-file ".\sys\%APPVEYOR_PROJECT_NAME%.rc" --resource.file-version --resource.product-version
```
