using System.Web.Mvc;

namespace ABC.Infrastructure.Web.Defaults
{
    public static class LayoutHelper
    {
        public static string DefaultLayout(this HtmlHelper _) => "~/Views/Shared/Master.cshtml";
    }
}