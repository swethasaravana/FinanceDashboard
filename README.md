Finance Dashboard
  A role-based backend system to manage financial records, users, and analytics using ASP.NET Core Web API, EF Core, and JWT Authentication.

Features
1. Authentication
Register (default role: Viewer)
Login with JWT
2. User Management (Admin)
Create users (Analyst/Admin)
View all users
Activate / Deactivate users
Update user roles
3. Financial Records
Create records (Income / Expense)
View own records
Get all records (Admin)
Update/Delete records (Admin)
4. Dashboard
Total Income
Total Expense
Net Balance
Category-wise summary

Tech Stack:
ASP.NET Core Web API
Entity Framework Core
SQL Server
JWT Authentication
Swagger

Roles
Viewer -	Create & view own records
Analyst -	Same as Viewer
Admin -	Full access
