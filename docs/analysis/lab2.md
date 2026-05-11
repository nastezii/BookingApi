# Booking API

REST API для системи бронювання з реалізацією шарової архітектури, Domain Model та JWT-автентифікації.

---

# Мета роботи

Розділити бізнес-логіку та інфраструктуру, реалізувати Domain Model і побудувати проєкт за принципами Clean Architecture.

---

# Технології

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Authentication
- Swagger
- xUnit

---

# Архітектура

Проєкт розділений на 4 шари:

```text
BookingApi                -> Presentation Layer
BookingApi.Application    -> Application Layer
BookingApi.Domain         -> Domain Layer
BookingApi.Infrastructure -> Infrastructure Layer
```

---

# Presentation Layer

Містить:
- Controllers
- HTTP endpoints
- DTO
- JWT authentication

### Controllers

- AuthController
- BookingsController

---

# Application Layer

Містить:
- use cases
- application services
- orchestration logic
- contracts

### Services

- BookingService
- AuthService

### Contracts

- BookingRequest
- LoginRequest
- RegisterRequest

---

# Domain Layer

Містить:
- domain entities
- value objects
- repository interfaces
- domain factories
- domain errors

---

## Domain Entities

### Booking

Модель бронювання.

Містить:
- Id
- UserId
- TimeRange
- Description

### User

Модель користувача.

Містить:
- Id
- Email
- PasswordHash

---

## Value Objects

### TimeRange

Описує часовий діапазон бронювання.

Інваріанти:
- start < end
- коректний часовий діапазон

---

## Domain Factory

### BookingFactory

Відповідає за створення бронювань та перевірку інваріантів.

Перевіряє:
- бронювання не в минулому
- часовий діапазон валідний
- відсутність конфліктів

---

## Domain Errors

### DomainError

Використовується для доменних помилок:
- invalid email
- invalid range
- booking conflict

---

## Repository Interfaces

### IBookingRepository

Описує:
- отримання бронювань
- створення
- видалення
- перевірку конфліктів

### IUserRepository

Описує:
- пошук користувача
- перевірку email
- авторизацію

---

# Infrastructure Layer

Містить:
- Entity Framework Core
- SQLite
- DbContext
- ORM entities
- repository implementations
- mappers

---

## DbContext

### AppDbContext

Містить:
- DbSet<UserEntity>
- DbSet<BookingEntity>

---

## ORM Entities

### BookingEntity

ORM-модель бронювання для EF Core.

### UserEntity

ORM-модель користувача для EF Core.

---

## Repository Implementations

### EFBookingRepository

Реалізація:
- IBookingRepository
- IUserRepository

Через Entity Framework Core.

---

## Mappers

### BookingMapper

Мапінг:
- Booking ↔ BookingEntity

### UserMapper

Мапінг:
- User ↔ UserEntity

---

# Бізнес-правила

## Для користувача

- email повинен бути валідним
- email має бути унікальним

## Для бронювання

- початок бронювання не може бути в минулому
- кінець має бути після початку
- бронювання не можуть перетинатися

---

# JWT Authentication

Реалізовано:
- реєстрацію
- логін
- генерацію JWT token
- захист ендпоінтів через `[Authorize]`

---

# Тести

## Unit Tests

Перевіряють:
- domain logic
- validation
- factories
- services

### Приклади

- booking validation
- booking conflicts
- invalid time range

---

## Integration Tests

Перевіряють:
- API endpoints
- авторизацію
- повний HTTP flow
- роботу з базою даних

---

# Основні use cases

- реєстрація користувача
- логін користувача
- створення бронювання
- оновлення бронювання
- видалення бронювання
- перегляд бронювань

---

# Відмінності від лабораторної №1

## Лабораторна №1

- бізнес-логіка знаходилась у контролерах
- ORM-моделі використовувались напряму
- DbContext використовувався у Presentation Layer
- сильна залежність від Entity Framework

---

## Лабораторна №2

- логіка винесена у Application Layer
- створений Domain Layer
- використовується Repository Pattern
- додані Domain Entities
- додані Value Objects
- додані Factories
- реалізований mapping між Domain та ORM моделями

---

# Переваги нової архітектури

- слабша залежність між шарами
- простіше тестування
- легше замінити базу даних
- краща масштабованість
- чистіша структура проєкту
- бізнес-логіка не залежить від ORM

---

# Структура проєкту

```text
BookingApi
│
├── Controllers
├── Program.cs
│
├── BookingApi.Application
│   ├── Contracts
│   └── Services
│
├── BookingApi.Domain
│   ├── Entities
│   ├── Errors
│   ├── Factories
│   ├── Repositories
│   └── ValueObjects
│
├── BookingApi.Infrastructure
│   ├── Data
│   ├── Entities
│   ├── Mappers
│   └── Repositories
│
└── BookingApi.Tests
```

---

# Запуск проєкту

## 1. Clone repository

```bash
git clone <repository-url>
```

---

## 2. Open solution

Відкрити:

```text
BookingApi.sln
```

---

## 3. Restore packages

```bash
dotnet restore
```

---

## 4. Run project

```bash
dotnet run
```

---

# Swagger

Swagger доступний після запуску:

```text
https://localhost:<port>/swagger
```

---
