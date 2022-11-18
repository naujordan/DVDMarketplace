using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.UI.Models;
using JTN.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class MovieController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public MovieController(IWebHostEnvironment host)
        {
            _host = host;
        }

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

        public ActionResult Details(int id)
        {
            ViewBag.Title = "Movies";
            return View(MovieManager.LoadById(id));
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Add a Movie";
            MovieVM movieVM = new MovieVM();
            movieVM.genres = GenreManager.Load();
            return View(movieVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieVM movieVM)
        {
            try
            {
                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;

                    // Upload the file
                    string path = _host.WebRootPath + "\\images\\";

                    if (!System.IO.File.Exists(path + movieVM.File.FileName))
                    {
                        using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                        {
                            movieVM.File.CopyTo(stream);
                            ViewBag.Message = "File Uploaded";
                        }
                    }

                }
                MovieManager.Insert(movieVM.Movie);
                IEnumerable<int> newGenreIds = new List<int>();
                if (movieVM.genreIds != null)
                    newGenreIds = movieVM.genreIds;

                movieVM.genreIds.ToList().ForEach(a => MovieGenreManager.Insert(movieVM.Movie.Id, a));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Title = "Add a Movie";
                return View(movieVM);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Movie";
            MovieVM movieVM = new MovieVM(id);
            HttpContext.Session.SetObject("genreids", movieVM.genreIds);
            return View(movieVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieVM movieVM)
        {
            try
            {
                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;

                    // Upload the file
                    string path = _host.WebRootPath + "\\images\\";

                    if (!System.IO.File.Exists(path + movieVM.File.FileName))
                    {
                        using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                        {
                            movieVM.File.CopyTo(stream);
                            ViewBag.Message = "File Uploaded";
                        }
                    }

                }
                IEnumerable<int> newGenreIds = new List<int>();
                if (movieVM.genreIds != null)
                    newGenreIds = movieVM.genreIds;
                IEnumerable<int> oldGenreIds = new List<int>();
                oldGenreIds = GetObject();

                IEnumerable<int> deletes = oldGenreIds.Except(newGenreIds);
                IEnumerable<int> adds = newGenreIds.Except(oldGenreIds);

                //db maintenance
                deletes.ToList().ForEach(d => MovieGenreManager.Delete(id, d));
                adds.ToList().ForEach(a => MovieGenreManager.Insert(id, a));

                MovieManager.Update(movieVM.Movie);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Title = "Edit Movie";
                return View(movieVM);
            }
        }

        public ActionResult Delete(int id)
        {
            ViewBag.Title = "Delete a Movie";
            return View(MovieManager.LoadById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Movie movie)
        {
            try
            {
                MovieManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete a Movie";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        private IEnumerable<int> GetObject()
        {
            if (HttpContext.Session.GetObject<IEnumerable<int>>("genreids") != null)
            {
                return HttpContext.Session.GetObject<IEnumerable<int>>("genreids");
            }
            else
                return null;
        }

    }
}
