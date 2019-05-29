using ServiceStack.Mvc;
using System.Collections.Generic;

public abstract class ControllerBase : ServiceStackController
{
    const string BasketKey = "Basket";

    protected List<BasketViewModel> Basket
    {
        get => SessionBag.GetOrAdd(BasketKey, new List<BasketViewModel>());
        set => SessionBag[BasketKey] = value;
    }
}