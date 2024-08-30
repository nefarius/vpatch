using System.Collections.Generic;

namespace vpatch;

public static class Patterns
{
    public static KeyValuePair<string, string> AssemblyVersion
        =>
            new(@"^ *\[assembly[\: ]*AssemblyVersion.*\]",
                "[assembly: AssemblyVersion(\"{0}\")]");

    public static KeyValuePair<string, string> AssemblyFileVersion =>
        new(@"^ *\[assembly[\: ]*AssemblyFileVersion.*\]",
            "[assembly: AssemblyFileVersion(\"{0}\")]");

    public static KeyValuePair<string, string> ResourceFileVersion =>
        new(@"FILEVERSION(?<spacing>[ \\t]*)\d*,\d*,\d*,\d*", "FILEVERSION${{spacing}}{0}");

    public static KeyValuePair<string, string> ResourceProductVersion =>
        new(@"PRODUCTVERSION(?<spacing>[ \\t]*)\d*,\d*,\d*,\d*", "PRODUCTVERSION${{spacing}}{0}");

    public static KeyValuePair<string, string> ResourceStringFileVersion
        => new("^(?<indent>[ \\t]*)VALUE[ \\t]*\"FileVersion\"[ \\t]*,(?<spacing>[ \\t]*)\"\\d*\\.\\d*\\.\\d*\\.\\d*\"",
            "${{indent}}VALUE \"FileVersion\",${{spacing}}\"{0}\"");

    public static KeyValuePair<string, string> ResourceStringProductVersion
        => new("^(?<indent>[ \\t]*)VALUE[ \\t]*\"ProductVersion\"[ \\t]*,(?<spacing>[ \\t]*)\"\\d*\\.\\d*\\.\\d*\\.\\d*\"",
            "${{indent}}VALUE \"ProductVersion\",${{spacing}}\"{0}\"");
}