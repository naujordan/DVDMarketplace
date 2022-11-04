using JTN.DVDCentral.BL.Models;

namespace JTN.DVDCentral.UI.Models
{
    public static class Authenticate
    {
        public static bool isAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
