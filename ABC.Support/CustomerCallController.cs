using System;
using System.Web.Mvc;
using ABC.Infrastructure.Contracts;

namespace ABC.Support
{
    public class CustomerCallController : Controller
    {
        private readonly CallsRepository _callsRepository;

        public CustomerCallController(CallsRepository callsRepository)
        {
            _callsRepository = callsRepository;
        }

        public ActionResult Index()
        {
            return View(_callsRepository.GetCallsStatistics());
        }
    }
}
