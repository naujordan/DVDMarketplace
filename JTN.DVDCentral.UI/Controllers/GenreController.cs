using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class GenreController : Controller
    {
        // GET: GenreController
        public ActionResult Index()
        {
            ViewBag.Title = "Genres";
            return View(GenreManager.Load());
        }

        // GET: GenreController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Genres";
            return View(GenreManager.LoadById(id));
        }

        // GET: GenreController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add a Genre";
            return View();
        }

        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            try
            {
                GenreManager.Insert(genre);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Add a Genre";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: GenreController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit a Genre";
            return View(GenreManager.LoadById(id));
        }

        // POST: GenreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genre genre)
        {
            try
            {
                GenreManager.Update(genre);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit a Genre";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: GenreController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Genre";
            return View(GenreManager.LoadById(id));
        }

        // POST: GenreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                GenreManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete a Genre";
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
