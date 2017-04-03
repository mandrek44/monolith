using System;
using System.Web.Mvc;

namespace ABC.Sales
{
    public class SalesController : Controller
    {
        private readonly SalesStatsRepoitory _statsRepository;

        public SalesController(SalesStatsRepoitory statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public ActionResult Index()
        {
            return View(_statsRepository.GetStatsData());
        }
    }
}