using System;
using System.Web.Mvc;
using System.Web.WebPages;
using ABC.Infrastructure.Web.Defaults.VirtualPath;
using Autofac;
using RazorGenerator.Mvc;

namespace ABC.Infrastructure.Web.Defaults
{
    public class DefaultRazorEngine
    {
        public static PrecompiledMvcEngine Initialize(Type t, ContainerBuilder container)
        {
            var pathProvider = GetPathProvider(t);
            var engine = new PhysicalWrapperEngine(t.Assembly, pathProvider)
            {
                UsePhysicalViewsIfNewer = true,
                AreaViewLocationFormats = new[] {"~/{2}/{1}/{0}.cshtml"}
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);

            container.RegisterInstance(pathProvider).AsImplementedInterfaces();

            return engine;
        }

        private static IPhysicalPathProvider GetPathProvider(Type t)
        {
#if DEBUG
            return new SimpleMapPathProvider(ExplicitVirtualPathMapLoader.Load(t.Assembly.GetAssemblyPhysicalDir()));
#else
            return new SimpleMapPathProvider(new Dictionary<string, string>());
#endif
        }
    }
}