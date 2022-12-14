using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            ViewBag.Title = "Users";
            return View(UserManager.Load());
        }

        public ActionResult Seed()
        {
            UserManager.Seed();
            return View();
        }

        public ActionResult Logout()
        {
            SetUser(null);
            return View();
        }

        public ActionResult Login(string returnuri)
        {
            TempData["returnuri"] = returnuri;
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            try
            {
                UserManager.Login(user);
                SetUser(user);
                if (TempData["returnuri"] != null)
                    return Redirect(TempData["returnuri"]?.ToString());
                else
                    return RedirectToAction("AssignToCustomer", "ShoppingCart");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);
            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FullName);
                HttpContext.Session.SetObject("userId", user.Id);
                HttpContext.Session.SetObject("loggedOut", "no");
            }
            else
            {
                HttpContext.Session.SetObject("fullname", string.Empty);
                HttpContext.Session.SetObject("loggedOut", "yes");
            }
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Add a User";
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                UserManager.Insert(user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Add a User";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit a User";
            return View(UserManager.LoadById(id));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                UserManager.Update(user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Title = "Edit a User";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a User";
            return View(UserManager.LoadById(id));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User user)
        {
            try
            {
                UserManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit a User";
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
