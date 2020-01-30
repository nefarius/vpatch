# vpatch

Versioning helper command line utility

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
