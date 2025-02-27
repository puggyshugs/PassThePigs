# Pass the Pigs - Backend

## Overview
This is the backend for the *Pass the Pigs* multiplayer game, built with .NET 9. It handles game logic, player interactions, and real-time updates using SignalR. The app uses an in-memory cache for temporary data storage, making it lightweight and fast.

## Running the Application
To start the backend, navigate to the `PassThePigs.Api` project directory and run:

```sh
cd PassThePigs.Api
 dotnet run
```

By default, the API will start on `http://localhost:5233`.

## Technologies Used
- **ASP.NET 9** – Main framework for the API.
- **SignalR** – Enables real-time multiplayer communication.
- **In-Memory Cache (ConcurrentDictionary)** – Stores game data without needing a database.
- **Dependency Injection** – Follows clean architecture principles.

## Project Structure

- `PassThePigs.Api` - Handles incoming HTTP requests.
- `PassThePigs.Services` - Implements game logic and cache interaction.
- `PassThePigs.Data` - Manages the in-memory cache (could be replaced with Redis/DB later).
- `PassThePigs.Hubs` - Contains hubs for SignalR requests.
- `PassThePigs.Domain` - Holds the data models used across the application.

## Key Design Decisions

### **In-Memory Cache**
- The in-memory cache uses a **ConcurrentDictionary** and is **synchronous** since it's not blocking and remains thread-safe.
- Since it's in-memory, **async operations aren’t required**, reducing complexity.
- If migrating to **Redis or a database**, we'd need to make data operations async.

### **SignalR and Async Execution**
- SignalR must remain **asynchronous** to handle multiple concurrent connections.
- API methods are also **asynchronous** to support scalable client requests.

### **Why Not a Repository Pattern?**
- Since the cache is temporary storage, a repository layer is **not necessary**.
- If switching to a **database**, a repository pattern could be introduced.
- The `GameMemoryCache` was placed in the **Data Layer** to separate concerns from the service layer, and helps future-proof should I add a DB or Redis cache later.

## Future Improvements
- Replace in-memory cache with **Redis** or a **SQL database** for persistence.
- Implement **authentication** for secured game sessions.
- Expand **unit tests** for better reliability.

---

This backend is lightweight and scalable, ensuring smooth real-time gameplay. 🚀

