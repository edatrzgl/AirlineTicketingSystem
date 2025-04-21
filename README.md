# Airline Ticketing System

## Overview

Airline Ticketing System is a RESTful API built with **ASP.NET Core** and **Entity Framework Core**. It provides functionalities for managing flights, purchasing tickets, and performing check-in operations for an airline system. The project uses **SQL Server** as the database and includes features like user authentication with JWT, flight management, ticket purchasing, and passenger check-in.

### Features

- **Flight Management**: Add and query flights (Admin only).
- **Ticket Purchasing**: Purchase tickets for flights and assign passengers.
- **Check-In**: Perform check-in for passengers and update their status.
- **User Authentication**: Secure endpoints with JWT-based authentication (Admin and User roles).
- **Pagination**: Paginated results for flight queries and passenger lists.
- **Swagger UI**: Interactive API documentation and testing.

## Technologies Used

- **ASP.NET Core 8.0**: Backend framework.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server**: Database for storing flights, tickets, passengers, and users.
- **JWT Authentication**: Secure API endpoints.
- **BCrypt**: Password hashing for user authentication.
- **Swagger (Swashbuckle)**: API documentation and testing.
- **C#**: Programming language.

## Project Structure

- **Controllers/**: API controllers for handling HTTP requests.
  - `FlightsController.cs`: Manages flight-related operations.
  - `TicketsController.cs`: Handles ticket purchasing.
  - `CheckInController.cs`: Manages check-in operations.
  - `AuthController.cs`: Handles user authentication.
- **Services/**: Business logic for flights, tickets, check-in, and authentication.
- **DTOs/**: Data Transfer Objects for API requests and responses.
- **Models/**: Entity models for the database.
- **Data/**: Database context (`AirlineDbContext.cs`) and migrations.
- **appsettings.json**: Configuration file for database connection and JWT settings.

## Prerequisites

- **.NET 8.0 SDK**: Install from here.
- **SQL Server**: Install SQL Server and SSMS (SQL Server Management Studio).
- **IDE**: Visual Studio 2022 (or any IDE that supports .NET development).


### Run the Application

- Set the environment to Development:

  ```powershell
  $env:ASPNETCORE_ENVIRONMENT = "Development"
  ```
- Run the project:

  ```powershell
  dotnet run --launch-profile http
  ```
- The API will be available at `http://localhost:5205`.

### Access Swagger UI

- Open your browser and navigate to `http://localhost:5205/swagger`.
- Use Swagger UI to test the API endpoints.

## API Endpoints

### Authentication

- **POST /api/v1/auth/login**
  - Authenticate a user and get a JWT token.
  - Example Request:

    ```json
    {
      "username": "admin",
      "password": "admin123"
    }
    ```
  - Example Response:

    ```json
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    ```

### Flights

- **POST /api/v1/flights** (Admin only)
  - Add a new flight.
  - Example Request:

    ```json
    {
      "flightNumber": "TK401",
      "dateFrom": "2025-04-28T13:00:00Z",
      "dateTo": "2025-04-28T16:00:00Z",
      "airportFrom": "IST",
      "airportTo": "DXB",
      "duration": 180,
      "capacity": 200
    }
    ```
  - Example Response:

    ```json
    {
      "message": "Flight added"
    }
    ```
- **GET /api/v1/flights**
  - Query flights with filters.
  - Example Query: `?airportFrom=IST&airportTo=DXB`
  - Example Response:

    ```json
    [
      {
        "flightNumber": "TK401",
        "duration": 180,
        "dateFrom": "2025-04-28T13:00:00Z",
        "dateTo": "2025-04-28T16:00:00Z"
      }
    ]
    ```
- **GET /api/v1/flights/{flightNumber}/passengers**
  - Get the passenger list for a flight.
  - Example Query: `/api/v1/flights/TK401/passengers?date=2025-04-28`
  - Example Response:

    ```json
    [
      {
        "name": "Can Yılmaz",
        "seatNumber": 1
      }
    ]
    ```

### Tickets

- **POST /api/v1/tickets**
  - Purchase a ticket for a flight.
  - Example Request:

    ```json
    {
      "flightNumber": "TK401",
      "date": "2025-04-28",
      "passengerNames": ["Can Yılmaz", "Ece Kaya"]
    }
    ```
  - Example Response:

    ```json
    {
      "success": true,
      "ticketNumber": "p1q2r3s4",
      "message": "Ticket purchased successfully"
    }
    ```

### Check-In

- **POST /api/v1/checkin**
  - Perform check-in for a passenger.
  - Example Request:

    ```json
    {
      "flightNumber": "TK401",
      "date": "2025-04-28",
      "passengerName": "Can Yılmaz"
    }
    ```
  - Example Response:

    ```json
    {
      "success": true,
      "seatNumber": 1,
      "message": "Check-in successful for Can Yılmaz"
    }
    ```

## Testing with Swagger UI

1. Start the application and navigate to `http://localhost:5205/swagger`.
2. Authenticate using the `/api/v1/auth/login` endpoint to get a JWT token.
3. Authorize Swagger UI by adding the token (`Bearer <token>`).
4. Test the endpoints by adding flights, purchasing tickets, and performing check-in operations.


## Troubleshooting

- **Database Connection Issues**:
  - Ensure SQL Server is running and the connection string in `appsettings.json` is correct.
- **Authorization Errors**:
  - If the authorize part is incorrect, you will see the following error:
    ```
    401 Unauthorized
    ```
    - **Solution**: Verify the JWT token is correctly added in Swagger UI (`Bearer <token>` format). Ensure you have logged in using `/api/v1/auth/login` and copied the token correctly. Also, check if the user has the required role (e.g., Admin for adding flights).


![error](https://github.com/user-attachments/assets/d857e6ba-95c7-4127-a5f0-3be43fa566b6)
