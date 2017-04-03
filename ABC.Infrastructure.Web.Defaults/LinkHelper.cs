using System.Web.Mvc;
using System.Web.Mvc.Html;
using ABC.Infrastructure.Contracts;

namespace ABC.Infrastructure.Web.Defaults
{
    public static class LinkHelper
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, ActionLink link)
        {
            var controllerName = link.Controller.EndsWith("Controller")
                ? link.Controller.Substring(0, link.Controller.Length - "Controller".Length)
                : link.Controller;

            return htmlHelper.ActionLink(link.Title, link.Action, controllerName, new { link.Area }, new { });
        }

        public static MvcHtmlString Action(this HtmlHelper htmlHelper, ActionLink link)
        {
            var controllerName = link.Controller.EndsWith("Controller")
                ? link.Controller.Substring(0, link.Controller.Length - "Controller".Length)
                : link.Controller;

            return htmlHelper.Action(link.Action, controllerName, new { link.Area });
        }
    }
}