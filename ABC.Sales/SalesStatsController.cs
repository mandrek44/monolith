using System.Linq;
using System.Web.Mvc;

namespace ABC.Sales
{
    public class SalesStatsController : Controller
    {
        private readonly SalesStatsRepoitory _statsRepository;

        public SalesStatsController(SalesStatsRepoitory statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public ActionResult SalesStats()
        {
            return View(new[] { _statsRepository.GetStatsData().Last() });
        }
    }

    public class MonthStatistic
    {
        public string Month { get; set; }

        public decimal Revenue { get; set; }

        public double Target { get; set; }

        public double? Performance { get; set; }
    }
}