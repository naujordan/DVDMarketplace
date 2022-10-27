using JTN.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;


namespace JTN.DVDCentral.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(GenreManager.Load().OrderBy(g => g.Description));
        }
    }
}