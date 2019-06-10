using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : ControllerBase
{
    public async Task<IActionResult> Index()
    {
        var products = await UnitOfWork.Products.GetAllAsync();
        var viewModel =
            from p in products
            select p.ConvertTo<ProductViewModel>();

        return View(viewModel);
    }
}