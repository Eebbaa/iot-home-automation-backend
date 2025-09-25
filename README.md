IoT Home Automation Backend

A full-stack IoT backend built with ASP.NET Core MVC, Entity Framework Core, and SignalR, designed to manage smart devices such as an ESP32 door sensor.
This backend exposes a RESTful API for IoT devices and a real-time communication channel for frontend dashboards.

Features

    User Management: Register/login users (JWT-based authentication).

    Device Management: Register, update, and delete devices.

    Telemetry API: Receive data from IoT devices (e.g., ESP32 door sensor).

    Real-Time Updates: Push device status changes to frontend using SignalR.

    Database Persistence: Store users, devices, and telemetry data using EF Core.

    Secure API: Device authentication using tokens.

    Scalable: Ready for Docker + Kubernetes deployment.

Tech Stack

    Backend: ASP.NET Core 8, Entity Framework Core
    
    Database: SQL Server / PostgreSQL (configurable)
    
    Realtime: SignalR
    
    Auth: JWT (JSON Web Tokens)
    
    DevOps: Docker, Kubernetes, Azure (App Service/AKS)
    
    Frontend (separate repo): React + Tailwind + SignalR client

    API Endpoints (Sample)

Authentication

    POST /api/auth/register → Register new user
    
    POST /api/auth/login → Login & get JWT
    
    Devices
    
    GET /api/devices → Get user’s devices
    
    POST /api/devices → Register a new device
    
    DELETE /api/devices/{id} → Remove device

Telemetry

    POST /api/telemetry → Receive device status
Getting Started
Prerequisites

.NET 8 SDK

SQL Server or PostgreSQL

(Optional) Docker


Documentation for the project is on Confluence 
  - https://leiprojects2030.atlassian.net/wiki/x/DgCqAw

