using Microsoft.AspNetCore.Mvc.Razor;
using ServiceStack;
using ServiceStack.Host.NetCore;

public static class RazorPageUtils
{
    public static bool IsAuthenticated<TModel>(this RazorPage<TModel> page)
    {
        var serviceStackProvider = new ServiceStackProvider(new NetCoreRequest(page.Context, page.GetType().Name));

        return serviceStackProvider.IsAuthenticated;
    }

    public static bool IsAuthenticated(this RazorPage<dynamic> page) => IsAuthenticated<dynamic>(page);
}