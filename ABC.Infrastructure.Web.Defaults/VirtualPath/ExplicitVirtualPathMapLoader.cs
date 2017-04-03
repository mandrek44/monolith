using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ABC.Infrastructure.Web.Defaults.VirtualPath
{
    public static class ExplicitVirtualPathMapLoader
    {
        public static Dictionary<string, string> Load(string path)
        {
            return (from fileName in Directory.EnumerateFiles(path, "*.cshtml")
                    let razor = File.ReadAllText(fileName)
                    let match = Regex.Match(razor, @"@\* VirtualPath: (.+) \*@")
                    where match.Success
                    select new { fileName, vp = match.Groups[1].Value }
            ).ToDictionary(r => r.vp, r => r.fileName);
        }
    }
}