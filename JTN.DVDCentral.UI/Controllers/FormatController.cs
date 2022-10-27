using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class FormatController : Controller
    {
        // GET: FormatController
        public ActionResult Index()
        {
            ViewBag.Title = "Formats";
            return View(FormatManager.Load());
        }

        // GET: FormatController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Formats";
            return View(FormatManager.LoadById(id));
        }

        // GET: FormatController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add a Format";
            return View();
        }

        // POST: FormatController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Format format)
        {
            try
            {
                FormatManager.Insert(format);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Add a Format";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: FormatController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Update a Format";
            return View(FormatManager.LoadById(id));
        }

        // POST: FormatController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Format format)
        {
            try
            {
                FormatManager.Update(format);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Update a Format";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: FormatController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Format";
            return View(FormatManager.LoadById(id));
        }

        // POST: FormatController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                FormatManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete a Format";
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
