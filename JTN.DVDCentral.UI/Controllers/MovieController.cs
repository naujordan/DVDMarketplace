using JTN.DVDCentral.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        // GET: MovieController
        public ActionResult Index()
        {
            ViewBag.Title = "Movies";
            return View(MovieManager.Load());
        }

        public ActionResult Browse(int id)
        {
            ViewBag.Title = "Movies";
            return View(nameof(Index), MovieManager.Load(id));
        }

    }
}
