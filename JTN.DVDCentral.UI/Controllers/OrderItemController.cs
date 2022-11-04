using JTN.DVDCentral.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        // GET: OrderItemController
        public ActionResult Index()
        {
            ViewBag.Title = "Order Items";
            return View(OrderItemManager.Load());
        }

    }
}
