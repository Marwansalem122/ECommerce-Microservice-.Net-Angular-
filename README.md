# 🛒 eCommerce Microservices Platform

A **comprehensive eCommerce application** built with **ASP.NET Core 10** using a microservices architecture. This project demonstrates real-world patterns for building, containerizing, orchestrating, and deploying scalable microservices using modern DevOps tooling.

---

## 🚀 What I Will Complete

> **Learning Roadmap & Goals**

- ✅ Building a comprehensive eCommerce application with **ASP.NET Core Microservices**
- ✅ Orders Microservice with **MongoDB**, **service-to-service HTTP communication**
- 🔲 Utilizing **Docker**, **Kubernetes**, **RabbitMQ**, **Redis**, **Azure DevOps**, and **Azure Kubernetes Service (AKS)**
- 🔲 Practical skills in **containerization**, **orchestration**, **fault tolerance**, **caching**, **API management**, and **authentication**
- 🔲 Creating and managing **Azure DevOps pipelines** for continuous integration and deployment (CI/CD)
- 🔲 Implementing **fault tolerance** using **Polly** for retry strategies, circuit breakers, and fallback methods
- 🔲 Managing microservices communication with **RabbitMQ** for reliable messaging and integrating **Redis** for caching data
- 🔲 Hosting and orchestrating microservices on **Azure Kubernetes Service (AKS)** for scalable and efficient deployments

---

## 📐 Architecture Overview

```
┌───────────────────────────────────────────────────────────────────────────────┐
│                            eCommerce Platform                                 │
│                                                                               │
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────────────┐    │
│  │ Products Service │  │  Users Service   │  │     Orders Service       │    │
│  │ ASP.NET Core 10  │  │ ASP.NET Core 10  │  │    ASP.NET Core 10       │    │
│  │  Port: 7000      │  │  Port: 5000      │  │    Port: 7000 (compose)  │    │
│  │  DB: MySQL 8.0   │  │  DB: PostgreSQL  │  │    DB: MongoDB           │    │
│  └──────────────────┘  └──────────────────┘  └──────────────────────────┘    │
│           ▲                     ▲                   ▲          ▲              │
│           │  HTTP Client        │  HTTP Client      │          │              │
│           └─────────────────────┴───────────────────┘          │              │
│                                                                │              │
│  ┌──────────────────┐  ┌──────────────────┐                   │              │
│  │ Angular Products │  │  Angular Users   │  ←── CORS: 4200   │              │
│  │  front-end       │  │  front-end       │                   │              │
│  └──────────────────┘  └──────────────────┘                   │              │
│                                                                               │
│               🐳 Docker / Docker Compose (per service)                        │
│               ☸  Kubernetes / AKS (Planned)                                  │
│               🐰 RabbitMQ Messaging (Planned)                                 │
│               ⚡ Redis Caching (Planned)                                      │
└───────────────────────────────────────────────────────────────────────────────┘
```

---

## 🗂️ Project Structure

```
Microservices/
├── .gitignore                                    # Git ignore rules
├── compose.yaml                                  # Docker Compose (Products + Users)
├── mysql-init/
│   └── db.sql                                    # MySQL schema initialization
├── postgres-init/
│   ├── sql1.sql                                  # PostgreSQL schema (part 1)
│   └── sql2.sql                                  # PostgreSQL schema (part 2)
├── mongodb-init/
│   └── init.js                                   # MongoDB initialization script
├── ordersdb/                                     # ⚠️ MongoDB local data (git-ignored)
│
├── eCommerceSolution.ProductService/             # 📦 Products Microservice
│   ├── DataAccessLayer/                          # EF Core + MySQL data access
│   │   ├── Context/                              # DbContext
│   │   ├── Entities/                             # Domain entities
│   │   ├── Repositories/                         # Repository implementations
│   │   └── RepositoryContracts/                  # Repository interfaces
│   ├── BusinessLogicLayer/                       # Business logic + AutoMapper + FluentValidation
│   │   ├── DTO/                                  # Data Transfer Objects
│   │   ├── Mappers/                              # AutoMapper profiles
│   │   ├── ServiceContract/                      # Service interfaces
│   │   ├── Services/                             # Service implementations
│   │   └── Validators/                           # FluentValidation validators
│   └── ProductMicroService.API/                  # ASP.NET Core Web API
│       ├── ProductAPIEndPoints/                  # Minimal API endpoints
│       ├── Middlewares/                          # Exception handling middleware
│       ├── Dockerfile                            # Container definition
│       └── Program.cs                           # App entry point
│
├── eCommerceSolution.UsersService/               # 👤 Users & Auth Microservice
│   ├── eCommerce.Core/                           # Domain layer
│   │   ├── DTO/                                  # Auth DTOs (Login, Register, Refresh)
│   │   ├── Entities/                             # ApplicationUser, RefreshToken
│   │   ├── Mappers/                              # AutoMapper profiles
│   │   ├── RespositoryContract/                  # Repository interfaces
│   │   ├── ServicesContract/                     # Service + JWT interfaces
│   │   ├── Services/                             # UserService implementation
│   │   └── Validators/                           # Login/Register validators
│   ├── eCommerce.Infrastructure/                 # Infrastructure layer
│   │   ├── DbContext/                            # Dapper DbContext
│   │   └── Repositories/                         # User + RefreshToken repositories
│   └── eCommerce.API/                            # ASP.NET Core Web API
│       ├── Controllers/                          # AuthController
│       ├── Services/                             # JwtTokenGenerator
│       ├── Middlewares/                          # Exception handling middleware
│       ├── Dockerfile                            # Container definition
│       └── Program.cs                           # App entry point
│
├── eCommerceSolution.OrdersService/              # 🛒 Orders Microservice ✅
│   ├── docker-compose.yml                        # Orders-specific Docker Compose
│   ├── docker-compose.override.yml               # Local overrides
│   ├── DataAccessLayer/                          # MongoDB data access
│   │   ├── Entities/                             # Order, OrderItem entities
│   │   ├── Repositories/                         # OrdersRepository (MongoDB)
│   │   └── RepositoryContracts/                  # IOrdersRepository interface
│   ├── BusinessLogicLayer/                       # Business logic layer
│   │   ├── DTO/                                  # Order DTOs (Add/Update/Response)
│   │   ├── HttpClients/                          # UsersMicroserviceClient, ProductsMicroserviceClient
│   │   ├── Mappers/                              # AutoMapper profiles
│   │   ├── ServiceContracts/                     # IOrdersService interface
│   │   ├── Services/                             # OrdersService implementation
│   │   └── Validators/                           # FluentValidation validators
│   └── OrderMicroservice.API/                    # ASP.NET Core Web API
│       ├── ApiControllers/                       # OrdersController
│       ├── Middlewares/                          # Exception handling middleware
│       ├── Dockerfile                            # Container definition
│       └── Program.cs                           # App entry point
│
└── front-end/
    ├── Product-Service/                          # Angular app for Products
    └── User-Service/                             # Angular app for Users
```

---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| **ASP.NET Core 10** | Microservice Web APIs |
| **Entity Framework Core** | ORM for Products (MySQL) |
| **Dapper** | Lightweight ORM for Users (PostgreSQL) |
| **MongoDB.Driver 3.13** | MongoDB driver for Orders |
| **AutoMapper** | Object-to-object mapping |
| **FluentValidation** | Input validation |
| **JWT Bearer Auth** | Authentication & token management |
| **HttpClient** | Service-to-service HTTP communication |
| **Swashbuckle / Swagger** | API documentation |

### Databases
| Database | Version | Service |
|---|---|---|
| **MySQL** | 8.0 | Products Microservice |
| **PostgreSQL** | 13 | Users Microservice |
| **MongoDB** | latest | Orders Microservice |

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

### Option 1 — Products + Users Services (root compose)

```bash
# Clone the repository
git clone <your-repo-url>
cd Microservices

# Pull and start Products + Users services
docker compose up -d

# View running containers
docker compose ps

# View logs
docker compose logs -f

# Stop services
docker compose down
```

### Option 2 — Orders Service (with all dependencies)

```bash
cd eCommerceSolution.OrdersService

# Start Orders + MongoDB + Products + Users
docker compose up -d

# Stop
docker compose down
```

### Services & Ports

#### Root `compose.yaml` (Products + Users)
| Service | Container Name | Port (Host → Container) |
|---|---|---|
| Products API | `products-microservice` | `8080:8080` |
| Users API | `users-microservice` | `18090:9090` |
| MySQL | `mysql-container` | `3307:3306` |
| PostgreSQL | `postgres-container` | `5432:5432` |

#### Orders `docker-compose.yml`
| Service | Container Name | Port (Host → Container) |
|---|---|---|
| Orders API | `ordermicroservice.api` | `7000:8080` |
| MongoDB | `mongodb-container` | `27017:27017` |
| Products API (dep.) | `products-microservice` | `6000:8080` |
| MySQL (dep.) | `mysql-container` | `3307:3306` |
| Users API (dep.) | `users-microservice` | `5000:9090` |
| PostgreSQL (dep.) | `postgres-container` | `5433:5432` |

---

## 📡 API Endpoints

### Products Service — `http://localhost:8080`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/products` | Get all products |
| `GET` | `/api/products/{id}` | Get product by ID |
| `GET` | `/api/products/search/product-id/{productID}` | Search product by ID |
| `POST` | `/api/products` | Create a new product |
| `PUT` | `/api/products/{id}` | Update a product |
| `DELETE` | `/api/products/{id}` | Delete a product |

### Users Service — `http://localhost:18090`

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/auth/register` | Register a new user |
| `POST` | `/api/auth/login` | Login and get JWT tokens |
| `POST` | `/api/auth/refresh-token` | Refresh JWT access token |
| `GET` | `/api/users/{userID}` | Get user by ID |

### Orders Service — `http://localhost:7000`

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/orders` | Get all orders |
| `GET` | `/api/orders/search/orderid/{orderID}` | Get order by Order ID |
| `GET` | `/api/orders/search/userid/{userID}` | Get orders by User ID |
| `GET` | `/api/orders/search/productid/{productID}` | Get orders by Product ID |
| `GET` | `/search/orderDate/{orderDate}` | Get orders by date |
| `POST` | `/api/orders` | Create a new order |
| `PUT` | `/api/orders/{orderID}` | Update an order |
| `DELETE` | `/api/orders/{orderID}` | Delete an order |

> 📖 Swagger UI available at:
> - Products: `http://localhost:8080/swagger`
> - Users: `http://localhost:18090/swagger`
> - Orders: `http://localhost:7000/swagger`

---

## 🔗 Service-to-Service Communication

The **Orders Service** communicates with other microservices via **HTTP clients**:

| Client | Target | Endpoint Called |
|---|---|---|
| `UsersMicroserviceClient` | Users Service | `GET /api/users/{userID}` |
| `ProductsMicroserviceClient` | Products Service | `GET /api/products/search/product-id/{productID}` |

Configured via environment variables:
```
UsersMicroserviceName   = <hostname>
UsersMicroservicePort   = <port>
ProductsMicroserviceName = <hostname>
ProductsMicroservicePort = <port>
```

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
| `MYSQL_HOST` | `mysql-container` | MySQL hostname |
| `MYSQL_PORT` | `3306` | MySQL port |
| `MYSQL_DATABASE` | `ecommerceproductsdatabase` | Database name |
| `MYSQL_PASSWORD` | `admin` | MySQL password |

#### Users Service
| Variable | Default | Description |
|---|---|---|
| `POSTGRES_HOST` | `postgres-container` | PostgreSQL hostname |
| `POSTGRES_PORT` | `5432` | PostgreSQL port |
| `POSTGRES_DATABASE` | `eCommerceUsers` | Database name |
| `POSTGRES_PASSWORD` | `admin` | PostgreSQL password |
| `Jwt__Key` | *(required)* | JWT signing secret key |

#### Orders Service
| Variable | Default | Description |
|---|---|---|
| `MONGODB_HOST` | `mongodb-container` | MongoDB hostname |
| `MONGODB_PORT` | `27017` | MongoDB port |
| `MONGODB_DATABASE` | `OrdersDatabase` | MongoDB database name |
| `UsersMicroserviceName` | — | Users service hostname |
| `UsersMicroservicePort` | — | Users service port |
| `ProductsMicroserviceName` | — | Products service hostname |
| `ProductsMicroservicePort` | — | Products service port |

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

### Orders Microservice

```bash
cd eCommerceSolution.OrdersService
docker build -f OrderMicroservice.API/Dockerfile -t ms2002/ecommerce-orders-microservice:v1.0 .
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

### Orders Service
| Package | Version | Purpose |
|---|---|---|
| `MongoDB.Driver` | 3.13.0 | MongoDB driver |
| `AutoMapper` | 16.3.0 | Object mapping |
| `FluentValidation` | 12.2.0 | Input validation |
| `FluentValidation.DependencyInjectionExtensions` | 12.2.0 | DI integration |
| `Swashbuckle.AspNetCore` | 10.3.1 | Swagger UI |

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
- [x] Orders Microservice (ASP.NET Core + MongoDB)
- [x] Service-to-service HTTP communication (Orders → Users, Orders → Products)

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
