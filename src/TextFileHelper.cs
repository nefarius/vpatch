using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Build.Construction;

namespace vpatch;

public static class TextFileHelper
{
    public static void RegexReplace(string path, string pattern, string replacement, Encoding encoding)
    {
        string content;

        using (StreamReader reader = new(path, encoding))
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

    public static void PatchProjectFile(string projectFile, string propertyName, string replacement)
    {
        ProjectRootElement? projectRootElement = ProjectRootElement.Open(projectFile);

        if (projectRootElement is null)
        {
            throw new IOException("Couldn't open project file for reading.");
        }

        try
        {
            projectRootElement.AddProperty(propertyName, replacement);

            projectRootElement.Save(projectFile);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to parse project file.", ex);
        }
    }

    public static void PatchInfFile(string projectFile, Version version)
    {
        ProjectRootElement? root = ProjectRootElement.Open(projectFile);

        if (root is null)
        {
            throw new IOException("Couldn't open project file for reading.");
        }

        foreach (ProjectItemDefinitionGroupElement itemDefinitionGroup in root.ItemDefinitionGroups)
        {
            ProjectItemDefinitionElement? infElement =
                itemDefinitionGroup.ItemDefinitions.SingleOrDefault(element => element.ElementName.Equals("Inf"));

            if (infElement is null)
            {
                continue;
            }

            if (infElement.Children.SingleOrDefault(element => element.ElementName.Equals("TimeStamp")) is ProjectMetadataElement timeStamp)
            {
                timeStamp.Value = version.ToString();
            }
            else
            {
                infElement.AddMetadata("TimeStamp", version.ToString());
            }
        }
    }
}