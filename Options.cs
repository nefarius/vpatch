using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using CommandLine;

namespace vpatch;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class Options
{
    [Option("stamp-version", SetName = "version",
        HelpText = "The version information to set in the target file (e.g. \"1.2.0.0\").")]
    public Version Version { get; set; }

    [Option("stamp-version-from-file", SetName = "version-file",
        HelpText = "A text file containing one line that gets parsed as the target version.")]
    public string TargetVersionFile { get; set; }

    [Option("target-file", Required = true,
        HelpText = "The target file to get parsed and modified.")]
    public string TargetFile { get; set; }

    [Option("assembly.version",
        HelpText = "Update 'AssemblyVersion' attribute in the specified assembly source file.")]
    public bool OverwriteAssemblyVersion { get; set; }

    [Option("assembly.file-version",
        HelpText = "Update 'AssemblyFileVersion' attribute in the specified assembly source file.")]
    public bool OverwriteAssemblyFileVersion { get; set; }

    [Option("resource.file-version",
        HelpText = "Update 'FILEVERSION' and 'FileVersion' values in the specified resource file.")]
    public bool OverwriteResourceFileVersion { get; set; }

    [Option("resource.product-version",
        HelpText = "Update 'PRODUCTVERSION' and 'ProductVersion' values in the specified resource file.")]
    public bool OverwriteResourceProductVersion { get; set; }

    [Option("vcxproj.inf-time-stamp",
        HelpText = "Update '<TimeStamp>' value in the specified Visual Studio project file.")]
    public bool OverwriteVcxprojInfTimeStamp { get; set; }

    [Option("csproj.assembly-version",
        HelpText = "Update 'AssemblyVersion' attribute in the specified .csproj project file.")]
    public bool OverwriteNetCoreAssemblyVersion { get; set; }

    [Option("csproj.file-version",
        HelpText = "Update 'FileVersion' attribute in the specified .csproj project file.")]
    public bool OverwriteNetCoreFileVersion { get; set; }

    [Option("csproj.version",
        HelpText = "Update 'Version' attribute in the specified .csproj project file.")]
    public bool OverwriteNetCoreVersion { get; set; }

    public void UseVersionFromFile()
    {
        if (!File.Exists(TargetVersionFile))
        {
            return;
        }

        Version = Version.Parse(File.ReadAllText(TargetVersionFile).Trim(' ', '\n', '\r'));
    }
}