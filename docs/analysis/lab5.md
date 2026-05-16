# Модульний моноліт

## Мета

Метою лабораторної роботи було розділення системи на ізольовані модулі (bounded contexts) у межах одного застосунку. Також було реалізовано міжмодульну взаємодію через події, ACL (Anti-Corruption Layer) та eventual consistency.

---

# Реалізовані bounded contexts

У системі було виділено два основних контексти:

## Core Context

Основний модуль системи, який відповідає за:

- створення бронювань;
- отримання списку бронювань;
- перевірку конфліктів часу;
- бізнес-логіку системи.

Модуль використовує:

- handlers;
- repositories;
- events;
- event bus;
- notification service.

---

## Analytics Context

Аналітичний модуль відповідає за:

- отримання подій із основного контексту;
- створення власної моделі аналітики;
- збереження статистики;
- побудову окремої read-моделі.

Модуль працює незалежно від Core Context та не змінює його дані.

---

# Модульна структура

Система була поділена на окремі модулі:

```text
BookingApi
BookingApi.Application
BookingApi.Domain
BookingApi.Infrastructure
BookingApi.Analytics
BookingApi.Tests
```

Кожен модуль має власну відповідальність та власну внутрішню структуру.

---

# Реалізований ACL

Для взаємодії між модулями було реалізовано ACL (Anti-Corruption Layer).

ACL транслює модель події з основного контексту у внутрішню модель аналітичного модуля.

---

## Приклад ACL

```csharp
public static class BookingEventAcl
{
    public static BookingAnalyticsModel
        ToAnalyticsModel(
            BookingCreatedEvent @event)
    {
        return new BookingAnalyticsModel
        {
            UserId = @event.UserId,
            Description = @event.Description
        };
    }
}
```

---

# Реалізація міжмодульної комунікації

Для взаємодії між модулями використовується Event Bus.

Після створення бронювання основний модуль публікує подію:

```csharp
await _eventBus.PublishAsync(
    bookingCreatedEvent);
```

Аналітичний модуль підписується на цю подію через handler.

---

## Подія

```csharp
public class BookingCreatedEvent
{
    public int UserId { get; set; }

    public string Description
    {
        get;
        set;
    } = string.Empty;
}
```

---

## Handler аналітики

```csharp
public class AnalyticsBookingCreatedHandler
{
    private readonly IAnalyticsService
        _analyticsService;

    public AnalyticsBookingCreatedHandler(
        IAnalyticsService analyticsService)
    {
        _analyticsService =
            analyticsService;
    }

    public async Task Handle(
        BookingCreatedEvent @event)
    {
        var model =
            BookingEventAcl
                .ToAnalyticsModel(@event);

        await _analyticsService
            .SaveAsync(model);
    }
}
```

---

# Eventual Consistency

У системі реалізовано eventual consistency.

Після створення бронювання основна операція завершується одразу, а аналітика обробляється окремо через подію.

Це дозволяє:

- зменшити зв’язність між модулями;
- зробити систему більш масштабованою;
- ізолювати бізнес-логіку модулів.

---

# Strong Consistency та Eventual Consistency

## Strong Consistency

Strong consistency використовується всередині основного модуля під час:

- створення бронювання;
- перевірки конфліктів;
- збереження даних у базу.

---

## Eventual Consistency

Eventual consistency використовується між модулями:

- Core публікує подію;
- Analytics отримує подію;
- аналітика оновлюється асинхронно.

---

# Порівняння з лабораторною роботою 4

У лабораторній роботі 4 система використовувала синхронну та асинхронну комунікацію між компонентами в межах одного контексту.

У лабораторній роботі 5 архітектура стала більш модульною. Було виділено окремі bounded contexts та реалізовано міжмодульну взаємодію через події.

---

## Що змінилось

### Лабораторна 4

- один контекст системи;
- допоміжний notification component;
- Event Bus використовувався всередині системи;
- слабка ізоляція модулів.

---

### Лабораторна 5

- декілька bounded contexts;
- окремий Analytics module;
- ACL між модулями;
- незалежні моделі даних;
- міжмодульна взаємодія через events;
- eventual consistency.

---

# Переваги нового підходу

Після переходу до модульного моноліту система стала:

- більш гнучкою;
- простішою для масштабування;
- менш зв’язаною;
- зручнішою для майбутнього переходу на мікросервіси.

---

# Недоліки

Недоліками підходу є:

- складніша структура проєкту;
- більша кількість модулів;
- складніше тестування;
- необхідність підтримки ACL та event communication.

---

# Висновок

У результаті лабораторної роботи було реалізовано модульний моноліт із bounded contexts, ACL та міжмодульною взаємодією через події.

Було створено окремий аналітичний модуль, який працює незалежно від основної бізнес-логіки системи.

Також було реалізовано eventual consistency між модулями та ізоляцію внутрішніх моделей через ACL.

Архітектура системи стала більш масштабованою, гнучкою та ближчою до реальних enterprise-рішень.
