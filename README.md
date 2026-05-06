# Booking API

REST API для системи бронювання з JWT-автентифікацією.

## Технології

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Authentication
- Swagger
- xUnit

---

# Лабораторна робота №1 — CRUD API

## Реалізовано

- CRUD для бронювань
- Реєстрація та логін користувачів
- JWT-автентифікація
- Захищені ендпоінти
- Swagger документація
- Unit та integration тести
- Валідація бізнес-правил

## Бізнес-правила

- email повинен бути валідним
- час початку бронювання не може бути в минулому
- час завершення має бути після початку
- не допускаються конфлікти бронювань

## Структура проєкту

- Controllers — API ендпоінти
- DTOs — моделі запитів
- Models — ORM-моделі
- Data — DbContext
- Tests — unit та integration тести

---

# Лабораторна робота №2 — Шарова архітектура

## Планується

- винесення бізнес-логіки із контролерів
- додавання service layer
- repository pattern
- domain models
- mapping між domain та ORM models
- domain exceptions
- ADR для вибору архітектури

---

# Запуск проєкту

```bash
dotnet restore
dotnet ef database update
dotnet run
