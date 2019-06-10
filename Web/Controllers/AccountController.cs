using Microsoft.AspNetCore.Mvc;
using ServiceStack;

public class AccountController : ControllerBase
{
    public IActionResult Login() => View();

    public IActionResult Register() => View();

    public IActionResult Logout()
    {
        // Esecuzione server-side (in-memory, no chiamata HTTP) del servizio ServiceStack di Logout.
        Gateway.Send(new Authenticate { provider = "logout" });

        // Redirect a /home/index.
        return Redirect(Url.Action<HomeController>());
    }
}