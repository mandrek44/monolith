using System;
using System.Web.Mvc;
using ABC.Infrastructure.Contracts;

namespace ABC.Support
{
    public class CustomerCallController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
