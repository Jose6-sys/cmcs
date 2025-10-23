CMCS – Claim Management and Compensation System

Author

Jose Novela
Contract montly claim system (CMCS)

Overview

The Contract montly claim system (CMCS) is a web application developed using ASP.NET Core MVC.
It allows employees to submit workrelated claims such as overtime hours, and enables administrators to review, approve, or reject them.
The project includes a simple, clean interface and basic role-based access.

Main Features

Employee registration and login

Submit new claims with details like hours worked and notes

Admin approval and rejection of claims

View claim history and current status

File uploads for supporting documents

Unit tests for core functionality

Technologies Used

Framework: ASP.NET Core MVC (.NET 8.0)

Database: Entity Framework Core (Code-First)

Frontend: Razor Views, Bootstrap

Testing: xUnit, Moq

IDE: Visual Studio 2022


Setup Instructions

Open the solution file (cmcs.sln) in Visual Studio 2022.

Update the connection string in appsettings.json to match your SQL Server.

Run the following in the Package Manager Console:

Add-Migration InitialCreate
Update-Database


Press Run to start the project.
