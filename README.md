# BJJ Tracker

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)
![C#](https://img.shields.io/badge/C%23-239120)
![EF Core](https://img.shields.io/badge/EF%20Core-9.0-512BD4)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-CC2927)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3)

BJJ Tracker is a web application built with ASP.NET Core MVC for owners of Brazilian Jiu-Jitsu academies and for the people who practice this sport.

## Features

**For academies**
- Create the academy and manage its training schedule.
- Create training sessions with a valid date, time and duration.
- See who attends the trainings and what type of training each practitioner attends.
- Share techniques with their name, position, description and a video link.

**For practitioners**
- Browse the schedule of every academy and attend its training sessions.
- See when and which training sessions they have attended.
- Track their sparring sessions for every attended training: the partner, the partner's belt, the partner's academy and the result.
- Watch the techniques uploaded by the academies to prepare for future sessions.

## Technologies

**Back-end**
- C# and ASP.NET Core MVC on .NET 9
- Entity Framework Core with SQL Server
- ASP.NET Core Identity (login, registration and roles)

**Front-end**
- Razor views (HTML with C#) with a shared layout, partial views and sections
- Bootstrap 5 for the responsive design
- jQuery with jQuery Validation for client-side validation

**Infrastructure**
- Docker Compose runs SQL Server 2019 in a container, so no local SQL Server installation is needed

## Getting started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Git
- EF Core CLI tool: `dotnet tool install --global dotnet-ef --version 9.*`

### Setup
1. Clone the repository and open its folder:
   ```
   git clone https://github.com/stefanovstefan562-ops/BJJTracker.git
   cd BJJTracker
   ```
2. Create the `.env` file from the example, then open it in a text editor (for example in Notepad) and replace `YOUR_PASSWORD` with your own SQL Server password (see the requirements in the table below):
   ```
   copy .env.example .env
   ```
3. Start SQL Server in Docker:
   ```
   docker compose up -d
   ```
4. Store the connection string in user secrets. Replace `YOUR_PASSWORD` with the same password that you set in `.env`:
   ```
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=BjjTrackerDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
   ```
5. Create the database by applying the migrations:
   ```
   dotnet ef database update
   ```
6. Run the application with the HTTPS profile and open https://localhost:7298:
   ```
   dotnet run --launch-profile https
   ```
   If the browser shows a certificate warning, run `dotnet dev-certs https --trust` once.

### Environment variables
| Name | Where | Description |
|---|---|---|
| `MSSQL_SA_PASSWORD` | `.env` | Password of the SQL Server `sa` user. It must have at least 8 characters, with upper-case, lower-case letters and digits. Avoid the characters `;` and `"`, because the same password is used in the connection string. |
| `ConnectionStrings:DefaultConnection` | user secrets | Connection string to the `BjjTrackerDb` database. |

### First use
Register a new account and choose **Academy** or **Practitioner**. The two roles are created automatically when the application starts.

### Basic flow
**Academy**
1. Register as an Academy and create the academy profile.
2. Create training sessions with a valid date, time and duration.
3. See who attends the trainings and what type of training each practitioner attends.
4. Share techniques with their name, position, description and a video link.

**Practitioner**
1. Register as a Practitioner and create the practitioner profile.
2. Choose a training session from the schedule of any academy and record the attendance.
3. Check when and which training sessions you have attended.
4. For every attended training, add a sparring session: who you sparred with, their belt and academy, and the result.
5. Watch the techniques uploaded by the academies to prepare for future sessions.