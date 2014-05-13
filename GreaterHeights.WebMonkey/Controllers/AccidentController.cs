using System.Web.Mvc;
using GreaterHeights.Domain;
using GreaterHeights.Interfaces;

namespace GreaterHeights.WebMonkey.Controllers
{
    using System.Net;

    using Microsoft.Owin.Security.Provider;

    public class AccidentController : Controller
    {
        private IAccidentRepository repository = null;

        public AccidentController(IAccidentRepository accidentRepository)
        {
            this.repository = accidentRepository;
        }

        public ViewResult Create()
        {
            if (repository.Authorised)
                return View(new AccidentReport());

            return View();
        }
    }
}