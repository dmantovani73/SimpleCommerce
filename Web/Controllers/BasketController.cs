using Microsoft.AspNetCore.Mvc;
using ServiceStack.Caching;
using ServiceStack.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BasketController : ControllerBase
{
    //List<BasketViewModel> Basket => SessionBag.GetOrAdd("Basket", new List<BasketViewModel>());

    //[HttpPost]
    //public IActionResult Add(int id, decimal price)
    //{
    //    var basket = Basket;
    //    basket.Add(new BasketViewModel { Id = id, Price = price });
    //    Basket = basket;

    //    TempData["BasketCount"] = basket.Count;

    //    return RedirectToAction("Index", "Home");
    //}

    [HttpGet]
    public IActionResult Add(int id, decimal price)
    {
        var basket = Basket;
        basket.Add(new BasketViewModel { Id = id, Price = price });
        Basket = basket;

        return Json(basket);
    }
}