﻿using System.Text;
using CommandLine;

namespace vpatch;

internal class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                o.UseVersionFromFile();

                if (o.OverwriteAssemblyVersion)
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.AssemblyVersion.Key,
                        string.Format(Patterns.AssemblyVersion.Value, o.Version));

                if (o.OverwriteAssemblyFileVersion)
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.AssemblyFileVersion.Key,
                        string.Format(Patterns.AssemblyFileVersion.Value, o.Version));

                if (o.OverwriteResourceFileVersion)
                {
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceFileVersion.Key,
                        string.Format(Patterns.ResourceFileVersion.Value, o.Version.ToString().Replace('.', ',')),
                        Encoding.Default);
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceStringFileVersion.Key,
                        string.Format(Patterns.ResourceStringFileVersion.Value, o.Version), Encoding.Default);
                }

                if (o.OverwriteResourceProductVersion)
                {
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceProductVersion.Key,
                        string.Format(Patterns.ResourceProductVersion.Value,
                            o.Version.ToString().Replace('.', ',')),
                        Encoding.Default);
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.ResourceStringProductVersion.Key,
                        string.Format(Patterns.ResourceStringProductVersion.Value, o.Version), Encoding.Default);
                }

                if (o.OverwriteVcxprojInfTimeStamp)
                    TextFileHelper.RegexReplace(o.TargetFile, Patterns.VcxprojInfTimeStamp.Key,
                        string.Format(Patterns.VcxprojInfTimeStamp.Value, o.Version));

                if (o.OverwriteNetCoreAssemblyVersion)
                    TextFileHelper.PatchProjectFile(o.TargetFile, "AssemblyVersion", o.Version.ToString());

                if (o.OverwriteNetCoreFileVersion)
                    TextFileHelper.PatchProjectFile(o.TargetFile, "FileVersion", o.Version.ToString());

                if (o.OverwriteNetCoreVersion)
                    TextFileHelper.PatchProjectFile(o.TargetFile, "Version", o.Version.ToString());
            });
    }
}