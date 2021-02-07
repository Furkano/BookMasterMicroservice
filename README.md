# BookMaster  Microservice With .Net

You can see on the below all **e-commerce microservice architecture** picture.

![Customer Churn Prediction (1)](https://user-images.githubusercontent.com/24279280/107155639-8c965d80-698a-11eb-9c44-fb2602c336b4.png)

This is a **E-Commerce Microservice** with **.Net Core** and **Clean Architecture**. There are five modules **Category**, **Product**, **Basket**, **Order** with **NoSQL(MongoDB,Redis,ElasticSearh)** and **Relational databases (Sql Server)** with communicating over the **MassTransit** using **RabbitMQ** Event Driven Communication and using **Ocelot API Gateway**.

# Whats Including In This Repository

When you up this repository, you should wait rabbitmq and other databases are running and available connect. First of all input your category than product. Because Product Query Api send to Http request Category Api.

**Product Microservice**
* **Clean Architecture** implementation with **CQRS** Design Pattern
* .Net Core 5.0 Web App
* Dockerfile implementation
* N-Layer implementation
* Solid Principles and Repository Pattern
* Product Command Api using **EF Core Code First and SQL Server**
* Product Command Api **Publish RabbitMQ** messages
* Product Query Api using **ElasticSearch**
* Product Query Api **Consume RabbitMQ** messages
* Implementation of **MediatR**, **MassTransit**, **Mapster**

**Order Microservice**
* **Clean Architecture** implementation 
* .Net Core 5.0 Web App
* **Dockerfile** implementation
* **N-Layer** implementation
* Solid Principles, Repository Pattern, **Cache Aside** Pattern
* Presentation using **EF Core Code First**, **SQL Server** and **Redis**
* Consume the Basket CheckOutEvent

**Basket Microservice**
* **Clean Architecture** implementation 
* .Net Core 5.0 Web App
* **Dockerfile** implementation
* **N-Layer** implementation
* Solid Principles, Repository Pattern
* Presentation using **EF Core Code First**, **SQL Server** 
* Produce the Basket CheckOutEvent

**Category Microservice**
* .Net Core 5.0 Web App
* **Dockerfile** implementation
* Solid Principles, Repository Pattern
* Presentation using **MongoDB**

# Run This App

You need to
* Visual Studio 2019
* .Net Core 5.0 or later
* Docker Desktop

## Installing

* Clone the repository
* At the root directory which include docker-compose.yaml files, run below command:
> **docker-compose up**

### When application is ready to be launch

* **ProductCommandApi** : http://localhost:8000/swagger/index.html
* **ProductQuerydApi** : http://localhost:8010/swagger/index.html
* **OrderApi** : http://localhost:8040/swagger/index.html
* **BasketApi** : http://localhost:8030/swagger/index.html
* **CategoryApi** : http://localhost:8020/swagger/index.html
* **OcelotApi** : http://localhost:8060//Order/{username}
