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
        new(@"FILEVERSION[ \\t]*\d*,\d*,\d*,\d*", "FILEVERSION {0}");

    public static KeyValuePair<string, string> ResourceProductVersion =>
        new(@"PRODUCTVERSION[ \\t]*\d*,\d*,\d*,\d*", "PRODUCTVERSION {0}");

    public static KeyValuePair<string, string> ResourceStringFileVersion
        => new("^[ \\t]*VALUE[ \\t]*\"FileVersion\"[ \\t]*,[ \\t]*\"\\d*\\.\\d*\\.\\d*\\.\\d*\"",
            "VALUE \"FileVersion\", \"{0}\"");

    public static KeyValuePair<string, string> ResourceStringProductVersion
        => new("^[ \\t]*VALUE[ \\t]*\"ProductVersion\"[ \\t]*,[ \\t]*\"\\d*\\.\\d*\\.\\d*\\.\\d*\"",
            "VALUE \"ProductVersion\", \"{0}\"");
}