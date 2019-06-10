using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BasketService : Service
{
    public async Task<object> Any(Basket request)
    {
        var basket = SessionBag.GetOrAdd("Basket", new List<BasketViewModel>());
        var products = await Db.SelectAsync<Product>();

        return new BasketResponse
        {
            Data = (
                from b in basket
                join p in products on b.Id equals p.Id
                select p
            ).ToList()
        };
    }
}