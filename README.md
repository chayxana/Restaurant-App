# Restaurant App 
[![Gitter](https://badges.gitter.im/Restaurant-App-Community/community.svg)](https://gitter.im/Restaurant-App-Community/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

Restaurant App is containerized polyglot microservices application which contains projects based on .NET Core, Golang, Java, Xamarin, React, Angular and etc. The project demonstrates how to develop small microservices for larger applications using containers, orchestration, service discovery, gateway, and etc.

To getting started, simply fork this repository. Please refer to [CONTRIBUTING.md](CONTRIBUTING.md) for contribution guidelines.

## Motivation

- Developing independently deployable and scalable micro-services
- Developing cross-platform mobile apps using Xamarin.Forms
- Configuring CI/CD pipelines
- Using Docker and k8s
- Writing clean, maintainable and fully testable code
- Using SOLID principles
- Using Design Patterns

## Architecture overview

The architecture proposes a micro-service oriented architecture implementation with multiple autonomous micro-services (each one owning its own data/db) and implementing different approaches within each microservice using REST/HTTP as the communication protocol between the client apps and backend and supports asynchronous communication for data updates propagation across multiple services based on Integration Events and an Event Bus(Apache Kafka).

## List of several individual microservices and infrastructure components

<table>
   <thead>
    <th>№</th>
    <th>Service</th>
    <th>Description</th>
    <th>Build status</th>
    <th>Coverage</th>
    <th>Endpoints</th>
  </thead>
  <tbody>
    <tr>
        <td align="center">1.</td>
        <td>Identity API (.NET Core + IdentityServer4)</td>
        <td>Authentication service powered by OAuth2 and OpenID Connect</td>
        <td>
            <a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/identity_api_build_status.svg">
            </a>
        </td>
        <td>
            <a href="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/coverage/identity_api/index.htm">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/identity_api_coverage.svg">
            </a>
        </td>
        <td>
            <a href="#">dev</a> | <a href="#">prod</a>
        </td>
    </tr>
    <tr>
        <td align="center">2.</td>
        <td>Basket API (Golang + Redis)</td>
        <td>Manages customer basket in order to keep items, caches items into redis</td>
        <td>
            <a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/basket_api_build_status.svg">
            </a>
        </td>
        <td>
            <a href="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/coverage/basket_api/coverage.html">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/basket_api_coverage.svg">
            </a>
        </td>
        <td>
            <a href="#">dev</a> |
            <a href="#">prod</a>
        </td>
    </tr>
    <tr>
        <td align="center">3.</td>
        <td>Menu API (.NET Core, PostgreSQL)</td>
        <td>Manages data for showing on the list</td>
        <td>
            <a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/menu_api_build_status.svg">
            </a>
        </td>
        <td>
            <a href="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/coverage/menu_api/index.htm">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/menu_api_coverage.svg">
            </a>
        </td>
        <td>
            <a href="#">dev</a> |
            <a href="#">prod</a>
        </td>
    </tr>
    <tr>
        <td align="center">4.</td>
        <td>Order API (Java + Spring Boot)</td>
        <td>Manages customer orders and notifies cook about new orders</td>
        <td>
            <a href="https://gitlab.com/Jurabek/Restaurant-App/pipelines">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/order_api_build_status.svg">
            </a>
        </td>
        <td>
            <a href="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/coverage/order_api/index.html">
                <img src="https://s3.eu-central-1.amazonaws.com/jurabek-restaurant-app/badges/order_api_coverage.svg">
            </a>
        </td>
        <td>
            <a href="#">dev</a> |
            <a href="#">prod</a>
        </td>
    </tr>
  </tbody>  
</table>

## Mobile app

| Mobile              | Build status |
|---------------------|--------------|
| Android (AppCenter) |   [![Android Build status](https://build.appcenter.ms/v0.1/apps/ae1793a8-cb35-40cc-a5db-583847244261/branches/develop/badge)](https://appcenter.ms)           |
| iOS (AppCenter)     |    [![iOS Build status](https://build.appcenter.ms/v0.1/apps/9a0e12b9-f5cc-4a2c-8d54-f09e48cffd86/branches/develop/badge)](https://appcenter.ms)          |

Mobile app developed by Xamarin.Forms and supports iOS and Android, here you can find how to develop cross-platform mobile apps using C#.
The example shows how to develop beautiful user interfaces using Xamarin.Forms and how to manage your code with Clean Architecture on the mobile side and get a clean, maintainable, testable code.

<img src="art/2.png" width="210"/> <img src="art/3.png" width="210"/>


### Contributors

Thank you to all the people who have already contributed to our project!
<a href="/graphs/contributors"><img src="https://opencollective.com/restaurant-app/contributors.svg?width=890" /></a>