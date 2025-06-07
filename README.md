# Fleet Rental System (Backend)

This project is a backend solution for managing motorcycle rentals and delivery drivers. It demonstrates a microservices architecture using .NET, PostgreSQL, RabbitMQ, and MinIO for object storage.

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
