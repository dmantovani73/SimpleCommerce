using ServiceStack.Mvc;
using System.Collections.Generic;

public abstract class ControllerBase<TUnitOfWork> : ServiceStackController
    where TUnitOfWork : class, IUnitOfWork
{
    const string BasketKey = "Basket";

    TUnitOfWork unitOfWork;

    public TUnitOfWork UnitOfWork => unitOfWork ?? (unitOfWork = TryResolve<TUnitOfWork>());

    /// <summary>
    /// URL su cui il servizio di autenticazione di ServiceStack fa redirect in caso di accesso non consentito (es. un controllo protetto e utente non autenticato).
    /// </summary>
    public override string UnauthorizedRedirectUrl => Url.Action<AccountController>("Login");

    /// <summary>
    /// URL su cui il servizio di autenticazione di ServiceStack fa redirect in caso di accesso da parte di un utente autenticato ma non avente permesso/ruolo richiesto.
    /// </summary>
    public override string ForbiddenRedirectUrl => Url.Action<AccountController>("Login");

    protected List<BasketViewModel> Basket
    {
        get => SessionBag.GetOrAdd(BasketKey, new List<BasketViewModel>());
        set => SessionBag[BasketKey] = value;
    }

    protected override void Dispose(bool disposing)
    {
        unitOfWork?.Dispose();

        base.Dispose(disposing);
    }
}

public abstract class ControllerBase : ControllerBase<UnitOfWork>
{ }