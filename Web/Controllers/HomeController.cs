using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : ControllerBase
{
    public async Task<IActionResult> Index()
    {
        TempData["BasketCount"] = Basket.Count;

        var products =
            from p in await Db.SelectAsync<Product>()
            select p.ConvertTo<ProductViewModel>();

        //var products =
        //    (await Db.SelectAsync<Product>())
        //    .Select(p => p.ConvertTo<ProductViewModel>());

        return View(products);
    }
}