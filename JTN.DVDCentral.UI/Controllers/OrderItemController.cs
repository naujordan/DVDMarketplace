using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
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

        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete an Order Item";
            return View(OrderItemManager.LoadById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, OrderItem orderItem)
        {
            try
            {
                OrderItemManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete an Order Item";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

    }
}
