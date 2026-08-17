# Library ASP.NET Core Microservices

A distributed **Library Management System** built with **.NET 10** to demonstrate **Microservices Architecture, Domain-Driven Design (DDD), Event-Driven Architecture, synchronous gRPC communication, and reliable asynchronous messaging with the Outbox/Inbox patterns**.

The project was inspired by the architecture and concepts presented in [Run-ASP.NETCore-Microservices](https://github.com/aspnetrun/run-aspnetcore-microservices), while taking the opportunity to implement the same architectural principles in a different domain and explore more recent .NET capabilities and messaging patterns.

> **Note:** This is an educational project focused on demonstrating architectural patterns and trade-offs rather than providing a production-ready library management platform.

## Architecture Overview

The system is composed of multiple independently deployable services that communicate through a combination of **synchronous gRPC calls** and **asynchronous events**.

```text
                              ┌─────────────────────┐
                              │   YARP API Gateway  │
                              └──────────┬──────────┘
                                         │ HTTP
              ┌──────────────────────────┼──────────────────────────┐
              │                          │                          │
              ▼                          ▼                          ▼
       ┌─────────────┐           ┌─────────────┐           ┌─────────────┐
       │   Catalog   │           │    Loan     │           │ Membership  │
       │   Service   │           │   Service   │           │   Service   │
       │     API     │           │  API + gRPC │           │  API + gRPC │
       └─────────────┘           └──────┬──────┘           └─────────────┘
                                        │
                                        │ gRPC
                                        ▼
                                 ┌─────────────┐
                                 │  Inventory  │
                                 │   Service   │
                                 │  API + gRPC │
                                 └─────────────┘


                 ╔══════════════════════════════════════════╗
                 ║          EVENT-DRIVEN COMMUNICATION     ║
                 ╚══════════════════════════════════════════╝

                              ┌─────────────────┐
                              │     RabbitMQ    │
                              │   + MassTransit │
                              └────────┬────────┘
                                       │
                         ┌─────────────┼─────────────┐
                         │             │             │
                         ▼             ▼             ▼
                  ┌────────────┐ ┌────────────┐ ┌────────────┐
                  │ Membership │ │ Inventory  │ │    Loan    │
                  │  Consumer  │ │  Consumer  │ │  Consumer  │
                  └────────────┘ └────────────┘ └────────────┘
```

## Services

### YarpApiGateway

Acts as the entry point for external HTTP requests and routes them to the appropriate microservice using **YARP (Yet Another Reverse Proxy)**.

### Catalog

Responsible for the library catalog, including:

* Books
* Authors
* Categories

### Inventory

Manages book availability and reservations, keeping track of which books are currently available for borrowing.

### Loan

Responsible for the borrowing and returning of books.

### Membership

Manages library members and their borrowing eligibility.

### BuildingBlocks

Contains shared infrastructure and contracts used across the services, including common abstractions, messaging components, and **Protocol Buffer (`.proto`) definitions** used by the gRPC communication.

The goal is to keep cross-cutting concerns and shared communication contracts centralized without coupling the business logic of individual services.

## Communication Patterns

The system intentionally uses two different communication models.

### Synchronous Communication with gRPC

gRPC is used when the Loan service needs to perform immediate business-rule validation before creating a loan.

The following rules are synchronously validated:

* Whether the member has overdue loans.
* Whether the requested books are currently available.

This allows the Loan service to receive an immediate response before proceeding with the operation.

However, this is also an intentional architectural trade-off in this educational project. Since the validation depends on other services being available at request time, it introduces runtime coupling and reduces the availability benefits normally associated with asynchronous communication.

In a production system, these business rules could potentially be redesigned around **local projections, replicated state, or asynchronous workflows**, depending on the consistency requirements.

## Event-Driven Architecture

After a loan is successfully created, the system uses asynchronous events to propagate state changes across services.

Messaging is implemented with **RabbitMQ** and **MassTransit**.

The main events are:

### `LoanRegistryCreatedEvent`

Published after a loan has been successfully created and its synchronous business rules have been validated.

Consumers react to this event independently:

* **Membership** updates the member's number of active loans.
* **Inventory** reserves the borrowed books and updates their availability.

This allows the services to react independently without the Loan service directly coordinating these state changes.

### `LoanRegistryReturnedEvent`

Published when a loan is returned.

Consumers react to the event by:

* Updating the member's number of active loans.
* Releasing the corresponding inventory reservations.

### `MemberHasOverdueLoanEvent`

A background worker periodically checks for overdue loans.

When overdue loans are detected, this event is published and consumed by the Membership service, which marks the member as having an overdue loan.

Members with overdue loans are then prevented from creating new loans.

### `MemberLoanEligibilityRestoredEvent`

When a loan is returned, the system checks whether the member still has any outstanding overdue loans.

If the member has no remaining overdue loans, this event is published and Membership clears the member's overdue status, making them eligible for new loans again.

## Outbox and Inbox Patterns

One of the main goals of the project is demonstrating reliable event-driven communication using the **Outbox and Inbox patterns** with MassTransit.

### Outbox

The Outbox pattern helps prevent inconsistencies between database state changes and event publishing.

Instead of relying on a database transaction and message broker operation succeeding independently, messages are persisted as part of the local transaction and published asynchronously by the messaging infrastructure.

This provides a more reliable delivery mechanism and helps avoid scenarios where:

1. A database transaction succeeds.
2. The application crashes before publishing the corresponding event.
3. Other services never receive the event.

### Inbox

The Inbox pattern is used to handle incoming messages reliably and prevent the same message from being processed more than once.

This is particularly important in distributed systems because message delivery is generally designed around **at-least-once delivery**, meaning consumers must be prepared to receive duplicate messages.

The Inbox therefore contributes to the system's **idempotent message processing strategy**.

## Background Processing

The system includes a background worker responsible for periodically checking for overdue loans.

The worker runs every **6 hours** and publishes `MemberHasOverdueLoanEvent` for members whose loans have exceeded their due date.

This demonstrates how scheduled/background processing can participate in the same event-driven architecture used by the application services.

## Technology Stack

* **.NET 10**
* **ASP.NET Core**
* **C#**
* **Entity Framework Core**
* **PostgreSQL**
* **RabbitMQ**
* **MassTransit**
* **gRPC**
* **Protocol Buffers**
* **YARP**
* **Docker / Docker Compose**
* **Domain-Driven Design (DDD)**
* **Clean Architecture**
* **Event-Driven Architecture**
* **Outbox / Inbox Patterns**

## Project Structure

```text
src/
├── ApiGateways/
│   └── YarpApiGateway/
│
├── BuildingBlocks/
│   └── ...
│
└── Services/
    ├── Catalog/
    ├── Inventory/
    ├── Loan/
    └── Membership/
```

Each service is designed to own its own business logic and data, minimizing direct coupling between bounded contexts.

## Running the Project

The entire environment can be started using Docker Compose:

```bash
docker compose up -d
```

Once the containers are running, the API Gateway can be used as the entry point for interacting with the system.

## API Collection

A Postman collection is included in the repository to make it easier to explore and test the available API endpoints.

The collection contains the routes used to interact with the Library Microservices through the API Gateway.

You can find it in the `source` directory:

**[Library Microservices Routes.postman_collection.json](https://github.com/ElianMariano/library-aspnetcore-microservices/blob/main/src/Library%20Microservices%20Routes.postman_collection.json)**

After starting the application with Docker Compose, import the collection into **Postman** and use it to send requests to the available endpoints.

## Architectural Goals

This project was built primarily to explore and demonstrate:

* Microservice boundaries and service ownership
* Domain-Driven Design
* Event-Driven Architecture
* Synchronous vs. asynchronous communication
* gRPC-based service-to-service communication
* RabbitMQ messaging
* MassTransit
* Reliable event publishing with the Outbox pattern
* Duplicate message handling with the Inbox pattern
* Background workers
* Distributed consistency and eventual consistency
* Dockerized microservice environments
* Modern .NET 10 development practices

## Disclaimer

This project is intentionally designed as a **learning and architectural demonstration**.

Some decisions, particularly the synchronous gRPC validation performed before creating a loan, introduce coupling that would require further consideration in a production-grade distributed system.

The goal is to demonstrate the implementation of these patterns while making their **trade-offs and limitations explicit**, rather than claiming that a single architecture is universally optimal.
