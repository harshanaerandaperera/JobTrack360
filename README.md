![Job Tracker Banner](./assets/SwaggerImg.png)

# 📌 Job Application Tracker - Backend API

This is the backend API for the **JobTrack360** app, built with **ASP.NET Core Web API** and **Entity Framework Core (In-Memory)**. 
It allows users to track job applications, including the company name, position, User Notes, application status, and date applied.

---

## ⚙️ Technologies Used

- ASP.NET 8
- Entity Framework Core (In-Memory)
- Swagger (OpenAPI)
- Repository Pattern
- Dependency Injection (DI)
- RESTful API

---


### 1. 📦 Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- IDE like Visual Studio, VS Code

### 2. 📁 Clone the Repository

```bash
git clone https://github.com/harshanaerandaperera/JobTrack360BE.git
cd JobTrack360BE
```

### 3. Project Structure

```
JobTrack360.API/
├── Controllers/
│   └── JobApplicationsController.cs
├── DataEF/
│   └── ApplicationDbContext.cs
├── Models/
│   └── JobApplication.cs
├── Repositories/
│   ├── IJobApplicationRepository.cs
│   └── JobApplicationRepository.cs
├── Program.cs
└── JobTrack360.csproj
```

### 4. 🛠️ Run the Application
```bash
	dotnet run
```