using System.Collections.Generic;

namespace vpatch
{
    public class Patterns
    {
        public static KeyValuePair<string, string> AssemblyVersion
            =>
                new KeyValuePair<string, string>(@"^ *\[assembly[\: ]*AssemblyVersion.*\]",
                    "[assembly: AssemblyVersion(\"{0}\")]");

        public static KeyValuePair<string, string> AssemblyFileVersion =>
            new KeyValuePair<string, string>(@"^ *\[assembly[\: ]*AssemblyFileVersion.*\]",
                "[assembly: AssemblyFileVersion(\"{0}\")]");

        public static KeyValuePair<string, string> ResourceFileVersion =>
            new KeyValuePair<string, string>(@"FILEVERSION[ \\t]*\d*,\d*,\d*,\d*", "FILEVERSION {0}");

        public static KeyValuePair<string, string> ResourceProductVersion =>
            new KeyValuePair<string, string>(@"PRODUCTVERSION[ \\t]*\d*,\d*,\d*,\d*", "PRODUCTVERSION {0}");

        public static KeyValuePair<string, string> ResourceStringFileVersion
            => new KeyValuePair<string, string>("^[ \\t]*VALUE[ \\t]*\"FileVersion\"[ \\t]*,[ \\t]*\"\\d*\\.\\d*\\.\\d*\\.\\d*\"",
                "VALUE \"FileVersion\", \"{0}\"");

        public static KeyValuePair<string, string> ResourceStringProductVersion
            => new KeyValuePair<string, string>("^[ \\t]*VALUE[ \\t]*\"ProductVersion\"[ \\t]*,[ \\t]*\"\\d*\\.\\d*\\.\\d*\\.\\d*\"",
                "VALUE \"ProductVersion\", \"{0}\"");

        public static KeyValuePair<string, string> VcxprojInfTimeStamp
            =>
                new KeyValuePair<string, string>(
                    "<Inf>(\\n|\\r|\\r\\n) *<TimeStamp>\\d*\\.\\d*\\.\\d*\\.\\d*<\\/TimeStamp>(\\n|\\r|\\r\\n) *<\\/Inf>",
                    "<Inf>\r\n      <TimeStamp>{0}</TimeStamp>\r\n    </Inf>");
    }
}