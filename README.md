# Restaurant App 
[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/chayxana/Restaurant-App)

if you have any questions, please reach me out:

[![](https://img.shields.io/badge/twitter-00ACEE?style=for-the-badge&logo=twitter&logoColor=white)](https://twitter.com/jurabek_az)
[![](https://dcbadge.vercel.app/api/server/sy6ZGyPs)](https://discord.gg/mhHvfkR2)
[![Gitter](https://badges.gitter.im/Restaurant-App-Community/community.svg)](https://gitter.im/Restaurant-App-Community/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

Restaurant App is containerized polyglot microservices application that contains projects based on .NET Core, Golang, Java, Xamarin, React, Angular and etc. The project demonstrates how to develop small microservices for larger applications using containers, orchestration, service discovery, gateway, and best practices. You are always welcome to improve code quality and contribute it, if you have any questions or issues don't hesitate to ask in our [gitter](https://gitter.im/Restaurant-App-Community/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge) chat.

To getting started, simply fork this repository. Please refer to [CONTRIBUTING.md](CONTRIBUTING.md) for contribution guidelines.

## Motivation

- Developing independently deployable and scalable micro-services based on best practies using containerization ☸ 🐳 
- Developing cross-platform beautiful mobile apps using Xamarin.Forms
- Developing Single Page applications using React and Angular including best practices
- Configuring fully automated CI/CD pipelines using Github Actions to mono-repo and Azure Pipelines and AppCenter for mobile
- Using modern technologies such as GraphQL, gRPC, Apache Kafka, Serverless, Istio
- Writing clean, maintainable and fully testable code, Unit Testing, Integration Testing and Mocking practices
- Using SOLID Design Principles
- Using Design Patterns and Best practices in different programming languages

## Architecture overview

The architecture proposes a micro-service oriented architecture implementation with multiple autonomous micro-services (each one owning its own data/db and programming language) and using REST/HTTP as the communication protocol between the client apps, and gRPC for the backend communication in order to support data update propagation across multiple services.

## List of micro-services and infrastructure components

| №  | Service                                    | Description                                                                   | Build Status                                                                                               | Endpoints             |
|----|--------------------------------------------|-------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------|-----------------------|
| 1. | Identity API (.NET Core + IdentityServer4) | Identity management service, powered by OAuth2 and OpenID Connect             | ![Build Status](https://github.com/chayxana/Restaurant-App/actions/workflows/identity-api.yml/badge.svg)   | [dev](#) \| [prod](#) |
| 2. | Cart API (Golang + Redis)                  | Manages customer basket in order to keep items on in-memory cache using redis | ![Build Status](https://github.com/chayxana/Restaurant-App/actions/workflows/cart-api.yml/badge.svg)       | [dev](#) \| [prod](#) |
| 3. | Catalog API (Rust + Rocket, PostgreSQL)    | Manages data for showing restaurant menu                                      | ![Build Status](https://github.com/chayxana/Restaurant-App/actions/workflows/catalog-api.yml/badge.svg)    | [dev](#) \| [prod](#) |
| 4. | Order API (Java + Quarkus + Native Build)  | Manages customer orders                                                       | ![Build Status](https://github.com/chayxana/Restaurant-App/actions/workflows/order-api.yml/badge.svg)      | [dev](#) \| [prod](#) |
| 5. | Ceckout API (Node.js + Express)            | Responsible for checkout functionality                                        | ![Build Status](https://github.com/chayxana/Restaurant-App/actions/workflows/checkout-api.yml/badge.svg)   | [dev](#) \| [prod](#) |
| 6. | Payment API (Golang)                       | Fake payment API (Payment service abstracting PSP)                            | ![Build Status](https://github.com/chayxana/Restaurant-App/actions/workflows/payment-api.yml/badge.svg)    | [dev](#) \| [prod](#) |
| 6. | Web App (Typescript + Next.js ❤️ )          | Frontend application                                                          | ![Build Status](https://github.com/chayxana/Restaurant-App/actions/workflows/web-app.yml/badge.svg)        | [dev](#) \| [prod](#) |

## Web App
Web app developed using Next.js with SSR
<img width="1828" alt="image" src="https://github.com/chayxana/Restaurant-App/assets/8397056/590a5fe5-58c6-425f-934c-54456a9257bf">



## Mobile app
[Unfortunately I no longer be able to maintain Xamarin(mobile) part](https://github.com/chayxana/Restaurant-App/issues/81)

| Mobile              | Build status | Release |
|---------------------|--------------|-------------------|
| Android             |[![Build Status](https://dev.azure.com/jurabek/Restaurant%20App/_apis/build/status/chayxana.Restaurant-App?branchName=develop&jobName=Android)](https://dev.azure.com/jurabek/Restaurant%20App/_build/latest?definitionId=11&branchName=develop)| [Download Android]("/") |
| iOS                 |[![Build Status](https://dev.azure.com/jurabek/Restaurant%20App/_apis/build/status/chayxana.Restaurant-App?branchName=develop&jobName=iOS)](https://dev.azure.com/jurabek/Restaurant%20App/_build/latest?definitionId=11&branchName=develop)| [Download iOS]("/") |

Mobile app developed by Xamarin.Forms and supports iOS and Android, here you can find how to develop cross-platform mobile apps using C#.
The example shows how to develop beautiful user interfaces using Xamarin.Forms and how to manage your code with Clean Architecture on the mobile side and get a clean, maintainable, testable code.

<img src="art/2.png" width="210"/> <img src="art/3.png" width="210"/>


### Contributors

Thank you to all the people who have already contributed to our project!
<a href="/graphs/contributors"><img src="https://opencollective.com/restaurant-app/contributors.svg?width=890" /></a>
