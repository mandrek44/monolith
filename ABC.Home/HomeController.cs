using System.Collections.Generic;
using System.Web.Mvc;
using ABC.Infrastructure.Contracts;

namespace ABC.Home
{
    public class HomeController : Controller
    {
        private readonly IEnumerable<IMenuItem> _menuItems;
        private readonly IEnumerable<IDashboardWidget> _widgets;

        public HomeController(IEnumerable<IMenuItem> menuItems, IEnumerable<IDashboardWidget> widgets )
        {
            _menuItems = menuItems;
            _widgets = widgets;
        }

        public ViewResult Index()
        {
            return View(new Model(_menuItems, _widgets));
        }

        public ViewResult Navigation()
        {
            return View(new Model(_menuItems, _widgets));
        }

        public class Model
        {
            public IEnumerable<IDashboardWidget> Widgets { get; }

            public IEnumerable<IMenuItem> MenuItems { get; }

            public Model(IEnumerable<IMenuItem> menuItems, IEnumerable<IDashboardWidget> widgets)
            {
                Widgets = widgets;
                MenuItems = menuItems;
            }
        }
    }
}