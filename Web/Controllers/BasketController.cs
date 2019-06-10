using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.Linq;

public class BasketController : ControllerBase
{
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Add(int id, decimal price)
    {
        var basket = Basket;

        var product = basket.Where(p => p.Id == id).FirstOrDefault();
        if (product == null)
        {
            basket.Add(new BasketViewModel { Id = id, Price = price, Quantity = 1 });
        }
        else
        {
            product.Quantity++;
        }

        Basket = basket;

        return Redirect(Url.Action<HomeController>());
    }

    [Authenticate]
    public IActionResult Checkout() => View();
}