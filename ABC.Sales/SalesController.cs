using System;
using System.Web.Mvc;

namespace ABC.Sales
{
    public class SalesController : Controller
    {
        private readonly ISalesMonthPerformance _performanceService;

        public SalesController(ISalesMonthPerformance performanceService)
        {
            _performanceService = performanceService;
        }

        public ActionResult Index()
        {
            ValueTuple<string, double?>[] valueTuples = new [] { ("January", _performanceService.GetPerformance(0)),
                ("February", _performanceService.GetPerformance(1)),
                ("March", _performanceService.GetPerformance(2))};
            return View(valueTuples);
        }
    }
}