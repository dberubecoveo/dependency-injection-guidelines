# Dependency Injection Guidelines

This repository provides practical examples and guidelines for implementing dependency injection patterns in the coveo-platform/connectors repository.

## Overview

Dependency Injection (DI) is a design pattern that implements Inversion of Control (IoC) for resolving dependencies. It allows for more testable, maintainable, and loosely coupled code by removing direct dependencies between classes.

### Why Dependency Injection?

- **Testability**: Easy to mock dependencies for unit tests
- **Maintainability**: Changes to dependencies don't require changes to dependent classes
- **Single Responsibility**: Classes focus on their core functionality, not dependency management
- **Loose Coupling**: Classes depend on abstractions, not concrete implementations

### Gradual Implementation Strategy

Introducing dependency injection all at once wouldn't be realistic for an existing codebase. Instead, we provide guidelines that pave the way to a smoother transition:

1. **Incremental adoption**: Introduce changes as you create new classes or modify existing ones
2. **Minimal complexity**: Keep changes focused to avoid overwhelming pull requests
3. **Backward compatibility**: Maintain existing functionality while introducing new patterns
4. **Clear migration path**: Each change moves us closer to a full DI framework implementation

## Architecture Overview

The application demonstrates a layered architecture with dependency injection:

```
MainProcess (Entry Point)
    ↓
PrimaryJobService
    ↓
MyClass
    ↓
Various Services & Factories
```

## Starting point

The `MainProcess` class is the root of the application. This is where the `DependencyContainer` (which holds the singleton instances) is created and then passed to the `PrimaryJobService`.

# Implementation Guidelines

## 1. Dependencies as Private Properties

### Principle
Dependencies should be declared as private properties with getters, making them easily identifiable and immutable after construction.

### Example
If you look at `PrimaryJobService` and `MyClass`, you'll see dependencies defined as:

```csharp
private IPriceGeneratorService PriceGenerator { get; }
private IDeviceTypeGeneratorService DeviceTypeGenerator { get; }
private IDeviceFactory DeviceFactory { get; }
```

### Benefits
- **Clarity**: Dependencies are immediately visible at the class level
- **Immutability**: Properties can only be set during construction
- **Consistency**: Uniform pattern across all classes

## 2. Constructor Injection

### Principle
Dependencies are injected through the constructor, ensuring they are available when the class is instantiated and promoting immutability.

### Dual Constructor Pattern
Classes should have two constructors during the transition period:

1. **Container Constructor**: Accepts `DependencyContainer` (temporary, for current architecture)
2. **Direct Constructor**: Accepts individual dependencies (permanent, for future DI framework and tests)

### Example
```csharp
public class MyClass : IMyClass
{
    private IDeviceTypeGeneratorService DeviceTypeGenerator { get; }
    private IPriceGeneratorService PriceGenerator { get; }
    private IDeviceFactory DeviceFactory { get; }

    // Temporary constructor using container
    public MyClass(DependencyContainer dependencyContainer)
        : this(dependencyContainer.DeviceTypeGenerator,
               dependencyContainer.PriceGenerator,
               dependencyContainer.DeviceFactory)
    {
    }

    // Permanent constructor for DI framework
    public MyClass(IDeviceTypeGeneratorService deviceTypeGenerator,
                   IPriceGeneratorService priceGenerator,
                   IDeviceFactory deviceFactory)
    {
        DeviceTypeGenerator = deviceTypeGenerator;
        PriceGenerator = priceGenerator;
        DeviceFactory = deviceFactory;
    }
}
```

### Benefits
- **Explicit dependencies**: Clear what the class needs to function
- **Testability**: Easy to provide mock implementations
- **Future-proof**: Direct constructor will work with DI frameworks

## 3. Dependency Container

### Why Not Static Classes?
Classes with dependencies should never be marked as static because:
- **Testing difficulties**: Cannot mock static dependencies
- **Concurrency issues**: Shared state problems in tests

### Container Pattern
The `DependencyContainer` serves as a temporary service locator that:
- Holds singleton instances of services
- Provides a centralized registry of dependencies
- Bridges the gap between current architecture and future DI framework
- Eliminates the need to pass dependencies through multiple constructor layers

### Implementation Example
```csharp
// Container creation at application root
var configuration = new Config();
var container = new DependencyContainer(configuration);

// Container passed to services
var primaryJob = new PrimaryJobService(container);
primaryJob.StartJob();
```

### Container Usage in Classes
```csharp
public class PrimaryJobService : IPrimaryJobService
{
    private IMyClass MyClass { get; }

    // Container constructor (temporary)
    public PrimaryJobService(DependencyContainer dependencyContainer)
        : this(dependencyContainer.MyClass)
    {
    }

    // Direct injection constructor (permanent)
    public PrimaryJobService(IMyClass myClass)
    {
        MyClass = myClass;
    }
}
```

### Migration Benefits
- **No cascading changes**: Adding new dependencies doesn't require updating multiple constructor layers
- **Gradual transition**: Can introduce DI patterns without rewriting the entire application
- **Framework readiness**: Easy to replace container with proper DI framework later

## 4. Factory Pattern for Transient Dependencies

### Lifecycle Management
Different types of dependencies have different lifecycles:
- **Singleton**: One instance for the entire application lifetime
- **Transient**: New instance created each time it's requested
- **Scoped**: Single instance within a specific scope (e.g., per request, per operation)

### When to Use Factories
Use factories for dependencies that:
- Need to be created with different parameters each time
- Have a short lifecycle (transient or scoped)
- Require complex initialization logic
- Need to be created conditionally based on runtime parameters

### Factory Implementation
```csharp
public interface IDeviceFactory
{
    Device CreateDevice(DeviceType deviceType, string deviceId);
}

public class DeviceFactory : IDeviceFactory
{
    private IIdGenerationServiceFactory IdGenerationServiceFactory { get; }

    public DeviceFactory(IIdGenerationServiceFactory idGenerationServiceFactory)
    {
        IdGenerationServiceFactory = idGenerationServiceFactory;
    }

    public Device CreateDevice(DeviceType deviceType, string deviceId)
    {
        var idGenerationService = IdGenerationServiceFactory.CreateIdGenerationService();

        return deviceType switch
        {
            DeviceType.Desktop => new Desktop(deviceId, idGenerationService),
            DeviceType.Laptop => new Laptop(deviceId, idGenerationService),
            DeviceType.Phone => new Phone(deviceId, idGenerationService),
            _ => throw new ArgumentException($"Unknown device type: {deviceType}")
        };
    }
}
```

### Benefits
- **Parameter flexibility**: Can create instances with different parameters
- **Lifecycle control**: Proper management of transient instances
- **Encapsulation**: Complex creation logic is isolated in factories
- **Testability**: Easy to mock factory behavior in tests

## Best Practices and Anti-Patterns

### ✅ Do's
- **Use interfaces**: Always depend on abstractions, not concrete classes
- **Single responsibility**: Each class should have one reason to change
- **Consistent naming**: Use clear, descriptive names for dependencies
- **Immutable dependencies**: Make dependency properties read-only

### ❌ Don'ts
- **Service locator everywhere**: Don't pass the container to every class, only those who need singleton dependencies or factories
- **Static dependencies**: Avoid static classes with dependencies
- **Poor interface design**: Don't create interfaces that are too broad or too narrow
- **Hidden dependencies**: All dependencies should be explicit in constructors

### Code Quality Guidelines
```csharp
// ✅ Good: Clear, explicit dependencies
public class OrderService
{
    private IPaymentService PaymentService { get; }
    private IInventoryService InventoryService { get; }
    private INotificationService NotificationService { get; }

    public OrderService(IPaymentService paymentService,
                       IInventoryService inventoryService,
                       INotificationService notificationService)
    {
        PaymentService = paymentService;
        InventoryService = inventoryService;
        NotificationService = notificationService;
    }
}

// ❌ Bad: Hidden dependencies, hard to test
public static class OrderService
{
    public static void ProcessOrder(Order order)
    {
        PaymentService.ProcessPayment(order.Payment); // Hidden dependency
        InventoryService.UpdateStock(order.Items);    // Hidden dependency
        NotificationService.SendConfirmation(order);  // Hidden dependency
    }
}
```

## Migration Strategy

### Phase 1: Current State (Service Locator)
- Implement dual constructors in new classes
- Use `DependencyContainer` for singleton management
- Convert static classes to instance-based classes
- Introduce interfaces for all dependencies

### Phase 2: Preparation (Framework Ready)
- Ensure all classes have direct injection constructors
- Minimize container usage to root composition
- Implement comprehensive unit tests using direct constructors
- Document all dependency relationships

### Phase 3: Framework Integration
- Choose DI framework (Castle, Microsoft.Extensions.DependencyInjection, Autofac, etc.)
- Configure service registrations
- Remove container constructors
- Update root composition to use DI framework

### Phase 4: Advanced Patterns
- Implement decorator pattern for cross-cutting concerns
- Add scoped dependencies for request/operation contexts
- Implement factory patterns for complex object creation
- Add configuration-based service selection