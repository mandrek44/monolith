using System;

namespace ABC.Infrastructure.Contracts
{
    public class ActionLink
    {
        public ActionLink(string area, string controller,  string action, string title)
        {
            Controller = controller;
            Area = area;
            Action = action;
            Title = title;
        }

        public string Controller { get; }

        public string Area { get; }

        public string Action { get; }

        public string Title { get; }
    }

    public interface IMenuItem
    {
        ActionLink MenuLink { get; }
    }

    public interface IDashboardWidget
    {
        ActionLink WidgetLink { get; }
    }
}