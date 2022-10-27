using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class DirectorController : Controller
    {
        // GET: DirectorController
        public ActionResult Index()
        {
            ViewBag.Title = "Directors";
            return View(DirectorManager.Load());
        }

        // GET: DirectorController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Directors";
            return View(DirectorManager.LoadById(id));
        }

        // GET: DirectorController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add a Director";
            return View();
        }

        // POST: DirectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Director director)
        {
            try
            {
                DirectorManager.Insert(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Add a Director";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: DirectorController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Update a Director";
            return View(DirectorManager.LoadById(id));
        }

        // POST: DirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Director director)
        {
            try
            {
                DirectorManager.Update(director);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Update a Director";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: DirectorController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Director";
            return View(DirectorManager.LoadById(id));
        }

        // POST: DirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                DirectorManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete a Director";
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
