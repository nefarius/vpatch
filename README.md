<img src="assets/favicon_128x128.png" align="right" />

# vpatch

[![.NET](https://github.com/nefarius/vpatch/actions/workflows/build.yml/badge.svg)](https://github.com/nefarius/vpatch/actions/workflows/build.yml) ![Requirements](https://img.shields.io/badge/Requires-.NET%208.0-blue.svg) [![Nuget](https://img.shields.io/nuget/v/Nefarius.Tools.Vpatch)](https://www.nuget.org/packages/Nefarius.Tools.Vpatch/) [![Nuget](https://img.shields.io/nuget/dt/Nefarius.Tools.Vpatch)](https://www.nuget.org/packages/Nefarius.Tools.Vpatch/)

Versioning helper command line utility.

## Installation

```PowerShell
dotnet tool install --global Nefarius.Tools.Vpatch
```

## Usage examples

### Stamp `*.rc` files

Read version string from file `version.txt` and replace both `File Version` and `Product Version` attributes in resource file `ExampleProject.rc`:

```cmd
vpatch --stamp-version-from-file version.txt --target-file "%solution%\ExampleProject\ExampleProject.rc" --resource.file-version --resource.product-version
```

Stamp with version from AppVeyor:

```cmd
vpatch --stamp-version "%APPVEYOR_BUILD_VERSION%" --target-file "%solution%\ExampleProject\ExampleProject.rc" --resource.file-version --resource.product-version
```

### Stamp .NET `AssemblyInfo`

Read version string from file `version.txt` and replace both `AssemblyVersion` and `AssemblyFileVersion` attributes in `AssemblyInfo.cs`:

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

### appveyor.yml example

Updates relevant version sections within a Windows Driver Project run on AppVeyor CI:

```yaml
before_build:
- cmd: dotnet tool install --global Nefarius.Tools.Vpatch
- cmd: vpatch --stamp-version "%APPVEYOR_BUILD_VERSION%" --target-file ".\sys\%APPVEYOR_PROJECT_NAME%.vcxproj" --vcxproj.inf-time-stamp
- cmd: vpatch --stamp-version "%APPVEYOR_BUILD_VERSION%" --target-file ".\sys\%APPVEYOR_PROJECT_NAME%.rc" --resource.file-version --resource.product-version
```

## Remarks

If you want to use the INF stamping functionality, your project file needs to have at least a static version number set (like `1.0.0.0`), the asterisk (`*`) will be ignored.
