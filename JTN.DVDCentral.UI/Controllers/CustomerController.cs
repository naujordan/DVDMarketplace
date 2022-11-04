using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerController
        public ActionResult Index()
        {
            ViewBag.Title = "Customers";
            return View(CustomerManager.Load());
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Customers";
            return View(CustomerManager.LoadById(id));
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {        
            if (Authenticate.isAuthenticated(HttpContext))
            {
                ViewBag.Title = "Add a Customer";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnuri = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                CustomerManager.Insert(customer);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Add a Customer";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            if (Authenticate.isAuthenticated(HttpContext))
            {
                ViewBag.Title = "Update a Customer";
                return View(CustomerManager.LoadById(id));
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnuri = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                CustomerManager.Update(customer);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Update a Customer";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Customer";
            return View(CustomerManager.LoadById(id));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                CustomerManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Delete a Customer";
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
