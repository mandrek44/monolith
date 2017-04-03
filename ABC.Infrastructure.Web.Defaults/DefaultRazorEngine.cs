using System;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;

namespace ABC.Infrastructure.Web.Defaults
{
    public class DefaultRazorEngine
    {
        public static PrecompiledMvcEngine Initialize(Type t)
        {
            var engine = new PrecompiledMvcEngine(t.Assembly)
            {
                UsePhysicalViewsIfNewer = true,
                AreaViewLocationFormats = new[] {"~/{2}/{1}/{0}.cshtml"}
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);

            return engine;
        }
    }
}