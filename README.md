# Restaurant App

| Microservices | Language      | Build Status|
| ------------- |:-------------:| -----:|
| Basket API    | Golang        | [![pipeline status](https://gitlab.com/Jurabek/Restaurant-App/badges/feature/backend_microservices_architicture/pipeline.svg)](https://gitlab.com/Jurabek/Restaurant-App/commits/feature/backend_microservices_architicture) |
| Menu API      | .net core     |   [![pipeline status](https://gitlab.com/Jurabek/Restaurant-App/badges/feature/backend_microservices_architicture/pipeline.svg)](https://gitlab.com/Jurabek/Restaurant-App/commits/feature/backend_microservices_architicture) |
| Identity API | .net core      |    [![pipeline status](https://gitlab.com/Jurabek/Restaurant-App/badges/feature/backend_microservices_architicture/pipeline.svg)](https://gitlab.com/Jurabek/Restaurant-App/commits/feature/backend_microservices_architicture) |
| Order API | java |    [![pipeline status](https://gitlab.com/Jurabek/Restaurant-App/badges/feature/backend_microservices_architicture/pipeline.svg)](https://gitlab.com/Jurabek/Restaurant-App/commits/feature/backend_microservices_architicture) |
|iOS App | C#/Xamarin      | [![iOS Build status](https://build.appcenter.ms/v0.1/apps/9a0e12b9-f5cc-4a2c-8d54-f09e48cffd86/branches/develop/badge)](https://appcenter.ms) |
| Android App| C#/Xamarin      | [![Android Build status](https://build.appcenter.ms/v0.1/apps/ae1793a8-cb35-40cc-a5db-583847244261/branches/develop/badge)](https://appcenter.ms) |

[![Xamarin client build status](https://ci.appveyor.com/api/projects/status/p29atu2ty3ih7thm/branch/develop?svg=true&pendingText=client-core-unit-tests%20pending&passingText=client-core-unit-tests&failingText=client-core-unit-tests)](https://ci.appveyor.com/project/Jurabek/restaurant-manager-vwadp)
[![Backend build status](https://ci.appveyor.com/api/projects/status/4uh90c7u42d8aleo/branch/develop?svg=true&pendingText=backend-unit-tests%20pending&passingText=backend-unit-tests&failingText=backend-unit-tests)](https://ci.appveyor.com/project/Jurabek/restaurant-manager)
[![codecov](https://codecov.io/gh/Jurabek/Restaurant-App/branch/develop/graph/badge.svg)](https://codecov.io/gh/Jurabek/Restaurant-App)



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
