using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Build.Construction;

namespace vpatch
{
    public static class TextFileHelper
    {
        public static void RegexReplace(string path, string pattern, string replacement, Encoding encoding)
        {
            string content;

            using (var reader = new StreamReader(path, encoding))
            {
                content = reader.ReadToEnd();
            }

            content = Regex.Replace(content, pattern, replacement, RegexOptions.Multiline);

            File.WriteAllText(path, content, encoding);
        }

        public static void RegexReplace(string path, string pattern, string replacement)
        {
            RegexReplace(path, pattern, replacement, Encoding.UTF8);
        }

        public static void PatchProjectFile(string path, string propertyName, string replacement)
        {
            var projectRootElement = ProjectRootElement.Open(path);

            if (projectRootElement is null)
                throw new IOException("Couldn't open project file for reading.");

            projectRootElement.AddProperty(propertyName, replacement);

            projectRootElement.Save();
        }
    }
}