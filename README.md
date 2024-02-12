### Apartment Management System API Overview

**Project Summary:**
This .NET Core 8 API serves as the backend for an apartment management system. It handles dues and bills, user management, and payment tracking.

**User Roles and Actions:**
- **Administrator (Admin):**
  - Manages user profiles, dues, and bills.
  - Views payment statuses and tracks debts.
- **Resident Owners/Tenants:**
  - View and pay dues and bills.

**Data Models:**
- Apartments (Apartment.cs): Details include block info, status, type, floor, and number.
- Users (AppUser.cs): Contains personal info like name, email, and payment method.
- Payments (Payment.cs): Tracks payment details such as date, type, amount, and user association.

**Workflow:**
- Admins set up the system, manage users, and track payments.
- Users view and pay dues/bills.

**Requirements:**
- Developed using .NET 8 API, employing RESTful services.
- MS SQL Server used as the database with EF CORE as the ORM.
- Utilizes one-to-one and one-to-many table relationships.

**Token and User Operations:**
- Token-based authentication for user access (IdentitiesController.cs).
- User management and role assignment functionalities included.

**Background Services:**
- Periodic checks for user email notifications and system status updates (UserEmailSenderBackgroundService.cs, ApartmentManagementEmailSenderBackgroundService.cs).

This API streamlines apartment management tasks, facilitating efficient tracking and payment handling for residents and administrators.

