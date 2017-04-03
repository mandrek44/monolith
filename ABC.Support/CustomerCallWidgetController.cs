using System.Linq;
using System.Web.Mvc;

namespace ABC.Support
{
    public class CustomerCallWidgetController : Controller
    {
        private readonly CallsRepository _callsRepository;

        public CustomerCallWidgetController(CallsRepository callsRepository)
        {
            _callsRepository = callsRepository;
        }

        public ActionResult Index()
        {
            return View("Calls", new[] { _callsRepository.GetCallsStatistics().Last() });
        }
    }
}