using System.Collections.Generic;

namespace ABC.Infrastructure.Web.Defaults.VirtualPath
{
    public interface IPhysicalPathProvider
    {
        string GetPhysicalPath(string virtualPath);
    }

    public class SimpleMapPathProvider : IPhysicalPathProvider
    {
        private readonly Dictionary<string, string> _pathMap;

        public SimpleMapPathProvider(Dictionary<string, string> pathMap)
        {
            _pathMap = pathMap;
        }

        public string GetPhysicalPath(string virtualPath)
        {
            if (!virtualPath.StartsWith("~"))
                virtualPath = "~" + virtualPath;
            return _pathMap.ContainsKey(virtualPath) ? _pathMap[virtualPath] : null;
        }
    }
}