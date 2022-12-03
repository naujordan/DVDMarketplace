using JTN.DVDCentral.BL;
using JTN.DVDCentral.BL.Models;
using JTN.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JTN.DVDCentral.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCart cart;
        public IActionResult Index()
        {
            cart = GetShoppingCart();
            ViewBag.Title = "Shopping Cart";
            return View(cart);
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
                order.CustomerId = 1;
                order.UserId = 1;
                order.OrderItems.Add(orderItem);
            }
            OrderManager.Insert(order);
            HttpContext.Session.SetObject("cart", null);
            return View();
        }
    }
}
