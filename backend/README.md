# Developer Evaluation Project - Sales Management API

## About the Project
This project is a **Developer Evaluation Project - Sales Management API** designed to handle CRUD operations for managing sales records. It follows **Domain-Driven Design (DDD)** principles and implements business logic for calculating discounts based on product quantities. The API is built using **.NET 8**, leveraging technologies like **Mediator**, **AutoMapper**, **Entity Framework (EF)**, **RabbitMQ**, and **PostgreSQL**. It is containerized using **Docker Compose** for easy deployment, scalability, and isolation of services.

This API also includes **MessageBroker integration with RabbitMQ** for event publishing, supporting key events like:
- `SaleCreated`
- `SaleModified`
- `SaleCancelled`
- `ItemCancelled`

The system is designed for further expansion to other business domains such as **Products**, **Customers**, **Branches**, and **Cart** in the future.

## Features

### Sales Record Management
Store and manage sales information, including:
- Sale number
- Date of the sale
- Customer
- Branch where the sale occurred
- Products involved in the sale
- Item details such as:
  - Quantity
  - Unit price
  - Discounts
  - Cancellation status

### Business Logic
Implemented discount rules based on item quantity:
- 10% discount for purchases between 4 and 9 items.
- 20% discount for purchases between 10 and 20 items.
- Purchases above 20 identical items are restricted.
- No discounts are applied for quantities below 4 items.

### Event Publishing with RabbitMQ
Events are published to RabbitMQ when the following actions occur:
- `SaleCreated`
- `SaleModified`
- `SaleCancelled`
- `ItemCancelled`

### RESTful API Design
Clean and well-documented endpoints for client integration, built using **Swagger/OpenAPI** for automatic API documentation.

### Error Handling
Structured error responses for:
- Validation failures
- Not found cases
- General application issues

## Technologies Used
- **.NET 8** for API development
- **MediatR** for managing business logic and promoting decoupling
- **AutoMapper** for object-to-object mapping
- **PostgreSQL** for persistence
- **RabbitMQ** for event-driven architecture (MessageBroker integration)
- **Entity Framework Core (EF)** for data management
- **xUnit** for unit testing
- **Swagger/OpenAPI** for API documentation
- **Docker Compose** for containerized environment

## How to Execute the Project

### Clone the Repository
Clone the project to your local machine:
```bash
git clone https://github.com/wesleyheron/Ambev.DeveloperEvaluation.git
cd sales-management-api
```

## Setup the Database

1. Ensure **PostgreSQL** is installed and running (either locally or via Docker).
2. Create a new database (e.g., `DeveloperEvaluation` if using a locally installed instance).
3. Update the connection string in the `appsettings.json` file to match your database configuration.

## Build the Docker Containers

1. Ensure **Docker** and **Docker Compose** are installed on your machine.
2. Use the following command to build the Docker containers:

   ```bash
   docker-compose build
   ```
## Run Migrations

Run the necessary migrations to create the database tables:

```bash
dotnet ef database update
```
## Run the Application

Start the API and database containers:

```bash
docker-compose up --build
```

## Accessing the API

Once the containers are up, you can access the API via:
[http://localhost:7181](http://localhost:7181)

## Testing the API

Use Postman or another API testing tool to test the following endpoints:

- **POST /sales** – Create a new sale
- **GET /sales/{id}** – Retrieve sale details
- **PUT /sales/{id}** – Modify an existing sale
- **DELETE /sales/{id}** – Cancel a sale
- **PATCH /api/Sales/cancel/{id}** – Cancel a sale
- **PATCH /api/Sales/cancel-item/{saleId}/{itemId}** – Cancel a specific item from a sale
- **GET /sales** – List all sales

## Running Unit Tests

Run unit tests with xUnit:

```bash
dotnet test
```

## Checking Logs

The application logs messages for actions such as creating, modifying, and cancelling sales. You can check these logs in the Docker container logs or use a local log viewer depending on your configuration.

## Use Cases

This API can be used in various scenarios, including:

- Managing sales records for retail or e-commerce platforms.
- Integrating with front-end applications to display and modify sales data.
- Enforcing business-specific sales rules programmatically.
- Publishing events for sale creation, modification, cancellation, and item cancellation.

## Goals

The main objectives of this project are:

- Demonstrate backend development skills, including API design and implementation.
- Showcase proficiency in business rule implementation, validation, and error handling.
- Adhere to best practices for code quality, scalability, and maintainability.
- Implement MessageBroker integration with RabbitMQ for event-driven architecture.
- Implement Docker Compose for containerized deployment, ensuring ease of setup and scalability.

## Testing the Business Rules

The following logic was implemented and can be tested by creating or modifying sales:

- **Discount Calculation**:
  - Sales with 4-9 items receive a 10% discount.
  - Sales with 10-20 items receive a 20% discount.
  - Sales exceeding 20 identical items are not allowed.
  - No discounts are applied for purchases below 4 items.

## Future Enhancements

- **Extended Domain Development**: Future work will involve developing additional domains such as Products, Customers, Branches, and Cart to create a more comprehensive business application.
- **Additional Unit Testing**: Although unit testing was implemented, more comprehensive tests can be added in the future to ensure broader coverage of different scenarios and edge cases.






