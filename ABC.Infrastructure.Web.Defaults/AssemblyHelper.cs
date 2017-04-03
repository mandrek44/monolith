using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ABC.Infrastructure.Web.Defaults
{
    public static class AssemblyHelper
    {
        public static string GetAssemblyPhysicalDir(this Assembly assembly)
        {
            return GetAssemblyPhysicalDir(assembly.GetName().Name);
        }

        private static string GetAssemblyPhysicalDir(string assemblyName, [CallerFilePath] string physicalPath = "")
        {
            var thisAssembly = typeof(AssemblyHelper).Assembly.GetName().Name;
            var thisAssemblyNameIndex = physicalPath.IndexOf(thisAssembly);
            return Path.Combine(physicalPath.Substring(0, thisAssemblyNameIndex), assemblyName);
        }
    }
}