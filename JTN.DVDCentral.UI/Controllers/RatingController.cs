using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class RatingController : Controller
    {
        // GET: RatingController
        public ActionResult Index()
        {
            ViewBag.Title = "Ratings";
            return View(RatingManager.Load());
        }

        // GET: RatingController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Title = "Ratings";
            return View(RatingManager.LoadById(id));
        }

        // GET: RatingController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add a Rating";
            return View();
        }

        // POST: RatingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rating rating)
        {
            try
            {
                RatingManager.Insert(rating);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Add a Rating";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: RatingController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Update a Rating";
            return View(RatingManager.LoadById(id));
        }

        // POST: RatingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Rating rating)
        {
            try
            {
                RatingManager.Update(rating);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Update a Rating";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: RatingController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Rating";
            return View(RatingManager.LoadById(id));
        }

        // POST: RatingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Rating rating)
        {
            try
            {
                RatingManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete a Rating";
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
