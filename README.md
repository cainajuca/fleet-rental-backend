# Fleet Rental System (Backend)

This project is a backend solution for managing motorcycle rentals and delivery drivers. It demonstrates a microservices architecture using .NET, PostgreSQL, RabbitMQ, and MinIO for object storage.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [How to Run](#how-to-run)
- [Documentation](#documentation)
  - [ER Diagram](#er-diagram)
  - [System Design](#system-design)
  - [API Reference (Swagger)](#api-reference-swagger)
- [Use Case Example](#use-case-example)

## Features

- Motorcycle and driver management
- Rental plans with dynamic pricing and penalties
- File upload and external storage for driver license images
- Event-driven communication via RabbitMQ
- Background service (worker) to consume events asynchronously

## Technologies

- .NET 8 (C#)
- PostgreSQL
- RabbitMQ
- MinIO (S3 compatible)
- Docker & Docker Compose

## How to Run

```bash
docker-compose up --build
```

## Documentation

### ER Diagram
![ER Diagram](docs/er-diagram.png)

### System Design
![System Design](docs/architecture.png)

### API Reference (Swagger)

The API is fully documented using Swagger.
You can explore and test all endpoints directly at:
```bash
http://localhost:5000/swagger
```

> Make sure the API is running before accessing.

![Swagger UI](docs/swagger-ui.png)

## Use Case Example

### Creating a new vehicle

This example demonstrates how to create a new vehicle in the system and how the notification mechanism works ONLY for vehicles manufactured in 2024.

1. Send a **POST** request to `/vehicle` to create a new vehicle:

**Request**
```json
POST /vehicle
Content-Type: application/json

{
  "identifier": "Motorcycle123",
  "model": "CB 500",
  "year": 2024,
  "licensePlate": "ABC-1234"
}
```
**Response**
```json
{
  "isSuccess": true
}
```

2. When a vehicle is created, the endpoint publishes a message to RabbitMQ.  
3. The worker then consumes these messages and generates a `NotificationMessage` only for vehicles from the year 2024.

4. You can then retrieve the generated notification by calling GET /notificationmessage:

**Request**
```http
GET /notificationmessage
```
**Response**
```json
[
  {
  	"id": "52e39fdd-da6f-4e63-98c6-9f6446e46ce7",
    "message": "{\"Id\":\"6cd35c7b-233a-45e9-8220-47fac31d6bea\",\"Identifier\":\"Motorcycle123\",\"Model\":\"CB 500\",\"Year\":2024,\"LicensePlate\":\"ABC-1234\"}",
    "receivedAt": "2025-06-13T01:41:14.222974Z"
  }
]
```

> :warning: **Note**: Only vehicles from the year 2024 trigger this messaging mechanism. Vehicles from other years will be created normally but no notification message will be published.
