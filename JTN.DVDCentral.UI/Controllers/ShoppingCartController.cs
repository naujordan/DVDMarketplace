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
            OrderItem orderItem = new OrderItem();
            foreach (var item in cart.Items)
            {
                orderItem.MovieId = item.Id;
                orderItem.Quantity = 1;
                orderItem.Cost = item.Cost;
                orderItem.ImagePath = item.ImagePath;
                orderItem.Description = item.Description;
                orderItem.MovieTitle = item.Title;
                order.OrderItems.Add(orderItem);
                order.CustomerId = 1;
                order.UserId = 1;
                order.SubTotal = cart.SubTotal;
                order.Tax = cart.Tax;
                order.Total = cart.Total;
            }
            OrderManager.Insert(order);
            HttpContext.Session.SetObject("cart", null);
            return View();
        }
    }
}
