# Sales Management System

A modern, full-featured enterprise Sales Management System API built with **ASP.NET Core** and designed following industry best practices. This portfolio project demonstrates proficiency in backend development, API design, authentication, and database management.

---

## 🎯 Project Overview

This Sales Management System is a RESTful API that manages sales operations including employees, clients, branches, and transactions. It showcases professional-grade architecture with secure authentication, role-based access control, and comprehensive data management capabilities.

---

## ✨ Key Features & Achievements

### 🔐 **Authentication & Security**
- **JWT (JSON Web Token) Authentication** - Secure token-based authentication with configurable expiration and validation
- **ASP.NET Identity Integration** - User registration, login, and identity management
- **Password Security** - Enforced password policies:
  - Minimum 8 characters
  - Requires uppercase, lowercase, and digits
  - Account lockout after 5 failed attempts
- **Custom JWT Response Handling** - Tailored HTTP status codes and JSON responses for authentication failures

### 📊 **Database Design & Management**
- **SQL Server Integration** - Production-ready relational database using Entity Framework Core
- **Complex Relationships** - Sophisticated data models including:
  - Employee-Branch hierarchy (managers and subordinates)
  - Multi-tenant client management across branches
  - Transaction tracking with idempotency keys for data integrity
- **Entity Framework Migrations** - Version-controlled schema changes with reversible migrations
- **Optimized Models** - Precision decimal types for financial data (18,2 precision)

### 🏗️ **API Architecture**
- **RESTful Design Principles** - Clean separation of concerns with Controllers, Services, and Repositories
- **Repository Pattern** - Abstraction layer for data access (Interfaces & Implementations)
- **Data Transfer Objects (DTOs)** - Type-safe communication between layers
  - Account DTOs: `RegisterDto`, `NewUserDto`
  - Entity-specific DTOs for Clients, Employees, and Branches
- **Mapper Pattern** - Automated object mapping for clean data transformations

### 🔄 **API Versioning & Documentation**
- **API Versioning** - Built-in versioning support (v1.0 with extensibility for future versions)
- **Swagger/OpenAPI** - Auto-generated API documentation with Bearer token authentication support
- **Version-Aware Routing** - Seamless URL substitution for API versions

### 💾 **Caching & Performance**
- **Memory Caching** - Built-in in-memory cache for improved response times
- **Optimized Queries** - Efficient data retrieval patterns

### 📦 **Code Quality**
- **SOLID Principles** - Interface-driven design with dependency injection
- **Nullable Reference Types** - Modern C# null-safety features
- **Entity Relationships** - Proper foreign key management and referential integrity

---

## 🛠️ Technologies & Stack

| Technology | Purpose |
|-----------|---------|
| **ASP.NET Core** | RESTful API Framework |
| **Entity Framework Core** | ORM & Database Management |
| **SQL Server** | Relational Database |
| **JWT Bearer** | Authentication & Authorization |
| **Swagger/OpenAPI** | API Documentation |
| **API Versioning** | Version Management |
| **ASP.NET Identity** | User Management |
| **Docker** | Containerization |

**Language:** C# (99.7% of codebase)

---

## 📂 Project Structure
