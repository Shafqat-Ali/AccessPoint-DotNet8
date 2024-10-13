## AccessPoint  
A .NET 8 Web API Project Using Clean Architecture, Entity Framework, and JWT Authentication  
## Overview  
AccessPoint is a RESTful Web API built with .NET 8, designed for secure user authentication, registration, and balance management. The project follows Clean Architecture principles, ensuring the separation of concerns and scalability. It leverages Entity Framework Core for database interactions and JWT (JSON Web Token) for authentication, with password hashing for enhanced security.   

## Features  
- User Signup API: Registers a new user with hashed passwords.  
- User Authentication API: Authenticates users via JWT tokens and logs login information.  
- Balance API: Returns the user's current balance (5 GBP credited on first signup).  
- Clean Architecture: Separates business logic, data access, and API layers.  
- Entity Framework Core: Manages database interaction.  
- JWT Authentication: Secures endpoints with token-based authentication.  


## Project Structure  
### The project follows Clean Architecture and consists of the following layers:  

- Domain Layer: Core entities (Users, LoginHistory).  
- Application Layer: Business logic, DTOs, and services (signup, authentication, balance retrieval).  
- Infrastructure Layer: Database context, repositories, and integration with Entity Framework Core.  
- Presentation Layer: API controllers for interacting with the system
