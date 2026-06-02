# QuoteQuiz

A quiz application built with **.NET 8**, **Angular**, and **SQL Server**, where users guess the author of famous quotes in different game modes.

## Prerequisites

Before running the application, ensure the following tools are installed:

* **.NET 8 SDK**
* **Node.js 20+**
* **Angular CLI 19+**
* **SQL Server** (LocalDB, SQL Server Express, or Docker)
* **Git**

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd QuoteQuiz
```

### 2. Configure the Database Connection

Update the connection string in:

```text
QuoteQuiz.API/appsettings.Development.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=QuoteQuizDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Apply Database Migrations

The project uses **Entity Framework Core Code-First** migrations.

Run the following command from the solution root:

```bash
dotnet ef database update --project QuoteQuiz.Infrastructure --startup-project QuoteQuiz.API
```

### 4. Run the Backend API

```bash
dotnet run --project QuoteQuiz.API
```

### 5. Install Frontend Dependencies

Navigate to the Angular project:

```bash
cd quotequiz.ui
npm install
```

### 6. Run the Angular Application

```bash
ng serve
```

## Project Structure

```text
src/
├── QuoteQuiz.API
├── QuoteQuiz.Application
├── QuoteQuiz.Domain
└── QuoteQuiz.Infrastructure

frontend/
└── quotequiz.ui
```

## Technology Stack

### Backend

* ASP.NET Core 8 Web API
* Entity Framework Core 
* MSSQL Database
* Clean Architecture

### Frontend

* Angular 19+
* Angular Material
* RxJS
* TypeScript

