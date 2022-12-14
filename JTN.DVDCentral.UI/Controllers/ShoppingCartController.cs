using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.UI.Models;
using JTN.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;
        public IActionResult Index()
        {           
            if (Authenticate.isAuthenticated(HttpContext))
            {
                cart = GetShoppingCart();
                ViewBag.Title = "Shopping Cart";
                return View(cart);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnuri = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        public ActionResult AssignToCustomer()
        {
            CustomerVM custVM = new CustomerVM();
            Customer customer = new Customer();
            List<Customer> all = new List<Customer>();
            List<Customer> others = new List<Customer>();

            custVM.Cart = GetShoppingCart();
            custVM.UserId = HttpContext.Session.GetObject<int>("userId");
            custVM.Customers = CustomerManager.LoadByUserId(custVM.UserId);
            if (custVM.Customers.Count() == 0)
                custVM.Customers = CustomerManager.Load();
            custVM.CustomerId = HttpContext.Session.GetObject<int>("customerId");
            HttpContext.Session.SetObject("customerVM", custVM);
            ViewData["ReturnUrl"] = UriHelper.GetDisplayUrl(HttpContext.Request);
            return View(custVM);        
        }

        [HttpPost]
        public ActionResult AssignToCustomer(CustomerVM customerVM)
        {
            try
            {
                cart = HttpContext.Session.GetObject<CustomerVM>("customerVM").Cart;
                Order order = new Order();
                foreach (Movie movie in cart.Items)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.MovieId = movie.Id;
                    orderItem.Quantity = 1;
                    orderItem.Cost = movie.Cost;
                    orderItem.ImagePath = movie.ImagePath;
                    orderItem.Description = movie.Description;
                    orderItem.MovieTitle = movie.Title;
                    order.CustomerId = customerVM.CustomerId;
                    order.UserId = customerVM.UserId;
                    order.OrderItems.Add(orderItem);
                }
                OrderManager.Insert(order);
                HttpContext.Session.SetObject("customerId", customerVM.CustomerId);
                HttpContext.Session.SetObject("cart", null);
                return View("Checkout");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return HttpContext.Session.GetObject<ShoppingCart>("cart");
            }
            else
            {
                return new ShoppingCart();
            }
        }

        public IActionResult AddToCart(int id)
        {
            cart = GetShoppingCart();
            Movie movie = MovieManager.LoadById(id);
            cart.Items.Add(movie);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index), "Movie");
        }

        public IActionResult Remove(int id)
        {
            cart = GetShoppingCart();
            Movie movie = cart.Items.FirstOrDefault(i => i.Id == id);
            cart.Items.Remove(movie);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Checkout()
        {           
            if (Authenticate.isAuthenticated(HttpContext))
            {
                cart = GetShoppingCart();
                Order order = new Order();
                foreach (Movie movie in cart.Items)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.MovieId = movie.Id;
                    orderItem.Quantity = 1;
                    orderItem.Cost = movie.Cost;
                    orderItem.ImagePath = movie.ImagePath;
                    orderItem.Description = movie.Description;
                    orderItem.MovieTitle = movie.Title;
                    order.CustomerId = HttpContext.Session.GetObject<int>("customerId");
                    order.UserId = HttpContext.Session.GetObject<CustomerVM>("customerVM").UserId;
                    order.OrderItems.Add(orderItem);
                }
                OrderManager.Insert(order);
                HttpContext.Session.SetObject("cart", null);
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnuri = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }
    }
}
