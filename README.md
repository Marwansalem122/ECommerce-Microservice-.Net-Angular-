# 🛒 eCommerce Microservices Platform

A **comprehensive eCommerce application** built with **ASP.NET Core 10** using a microservices architecture. This project demonstrates real-world patterns for building, containerizing, orchestrating, and deploying scalable microservices using modern DevOps tooling.

---

## 🚀 What I Will Complete

> **Learning Roadmap & Goals**

- ✅ Building a comprehensive eCommerce application with **ASP.NET Core Microservices**
- 🔲 Utilizing **Docker**, **Kubernetes**, **RabbitMQ**, **Redis**, **Azure DevOps**, and **Azure Kubernetes Service (AKS)**
- 🔲 Practical skills in **containerization**, **orchestration**, **fault tolerance**, **caching**, **API management**, and **authentication**
- 🔲 Creating and managing **Azure DevOps pipelines** for continuous integration and deployment (CI/CD)
- 🔲 Implementing **fault tolerance** using **Polly** for retry strategies, circuit breakers, and fallback methods
- 🔲 Managing microservices communication with **RabbitMQ** for reliable messaging and integrating **Redis** for caching data
- 🔲 Hosting and orchestrating microservices on **Azure Kubernetes Service (AKS)** for scalable and efficient deployments

---

## 📐 Architecture Overview

```
┌─────────────────────────────────────────────────────────────────────┐
│                        eCommerce Platform                           │
│                                                                     │
│   ┌───────────────────────┐    ┌───────────────────────────────┐   │
│   │   Products Service    │    │        Users Service          │   │
│   │  (ASP.NET Core 10)    │    │      (ASP.NET Core 10)        │   │
│   │  Port: 8080           │    │      Port: 18090              │   │
│   │  DB: MySQL            │    │      DB: PostgreSQL           │   │
│   └───────────────────────┘    └───────────────────────────────┘   │
│                                                                     │
│   ┌───────────────────────┐    ┌───────────────────────────────┐   │
│   │   Angular (Products)  │    │      Angular (Users)          │   │
│   │   front-end client    │    │      front-end client         │   │
│   └───────────────────────┘    └───────────────────────────────┘   │
│                                                                     │
│                    🐳 Docker / Docker Compose                       │
│                    ☸  Kubernetes / AKS (Planned)                   │
│                    🐰 RabbitMQ Messaging (Planned)                  │
│                    ⚡ Redis Caching (Planned)                       │
└─────────────────────────────────────────────────────────────────────┘
```

---

## 🗂️ Project Structure

```
Microservices/
├── compose.yaml                          # Docker Compose orchestration
├── mysql-init/
│   └── db.sql                            # MySQL schema initialization
├── postgres-init/
│   ├── sql1.sql                          # PostgreSQL schema (part 1)
│   └── sql2.sql                          # PostgreSQL schema (part 2)
│
├── eCommerceSolution.ProductService/     # 📦 Products Microservice
│   ├── DataAccessLayer/                  # EF Core + MySQL data access
│   │   ├── Context/                      # DbContext
│   │   ├── Entities/                     # Domain entities
│   │   ├── Repositories/                 # Repository implementations
│   │   └── RepositoryContracts/          # Repository interfaces
│   ├── BusinessLogicLayer/               # Business logic + AutoMapper + FluentValidation
│   │   ├── DTO/                          # Data Transfer Objects
│   │   ├── Mappers/                      # AutoMapper profiles
│   │   ├── ServiceContract/              # Service interfaces
│   │   ├── Services/                     # Service implementations
│   │   └── Validators/                   # FluentValidation validators
│   └── ProductMicroService.API/          # ASP.NET Core Web API
│       ├── ProductAPIEndPoints/           # Minimal API endpoints
│       ├── Middlewares/                   # Exception handling middleware
│       ├── Dockerfile                     # Container definition
│       └── Program.cs                    # App entry point
│
├── eCommerceSolution.UsersService/       # 👤 Users & Auth Microservice
│   ├── eCommerce.Core/                   # Domain layer
│   │   ├── DTO/                          # Auth DTOs (Login, Register, Refresh)
│   │   ├── Entities/                     # ApplicationUser, RefreshToken
│   │   ├── Mappers/                      # AutoMapper profiles
│   │   ├── RespositoryContract/          # Repository interfaces
│   │   ├── ServicesContract/             # Service + JWT interfaces
│   │   ├── Services/                     # UserService implementation
│   │   └── Validators/                   # Login/Register validators
│   ├── eCommerce.Infrastructure/         # Infrastructure layer
│   │   ├── DbContext/                    # Dapper DbContext
│   │   └── Repositories/                 # User + RefreshToken repositories
│   └── eCommerce.API/                    # ASP.NET Core Web API
│       ├── Controllers/                   # AuthController
│       ├── Services/                      # JwtTokenGenerator
│       ├── Middlewares/                   # Exception handling middleware
│       ├── Dockerfile                     # Container definition
│       └── Program.cs                    # App entry point
│
└── front-end/
    ├── Product-Service/                  # Angular app for Products
    └── User-Service/                     # Angular app for Users
```

---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| **ASP.NET Core 10** | Microservice Web APIs |
| **Entity Framework Core** | ORM for Products (MySQL) |
| **Dapper** | Lightweight ORM for Users (PostgreSQL) |
| **AutoMapper** | Object-to-object mapping |
| **FluentValidation** | Input validation |
| **JWT Bearer Auth** | Authentication & token management |
| **Swashbuckle / Swagger** | API documentation |

### Databases
| Database | Service |
|---|---|
| **MySQL 8+** | Products Microservice |
| **PostgreSQL 15+** | Users Microservice |

### Frontend
| Technology | Purpose |
|---|---|
| **Angular** | Product Service UI |
| **Angular** | User Service UI |

### DevOps & Infrastructure *(Current & Planned)*
| Tool | Status | Purpose |
|---|---|---|
| **Docker** | ✅ Implemented | Containerization |
| **Docker Compose** | ✅ Implemented | Local multi-service orchestration |
| **Kubernetes** | 🔲 Planned | Container orchestration |
| **Azure Kubernetes Service (AKS)** | 🔲 Planned | Cloud-hosted Kubernetes |
| **Azure DevOps** | 🔲 Planned | CI/CD pipelines |
| **RabbitMQ** | 🔲 Planned | Async messaging between services |
| **Redis** | 🔲 Planned | Distributed caching |
| **Polly** | 🔲 Planned | Fault tolerance (retry, circuit breaker) |

---

## 🐳 Running with Docker Compose

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running
- Docker Compose (included with Docker Desktop)

### Quick Start

```bash
# Clone the repository
git clone <your-repo-url>
cd Microservices

# Pull and start all services
docker compose up -d

# View running containers
docker compose ps

# View logs
docker compose logs -f
```

### Services & Ports

| Service | Container Name | Port (Host → Container) |
|---|---|---|
| Products API | `products-microservice` | `8080:8080` |
| Users API | `users-microservice` | `18090:9090` |
| MySQL | `mysql-container` | `3307:3306` |
| PostgreSQL | `postgres-container` | `5432:5432` |

### Stop Services

```bash
docker compose down
```

---

## 📡 API Endpoints

### Products Service — `http://localhost:8080`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/products` | Get all products |
| `GET` | `/api/products/{id}` | Get product by ID |
| `POST` | `/api/products` | Create a new product |
| `PUT` | `/api/products/{id}` | Update a product |
| `DELETE` | `/api/products/{id}` | Delete a product |

### Users Service — `http://localhost:18090`

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/auth/register` | Register a new user |
| `POST` | `/api/auth/login` | Login and get JWT tokens |
| `POST` | `/api/auth/refresh-token` | Refresh JWT access token |

> 📖 Swagger UI available at:
> - Products: `http://localhost:8080/swagger`
> - Users: `http://localhost:18090/swagger`

---

## 🔐 Authentication

The **Users Service** implements **JWT (JSON Web Token)** authentication with:

- **Access Token** — short-lived token for API authorization
- **Refresh Token** — long-lived token stored in the database to issue new access tokens
- **ASP.NET Core Identity** — for password hashing and user management

### Example Login Request

```json
POST /api/auth/login
{
  "email": "user@example.com",
  "password": "YourPassword123!"
}
```

### Example Response

```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "dGhpcyBpcyBhIHJlZnJlc2g...",
  "expiresAt": "2026-11-06T10:00:00Z"
}
```

---

## ⚙️ Configuration

### Environment Variables

#### Products Service
| Variable | Default | Description |
|---|---|---|
| `MYSQL_HOST` | `localhost` | MySQL hostname |
| `MYSQL_PASSWORD` | `Temp123456` | MySQL password |

#### Users Service
| Variable | Default | Description |
|---|---|---|
| `POSTGRES_HOST` | `localhost` | PostgreSQL hostname |
| `POSTGRES_PASSWORD` | `admin` | PostgreSQL password |
| `Jwt__Key` | *(required)* | JWT signing secret key |

### Local Development (appsettings.json)

**Products Service** connection string:
```
Server=$MYSQL_HOST;Port=3306;Database=ecommerceproductsdatabase;User ID=product_api;Password=$MYSQL_PASSWORD;
```

---

## 🏗️ Building Docker Images

### Products Microservice

```bash
cd eCommerceSolution.ProductService
docker build -t ms2002/ecommerce-products-microservice:v1.0 .
```

### Users Microservice

```bash
cd eCommerceSolution.UsersService
docker build -t ms2002/ecommerce-users-microservice:v1.0 .
```

---

## 📦 NuGet Packages Used

### Products Service
| Package | Purpose |
|---|---|
| `MySql.EntityFrameworkCore` | MySQL EF Core provider |
| `AutoMapper` | Object mapping |
| `FluentValidation` | Input validation |
| `Swashbuckle.AspNetCore` | Swagger UI |

### Users Service
| Package | Purpose |
|---|---|
| `Npgsql` | PostgreSQL driver |
| `Dapper` | Micro-ORM for PostgreSQL |
| `AutoMapper` | Object mapping |
| `FluentValidation` | Input validation |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | JWT authentication |
| `Microsoft.Extensions.Identity.Core` | Password hashing |
| `Swashbuckle.AspNetCore` | Swagger UI |

---

## 🗺️ Roadmap

### Phase 1 — Foundation ✅
- [x] Products Microservice (ASP.NET Core + MySQL + EF Core)
- [x] Users Microservice (ASP.NET Core + PostgreSQL + Dapper)
- [x] JWT Authentication with Refresh Tokens
- [x] Docker containers for all services
- [x] Docker Compose local orchestration
- [x] Angular front-end clients
- [x] FluentValidation & AutoMapper integration

### Phase 2 — Messaging & Caching 🔲
- [ ] RabbitMQ integration for async event-driven communication
- [ ] Redis distributed caching for product catalog
- [ ] Service-to-service communication patterns

### Phase 3 — Resilience 🔲
- [ ] Polly retry policies
- [ ] Circuit breaker pattern
- [ ] Fallback strategies
- [ ] Health checks for all services

### Phase 4 — API Gateway 🔲
- [ ] Ocelot or YARP API Gateway
- [ ] Rate limiting
- [ ] Request aggregation
- [ ] Centralized authentication

### Phase 5 — Kubernetes & Cloud 🔲
- [ ] Kubernetes manifests (Deployments, Services, ConfigMaps)
- [ ] Azure Kubernetes Service (AKS) deployment
- [ ] Helm charts for environment management
- [ ] Kubernetes Ingress controller

### Phase 6 — CI/CD 🔲
- [ ] Azure DevOps pipeline setup
- [ ] Automated build and test pipelines
- [ ] Docker image publishing to Azure Container Registry (ACR)
- [ ] Automated AKS deployment pipeline

---

## 🤝 Contributing

Contributions, issues and feature requests are welcome! Feel free to check the [issues page](#).

---

## 📝 License

This project is for educational and demonstration purposes.

---

## 👨‍💻 Author

Built with ❤️ using **ASP.NET Core 10**, **Docker**, and modern cloud-native patterns.
