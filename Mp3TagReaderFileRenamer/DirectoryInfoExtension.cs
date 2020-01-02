using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class DirectoryInfoExtension
{
    public static IEnumerable<string> DirectorySplit(this DirectoryInfo Dir)
    {
        while (Dir != null)
        {
            yield return Dir.Name;
            Dir = Dir.Parent;
        }
    }

    public static string DirectoryPart(this DirectoryInfo Dir, int PartNr)
    {
        string[] Parts = Dir.DirectorySplit().ToArray();
        int L = Parts.Length;
        return PartNr >= 0 && PartNr < L ? Parts[L - 1 - PartNr] : "";

    }

    public static int NumberOfParts(this DirectoryInfo Dir)
    {
        string[] Parts = Dir.DirectorySplit().ToArray();
        return Parts.Length;


    }

    public static string[] Parts(this DirectoryInfo Dir)
    {
        string[] Parts = Dir.DirectorySplit().ToArray();
        return Parts;
    }
}
