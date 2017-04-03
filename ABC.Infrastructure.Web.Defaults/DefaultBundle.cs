using System.Web.Optimization;

namespace ABC.Infrastructure.Web.Defaults
{
    public class DefaultBundle
    {
        public const string CssName = "~/abc.css";
        public const string JsName = "~/abc.js";

        public static Bundle Css => BundleTable.Bundles.GetBundleFor(CssName);
        public static Bundle Js => BundleTable.Bundles.GetBundleFor(JsName);
    }
}