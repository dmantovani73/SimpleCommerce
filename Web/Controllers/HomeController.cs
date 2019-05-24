using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;

public class HomeController : Controller
{
    static readonly List<ProductViewModel> products = new List<ProductViewModel>
    {
        new ProductViewModel
        {
            Id = 1,
            Name = ".NET Bot Black Sweatshirt",
            Price = 19.5M,
            PictureUrl = "/images/products/1.png",
        },
        new ProductViewModel
        {
            Id = 2,
            Name = ".NET Black & White Mug",
            Price = 8.5M,
            PictureUrl = "/images/products/2.png",
        },
        new ProductViewModel
        {
            Id = 3,
            Name = "Corso universitario scontato al 50%",
            Price = 100M,
            PictureUrl = "/images/products/3.png",
        },
    };

    public IActionResult Index()
    {
        var container = HostContext.Container;
        var dbFactory = container.TryResolve<IDbConnectionFactory>();

        using (var db = dbFactory.Open())
        {

        }

        return View(products);
    }
}