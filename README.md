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
* Installare il pacchetto [ServiceStack.Mvc](https://www.nuget.org/packages/ServiceStack.Mvc/)
* Modificare [Setup.cs](https://gist.github.com/dmantovani73/70da48797b480ccbc7689e951e271d4d/3c107524f8e7967c449ce481e43886ea05a980a8) per aggiungere MVC alla pipeline
* Creare il folder ViewModels nel quale raccogliere tutte le classi che fungono da ViewModel
  * [ProductViewModel.cs](https://gist.github.com/dmantovani73/f6450bd09ab9502840ba995b753ee94d)
  * [BasketViewModel.cs](https://gist.github.com/dmantovani73/4880bde8f90af06ce9541f0279bbe5d8/67f26c288e6edc2ccb552e97f139835ec2f7219c)
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
  * Creare [ControllerBase.cs](https://gist.github.com/dmantovani73/b95eda2c14d1b11af477ecc8145f22e1/015ebfd65a702bb0ae945fb1ce127ae776fe51a6)
* Creare il folder Infrastructure
  * [SessionUtils.cs](https://gist.github.com/dmantovani73/db01d7d9c516a497927e81dc71426f35)
* Togliere la classe SessionUtils da [BasketController.cs](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/3a59b0e82de214591ec7665ed343795948aa7005) e farlo ereditare da ControllerBase.
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
* Modificare [BasketViewModel.cs](https://gist.github.com/dmantovani73/4880bde8f90af06ce9541f0279bbe5d8/7c9baf09a63979c6cbd1386bd7fcde6f22488989) aggiungendo la quantità.
* Modificare [BasketController.cs](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/ff42b2ca6eb167ba11ff79efd738812c42a8bda9) per gestire la quantità.
* Creare il folder ServiceModel
  * [Basket.cs](https://gist.github.com/dmantovani73/f04ac3dc04f34268f5e51f974bd4d3dc/9ffc816db7ab4c706da59d45c82d23e8fac9fea4)
* Creare il folder ServiceInterface
  * [BasketService.cs](https://gist.github.com/dmantovani73/7e71f503cc330f2fdee1bd5daef13d11/a6a348ca87dd3c62bddc7944de8a1f24594407d5)
* Installare [ServiceStack.Api.Swagger](https://www.nuget.org/packages/ServiceStack.Api.Swagger/)
* Modificare [AppHost.cs](https://gist.github.com/dmantovani73/243c9ba93985f217eba59f8f79a37696/3d55fc9222bd60c07d7969d7e18d377ce8a57dd4) per fare in modo che le route di ServiceStack non vadano in conflitto con quelle MVC (es. /basket).
* Rimuovere il counter degli elementi nel basket da [HomeController.cs](https://gist.github.com/dmantovani73/0ff091e8190d56c13db046409c7a9709/e50f85fda0aeacf0953577966abe7a92c69a8741) e [BasketController](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/341f5d3a5c5f7e9afce6f307386020f7878c2993)
* Modificare [Views/Shared/Header.cshtml](https://gist.github.com/dmantovani73/9e0751a3f43ca288b1dc216e54cb47f7/38bc179b7f1179b54f70ebcce4774e6cd7a6ec01) rimuovendo il conteggio degli elementi nel basket
* Modificare [Views/Shared/\_Layout.cshtml](https://gist.github.com/dmantovani73/57631cbe51313ef4135bf90e8022f4cc/2e5b13261e90d159620a101ae12f7135b6a1da86) leggendo il numero di elementi nel basket con una chiamata AJAX

### Esercizio
* Implementare la view che mostra il contenuto del carrello in termini di nome prodotto, quantità, prezzo.

> Riferimenti
> * [Creating a WebService from scratch](https://docs.servicestack.net/create-webservice-from-scratch)
> * [Serialization and Deserialization](https://docs.servicestack.net/serialization-deserialization)
> * [Swagger](https://docs.servicestack.net/swagger-api)
> * [jQuery AJAX](http://api.jquery.com/jquery.ajax/)

## Autenticazione a autorizzazione
* Installare il pacchetto [ServiceStack.Server](https://www.nuget.org/packages/ServiceStack.Server/). Contiene quello che serve per creare un Auth Repository (utenti, ruoli, permessi, ...) su db (via OrmLite).
* Modificare [AppHost.cs](https://gist.github.com/dmantovani73/243c9ba93985f217eba59f8f79a37696/4fc8234e7e7afae6741fead99a4c965218e29a3c) per configurare l'autenticazione (provider, repository credenziali, ...).
* Creare la classe helper [Infrastructure/UrlHelpers.cs](https://gist.github.com/dmantovani73/25708c23f91c4e13c43e45dadf1ba48e/e87704a02a02f76bd8109e673d176ff6a7f83644). E' un esempio di come estendere MVC con un extension method. Utile per recuperare la URL di una action evitando di utilizzare stringhe (che possono cambiare).
* Modificare la classe [ControllerBase.cs](https://gist.github.com/dmantovani73/b95eda2c14d1b11af477ecc8145f22e1/4f1457f03c253d15add78ffb2503253dc71eff7f) per indicare a ServiceStack.Mvc a quali URL fare redirect in caso di accesso negato / non consentito.
* Aggiungere ora [AccountController.cs](https://gist.github.com/dmantovani73/63c538c719aff2d639f685907e382dee/cee4d99d31c15e4c9464c8a1ab3dff20149113f2) contenente le action di login, register (registrazione esplicita), logout.
* Le pagine di login e registrazione non devono avere banner e filtri, per questa ragione facciamo un minimo di refactoring del layout in modo da mettere a fattor comune quello che si può
  * Creare [Views/Shared/_BasicLayout.cshtml](https://gist.github.com/dmantovani73/cb3a8e643a34fba9b5797ce55e3e2f8f/f1ee514203a02c1e119bdc2c7695776409bd8a62)
  * Modificare [Views/Shared/_Layout.cshtml](https://gist.github.com/dmantovani73/57631cbe51313ef4135bf90e8022f4cc/7d5c8aabfb6d363fa74fda1298743293093dbcac) in modo da "ereditare" da _BasicLayout.cshtml
* Creare la view di login [/Views/Account/Login.cshtml](https://gist.github.com/dmantovani73/d3fe1de39e420bdae6efd61002b12b38/062a337e966c7673725938f997975d474df0bfba)
* Creare la view di registrazione [/Views/Account/Register.cshtml](https://gist.github.com/dmantovani73/b2a5ca071d9c384636e6c46d9fa42c48/971b1758b1a29200d4a36c5619729ccb5f387510)
* Modificare [BasketController.cs](https://gist.github.com/dmantovani73/956c0dc70d5fe0535d060523e1514909/a13da2d8d78f23aeab3ab5a9704e6541414ab77e) aggiungendo la action Checkout accessibile solo agli utenti autenticati.
* Aggiungere la view [Views/Basket/Checkout.cshtml](https://gist.github.com/dmantovani73/08c5114917fae3b629c236160b5a0d47/231092127b345181dc2a35f9c60a0b8e5aad4488).

> Verificare login, register, logout. 
> Verificare che /basket/checkout sia accessibile solo se autenticati.

Ora facciamo in modo che nell'header compaia _Login_ se l'utente non è autenticato, _Logout_ se invece è loggato. Per farlo creare prima di tutto un HTML helper che permette in un file .cshtml di verificare se l'utente è autenticato o meno.
* Creare il file [Infrastructure/RazorPageHelpers.cs](https://gist.github.com/dmantovani73/6a6d1d50cb2921218c47e6850412768c/320d20c206a452622a169c4ee3cafb35709a5550)
* Modificare quindi [Header.cshtml](https://gist.github.com/dmantovani73/9e0751a3f43ca288b1dc216e54cb47f7/8c1c6b336c9f7189d36223c3928db8db5f5551bc) in modo che il link Login/Logout sia modificato dinamicamente.

> Dare un'occhiata agli attributi [RequiredPermission, RequiredRole](https://docs.servicestack.net/authentication-and-authorization#requiredrole-and-requiredpermission-attributes) per poter definire autorizzazioni.

> Riferimenti
> * [ServiceStack Authentication and Authorization](https://docs.servicestack.net/authentication-and-authorization)

## OAuth
Il procedimento è illustrato con Facebook ma è analogo con tutti gli altri OAuth provider.
* Accedere alla pagina developers.facebook.com/apps e creare una tantum un'applicazione.
* Sempre da https://developers.facebook.com, scegliere l'applicazione appena create e quindi sulla sinistra _Impostazioni > Di Base_
* In appsettings.json inserire le seguenti chiavi:

"oauth.RedirectUrl": "...",
"oauth.CallbackUrl": "...",
"oauth.facebook.Permissions": [ "email" ],
"oauth.facebook.AppId": "...",
"oauth.facebook.AppSecret": "..."

  * oauth.RedirectUrl: deve essere la URL della propria applicazione (es. https://localhost:44331)
  * oauth.CallbackUrl: deve essere la URL della propria applicazione seguita da /api/auth/{0} (es. https://localhost:44331/api/auth/{0})
  * oauth.facebook.AppId: ID dell'app appena creata recuperabile da https://developers.facebook.com, scegliere l'applicazione appena create e quindi sulla sinistra _Impostazioni > Di Base > ID app_ 
  * oauth.facebook.AppSecret: Chiave segreta dell'app appena creata recuperabile da https://developers.facebook.com, scegliere l'applicazione appena create e quindi sulla sinistra _Impostazioni > Di Base > Chiave segreta_ 
* Modificare [AppHost.cs](https://gist.github.com/dmantovani73/243c9ba93985f217eba59f8f79a37696/47f9ec92baf04bf90af5d454ea615a51f1442c1c) aggiungendo FacebookAuthProvider tra i provider possibili per il login.
* Aggiungere quindi a [Login.cshtml](https://gist.github.com/dmantovani73/d3fe1de39e420bdae6efd61002b12b38/84505d4a41a317c4eb25230aa2df473859c7c961) il link _Sign in with Facebook._

> Riferimenti
> * https://docs.servicestack.net/authentication-and-authorization#oauth-providers
> * https://docs.servicestack.net/authentication-and-authorization#oauth-configuration
> * [OAuth 2](https://www.digitalocean.com/community/tutorials/an-introduction-to-oauth-2)
> * https://developers.facebook.com
