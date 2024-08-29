using System.Diagnostics.CodeAnalysis;
using System.Text;

using CommandLine;

using Microsoft.Build.Locator;

namespace vpatch;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class Program
{
    private static void Main(string[] args)
    {
        MSBuildLocator.RegisterDefaults();

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                o.UseVersionFromFile();

                #region .NET Framework specific
                
                if (o.OverwriteAssemblyVersion)
                {
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.AssemblyVersion.Key,
                        string.Format(Patterns.AssemblyVersion.Value, o.Version));
                }

                if (o.OverwriteAssemblyFileVersion)
                {
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.AssemblyFileVersion.Key,
                        string.Format(Patterns.AssemblyFileVersion.Value, o.Version));
                }

                #endregion

                #region C++/Driver project specific

                string versionString = string.IsNullOrEmpty(o.VersionString) ? o.Version.ToString() : o.VersionString;
                
                if (o.OverwriteResourceFileVersion)
                {
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceFileVersion.Key,
                        string.Format(Patterns.ResourceFileVersion.Value, o.Version.ToString().Replace('.', ',')),
                        Encoding.Default);
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceStringFileVersion.Key,
                        string.Format(Patterns.ResourceStringFileVersion.Value, versionString), Encoding.Default);
                }

                if (o.OverwriteResourceProductVersion)
                {
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceProductVersion.Key,
                        string.Format(Patterns.ResourceProductVersion.Value,
                            o.Version.ToString().Replace('.', ',')),
                        Encoding.Default);
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceStringProductVersion.Key,
                        string.Format(Patterns.ResourceStringProductVersion.Value, versionString), Encoding.Default);
                }

                if (o.OverwriteVcxprojInfTimeStamp)
                {
                    TextFileHelper.PatchInfFile(o.TargetFile, o.Version);
                }
                
                #endregion

                #region .NET Core specific

                if (o.OverwriteNetCoreAssemblyVersion)
                {
                    TextFileHelper.PatchProjectFile(o.TargetFile, "AssemblyVersion", o.Version.ToString());
                }

                if (o.OverwriteNetCoreFileVersion)
                {
                    TextFileHelper.PatchProjectFile(o.TargetFile, "FileVersion", o.Version.ToString());
                }

                if (o.OverwriteNetCoreVersion)
                {
                    TextFileHelper.PatchProjectFile(o.TargetFile, "Version", o.Version.ToString());
                }

                #endregion
            });
    }
}