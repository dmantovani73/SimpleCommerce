## Setup
- Create un progetto di tipo ASP.NET Core Web Application

![ASP.NET Core Web Application](https://github.com/dmantovani73/OnlineShop/blob/master/ASP.NET%20Core%20Web%20Application.png)

  - Versione: 2.2
  - Template: Empty
  - Authentication: No Authentication
  - Advanced: Configure for HTTPS

> Struttura progetto, launchSettings.json, Project Properties, environments, eccezioni

## Middleware
* Esempi
  * [Calcolo del tempo impiegato per elaborare una richiesta HTTP](https://gist.github.com/dmantovani73/9a03962a6648c2e8887ce6bb5bb4f225)
  * [Endpoint per avere informazioni di sistema su cui gira la webapp](https://gist.github.com/dmantovani73/4afc12d4d82732d35fd2a723c71ec473)
  * [Esempio di MapWhen](https://gist.github.com/dmantovani73/40de6090d25c96226e30281efe8e8833)

> Riferimenti
> * https://docs.microsoft.com/it-it/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.2

## Static files
* Aggiungere il folder wwwroot e copiarci i file contenuti nello [zip](https://github.com/dmantovani73/OnlineShop/blob/master/wwwroot.zip)

## ServiceStack + Serilog
* Creare il file [AppHost.cs](https://gist.github.com/dmantovani73/243c9ba93985f217eba59f8f79a37696/4d56bf07d4ff22f0e1a2aa7f410014cfb4bdd7cd)
* Modificare [Setup.cs](https://gist.github.com/dmantovani73/70da48797b480ccbc7689e951e271d4d/9c11690a404db37d1eea9f941069cc4628834fb6)
* Creare il file [appsettings.json](https://gist.github.com/dmantovani73/412b78440e2431e8cbeadcd57b05a31f/f5c719e7e5f0323d9c6e8e600b4f180e6d3acfe7)

> Riferimenti
> * [Setup di ServiceStack in webapp .NET Core](https://docs.servicestack.net/netcore)
> * [Setup delle funzionalità di logging](https://docs.servicestack.net/logging)

## MVC
* Modificare [Setup.cs](https://gist.github.com/dmantovani73/70da48797b480ccbc7689e951e271d4d/3c107524f8e7967c449ce481e43886ea05a980a8) per aggiungere MVC alla pipeline
* Creare il folder ViewModels nel quale raccogliere tutte le classi che fungono da ViewModel
  * [ProductViewModel.cs](https://gist.github.com/dmantovani73/f6450bd09ab9502840ba995b753ee94d)
  * [BasketViewModel.cs](https://gist.github.com/dmantovani73/4880bde8f90af06ce9541f0279bbe5d8)
* Creare il folder Controllers dove raccogliere tutte le classi che fungono da Controller
  * [HomeController.cs](https://gist.github.com/dmantovani73/0ff091e8190d56c13db046409c7a9709/bccda6da131cfe05c1b2aecb96927431c30b00f9)
  * [BasketController.cs](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/9467e2561441952a7bb4e41599a5435e7c04a87b)
* Creare il folder Views
  * [\_ViewStart.cshtml](https://gist.github.com/dmantovani73/3568a2bbafad23761a84d1c8502a238a)
  * Creare il subfolder Shared
    * [\_Layout.cshtml](https://gist.github.com/dmantovani73/57631cbe51313ef4135bf90e8022f4cc/adb4333109bda872aed3b40c4d74522f41d80834)
    * [Banner.cshtml](https://gist.github.com/dmantovani73/35027842441b892bb71141089925dde4)
    * [Filters.cshtml](https://gist.github.com/dmantovani73/371df5e62b7469addf1e0c62a3f5bd9b)
    * [Footer.cshtml](https://gist.github.com/dmantovani73/9dd179f696f76ac020eade06bce36519)
    * [Header.cshtml](https://gist.github.com/dmantovani73/9e0751a3f43ca288b1dc216e54cb47f7/7d76f11a2988b2f9e80662e2394f5174470241c9)
  * Creare il subfolder Home
    * [Product.cshtml](https://gist.github.com/dmantovani73/df11a42453e867a5bbc0c8ee00146cea)
    * [Index.cshtml](https://gist.github.com/dmantovani73/6202d6fc6713f63ec81481609e325f86)

> Aprendo una nuova finestra del browser la sessione rimane la stessa e quindi gli elementi nel carrello rimangono; non succede invece se viene aperta una finestra in modalità anonima perchè avvia una sessione differente.

> Riferimenti
> * https://docs.microsoft.com/it-it/aspnet/core/mvc/overview?view=aspnetcore-2.2
> * https://docs.microsoft.com/it-it/aspnet/core/mvc/views/razor?view=aspnetcore-2.2

### Esercizio
Il codice contiene un bug, facendo refresh della pagina il contatore degli elementi nel carrello viene azzerato fino a quando non viene aggiunto un ulteriore prodotto.
* Controllers
  * Creare [ControllerBase.cs](https://gist.github.com/dmantovani73/b95eda2c14d1b11af477ecc8145f22e1)
* Creare il folder Infrastructure
  * [SessionUtils.cs](https://gist.github.com/dmantovani73/db01d7d9c516a497927e81dc71426f35)
* Togliere la classe SessionUtils da [BasketController.cs](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/3a59b0e82de214591ec7665ed343795948aa7005) e farlo ereditare da BaseController
* Modificare [HomeController.cs](https://gist.github.com/dmantovani73/0ff091e8190d56c13db046409c7a9709/1e3ca539960d8808c61f2726801e6cc0bf1d1253)

## Caching
> Salvare il carrello in session bag (che di default si appoggia alla memoria) presenta diversi problemi:
> * di default la cache è in memoria e quindi volatile (riavvio del processo per qualche ragione, ad es. recycle; provare a stoppare IISExpress e riavviarlo)
> * richiede [sticky session](https://stackoverflow.com/questions/10494431/sticky-and-non-sticky-sessions)
> * non è scalabile.

La soluzione è utilizzare un "cache server", meglio ancora se distribuito con possibilità di persistenza (es. basato su Redis).
Tramite [dependency injection](https://docs.servicestack.net/ioc) è possibile configurare il cache provider senza avere impatto sul resto del codice.

* Installare Redis e avviarlo
* Installare il package [ServiceStack.Redis](https://www.nuget.org/packages/ServiceStack.Redis/)
* Aggiungere ad [appsettings.json](https://gist.github.com/dmantovani73/412b78440e2431e8cbeadcd57b05a31f/86dd9427f22512e8fc6f09eba84f3e577f651af6) la stringa di connessione verso Redis
* Modificare [AppHost.cs](https://gist.github.com/dmantovani73/243c9ba93985f217eba59f8f79a37696/7fb0527dbabfc08f976ee54b9862badef4fdb4a9) dicendo di utilizzare Redis come cache server.
* Modificare [BasketController.cs](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/46daad70630bea404e095e6cb3e40b17f463b4b8)

> Riferimenti
> * [Caching](https://docs.servicestack.net/caching)
> * [Dependency Injection](https://docs.servicestack.net/ioc)
> * [Redis](https://redis.io/)
> * [Redis for Windows](https://github.com/ServiceStack/redis-windows)

## Accesso ai dati
* Installare il package [ServiceStack.OrmLite.Sqlite](https://www.nuget.org/packages/ServiceStack.OrmLite.Sqlite/)
* Aggiungere ad [appsettings.json](https://gist.github.com/dmantovani73/412b78440e2431e8cbeadcd57b05a31f/5aa7925b9f762e7e1c94216ec5232d25f7ff7f7b) la ConnectionString 
* Creare il folder Models
  ** [Product.cs](https://gist.github.com/dmantovani73/435e9ad32a3243168d740377d6cdfd4e/bd42b3f3c13db523e7d5b24b9a803825cf8d96af)
* Modificare [AppHost.cs](https://gist.github.com/dmantovani73/243c9ba93985f217eba59f8f79a37696/f9525e768efe7e9f68f7ad7bee796a8d83cbb208) per aggiungere il setup del db
* Modificare [HomeController.cs](https://gist.github.com/dmantovani73/0ff091e8190d56c13db046409c7a9709/74e99622d87a3478c7b12b2e23d97b7312416778) per leggere il catalogo prodotti da db.

> Riferimenti
> * [Auto-Mapping](https://docs.servicestack.net/auto-mapping)

## Servizi REST e chiamate AJAX
* Creare il folder ServiceModel
  * [Basket.cs](https://gist.github.com/dmantovani73/f04ac3dc04f34268f5e51f974bd4d3dc/9ffc816db7ab4c706da59d45c82d23e8fac9fea4)
* Creare il folder ServiceInterface
  * [BasketService.cs](https://gist.github.com/dmantovani73/7e71f503cc330f2fdee1bd5daef13d11/a6a348ca87dd3c62bddc7944de8a1f24594407d5)
* Installare [ServiceStack.Api.Swagger](https://www.nuget.org/packages/ServiceStack.Api.Swagger/)
* Modificare [AppHost.cs](https://gist.github.com/dmantovani73/243c9ba93985f217eba59f8f79a37696/3d55fc9222bd60c07d7969d7e18d377ce8a57dd4) per fare in modo che le route di ServiceStack non vadano in conflitto con quelle MVC (es. /basket).
* Rimuovere il counter degli elementi nel basket da [HomeController.cs](https://gist.github.com/dmantovani73/0ff091e8190d56c13db046409c7a9709/e50f85fda0aeacf0953577966abe7a92c69a8741) e [BasketController](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/341f5d3a5c5f7e9afce6f307386020f7878c2993)
* Modificare [Views/Shared/Header.cshtml](https://gist.github.com/dmantovani73/9e0751a3f43ca288b1dc216e54cb47f7/38bc179b7f1179b54f70ebcce4774e6cd7a6ec01) rimuovendo il conteggio degli elementi nel basket
* Modificare [Views/Shared/\_Layout.cshtml](https://gist.github.com/dmantovani73/57631cbe51313ef4135bf90e8022f4cc/4cf2f2e90b5b7331a066a6cefbc06a4047ad4c25) leggendo il numero di elementi nel basket con una chiamata AJAX

> Riferimenti
> * [Creating a WebService from scratch](https://docs.servicestack.net/create-webservice-from-scratch)
> * [Serialization and Deserialization](https://docs.servicestack.net/serialization-deserialization)
> * [Swagger](https://docs.servicestack.net/swagger-api)
> * [jQuery AJAX](http://api.jquery.com/jquery.ajax/)
