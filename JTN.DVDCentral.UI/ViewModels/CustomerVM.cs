using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;

namespace JTN.DVDCentral.UI.ViewModels
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        public List<Customer> Customers { get; set; }
        public int UserId { get; set; }
        public ShoppingCart Cart { get; set; }

        public CustomerVM()
        {
            Customers = new List<Customer>();
        }

    }
}
