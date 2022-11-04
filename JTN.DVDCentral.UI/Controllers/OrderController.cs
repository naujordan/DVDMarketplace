using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        public ActionResult Index()
        {
            ViewBag.Title = "Orders";
            return View(OrderManager.Load());
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Orders";
            return View(OrderManager.LoadById(id));
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            if (Authenticate.isAuthenticated(HttpContext))
            {
                ViewBag.Title = "Add an Order";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnuri = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                OrderManager.Insert(order);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Add an Order";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.isAuthenticated(HttpContext))
            {
                ViewBag.Title = "Update an Order";
                return View(OrderManager.LoadById(id));
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnuri = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                OrderManager.Update(order);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Update an Order";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete an Order";
            return View(OrderManager.LoadById(id));
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                OrderManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Delete an Order";
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
