using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Caching;
using System.Web.Hosting;

namespace ABC.Infrastructure.Web.Defaults.VirtualPath
{
    /// <summary>
    /// The goal of this VirtualPathProvider is to aid the development of application views. 
    /// It's not meant to be used in production server, as it will try to seek paths on hard drive
    /// based on the location of source file during compilation!
    /// </summary>
    public class ABCVirtualPathProvider : VirtualPathProvider
    {
        private readonly IEnumerable<IPhysicalPathProvider> _pathProviders;
        private static readonly PhysicalDirProvider _physicalDirProvider;

        static ABCVirtualPathProvider()
        {
            _physicalDirProvider = new PhysicalDirProvider.Default();
        }

        public ABCVirtualPathProvider(IPhysicalPathProvider[] pathProviders)
        {
            _pathProviders = pathProviders;
        }

        public override bool FileExists(string virtualPath)
        {
            var physicalDir = GetPhysicalDir(virtualPath);
            if (File.Exists(physicalDir))
                return true;

            return Previous.FileExists(virtualPath);
        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            var physicalDir = GetPhysicalDir(virtualPath);
            if (File.Exists(physicalDir))
            {
                var fileInfo = new FileInfo(physicalDir);
                return (fileInfo.Name + fileInfo.Length + fileInfo.LastWriteTimeUtc + fileInfo.CreationTimeUtc)
                    .GetHashCode()
                    .ToString();
            }

            return Previous.GetFileHash(virtualPath, virtualPathDependencies);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            var physicalDir = GetPhysicalDir(virtualPath);
            if (File.Exists(physicalDir))
                return new CacheDependency(physicalDir);

            return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            var dir = GetPhysicalDir(virtualDir);
            if (Directory.Exists(dir))
                return true;

            return Previous.DirectoryExists(virtualDir);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            var dir = GetPhysicalDir(virtualDir);
            if (Directory.Exists(dir))
                return new DirectoryInfoVirtualDirectory(virtualDir, new DirectoryInfo(dir));

            return Previous.GetDirectory(virtualDir);
        }

        public override string GetCacheKey(string virtualPath)
        {
            var dir = GetPhysicalDir(virtualPath);
            if (Directory.Exists(dir) || File.Exists(dir))
                return null; // returning null will force the BuildManager to calculate the key

            return Previous.GetCacheKey(virtualPath);
        }

        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            return Previous.CombineVirtualPaths(basePath, relativePath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            var dir = GetPhysicalDir(virtualPath);
            if (File.Exists(dir))
                return new FileInfoVirtualFile(virtualPath, new FileInfo(dir));

            return Previous.GetFile(virtualPath);
        }

        private string GetPhysicalDir(string virtualPath)
        {
            var physicalPath = _pathProviders.Select(provider => provider.GetPhysicalPath(virtualPath)).FirstOrDefault(path => path != null);
            if (physicalPath != null)
                return physicalPath;

            return _physicalDirProvider.GetPhysicalDir(virtualPath);
        }

        private class FileInfoVirtualFile : VirtualFile
        {
            private readonly FileInfo _file;

            public FileInfoVirtualFile(string virtualPath, FileInfo file)
                : base(virtualPath)
            {
                _file = file;
            }

            public override Stream Open()
            {
                return _file.OpenRead();
            }
        }

        private class DirectoryInfoVirtualDirectory : VirtualDirectory
        {
            private readonly DirectoryInfo _dir;

            public DirectoryInfoVirtualDirectory(string virtualPath, DirectoryInfo dir) : base(virtualPath)
            {
                _dir = dir;
            }

            public override IEnumerable Directories => _dir.EnumerateDirectories();

            public override IEnumerable Files => _dir.EnumerateFiles();

            public override IEnumerable Children => _dir.EnumerateFiles();
        }
    }
}