using Microsoft.AspNetCore.Mvc;

public static class UrlHelpers
{
    /// <summary>
    /// Utilizzo: 
    /// Url.Action<HomeController>()        -> /home/index
    /// Url.Action<BasketController>("Add") -> /basket/add
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static string Action<T>(this IUrlHelper url, string action = "Index")
        where T : Controller
    {
        var controllerName = typeof(T).Name;
        var controller = controllerName.Substring(0, controllerName.Length - 10);

        return url.Action(action, controller);
    }
}