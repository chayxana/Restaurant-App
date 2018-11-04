# Restaurant App

| Microservices | Language      | Build Status| Coverage |
| ------------- |:-------------:| -----------:| --------:|
| Basket API    | Golang        |<a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines"><img alt="build status" src="https://www.dropbox.com/s/iyd2zm0dbuuz04k/basket_api_build_status.svg?raw=1"/></a> |
| Menu API      | .net core     |<a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines"><img alt="build status" src="https://www.dropbox.com/s/k3mwmuo6nexjwwo/menu_api_build_status.svg?raw=1"/></a> | <a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines"><img alt="coverage" src="https://www.dropbox.com/s/cxqz4izs3i7tve3/menu_api_coverage.svg?raw=1"/></a> |
| Identity API | .net core | <a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines"><img alt="build status" src="https://www.dropbox.com/s/5rnmc8yrfynmyiw/identity_api_build_status.svg?raw=1"/></a>|
| Order API | java |<a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines"><img alt="build status" src="https://www.dropbox.com/s/h7mtywm6vb3vrx6/order_api_build_status.svg?raw=1"/></a>|

[![Android Build status](https://build.appcenter.ms/v0.1/apps/ae1793a8-cb35-40cc-a5db-583847244261/branches/develop/badge)](https://appcenter.ms)
[![iOS Build status](https://build.appcenter.ms/v0.1/apps/9a0e12b9-f5cc-4a2c-8d54-f09e48cffd86/branches/develop/badge)](https://appcenter.ms)


<img src="art/1.png" width="210"/> <img src="art/2.png" width="210"/> <img src="art/3.png" width="210"/> <img src="art/4.png" width="210"/> <img src="art/5.png" width="210"/>

The Restaurant App is a sample open source application powered by C# and Xamarin, this sample provides us how to build mobile and web applications with a clean architecture and write testable and clean code.

## Using technologies

* Back-end
  * ASP.NET Core Web API
  * Identity Server4
  * Entity Framework Core
  * PostgreSQL
  * SQL Server
* Xamarin
  * Xamarin.Forms + Material Design
  * Custom Renderers
  * Reactive UI
  * Rx.NET
  * MVVM
  * Dependency Injection (Autofac)
* Testing
  * BDD Unit testing
  * Moq
  * xUnit (backend)
  * NUnit (xamarin)
  * AutoFixture
* DevOps
  * PowerShell
  * CI (AppVeyor)
  * [Azure Deploy](https://restaurantserverapi.azurewebsites.net/)
  * Heroku Deploy - cooming soon
  * Docker Containers - cooming soon
* Front-end
  * [Angualar 4 - Dashboard Admin](https://github.com/Jurabek/Restaurant-App-Dashboard)

**Development:**

* [Jurabek Azizkhujaev](https://github.com/jurabek)

**Future implementations:**

* Ordering foods using AI with neural network
* Creating chatbot for this neural network
