using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ABC.Infrastructure.Web.Defaults.VirtualPath
{
    public abstract class PhysicalDirProvider
    {
        private readonly PhysicalDirProvider _successor;

        protected PhysicalDirProvider(PhysicalDirProvider successor)
        {
            _successor = successor;
        }

        protected abstract bool CanProcess(string virtualPath);

        protected abstract string Process(string virtualPath);

        public string GetPhysicalDir(string virtualPath)
        {
            if (CanProcess(virtualPath))
                return Process(virtualPath);

            if (_successor != null)
                return _successor.GetPhysicalDir(virtualPath);

            throw new InvalidOperationException(
                $"Virtual path {virtualPath} canot be handled by any PhysicalDirProvider");
        }

        protected static string GetStandardProductDir(string assemblyName, [CallerFilePath] string physicalPath = "")
        {
            return Path.Combine(Directory.GetParent(Path.GetDirectoryName(physicalPath)).Parent.FullName, assemblyName);
        }



        public class Default : PhysicalDirProvider
        {
            protected override bool CanProcess(string virtualPath)
            {
                return true;
            }

            protected override string Process(string virtualPath)
            {
                return virtualPath;
            }

            public Default() : base(null)
            {
            }
        }
    }
}