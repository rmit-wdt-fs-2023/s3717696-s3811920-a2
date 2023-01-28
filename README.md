# MCBA - Most Common Bank of Australia
This project is an assignment for the Web Development Technologies course at RMIT. The project consists of two separate projects: Admin WEB-API and Customer-WEB.

## Project Information

### Summary
Repository Link: https://github.com/rmit-wdt-fs-2023/s3717696-s3811920-a2  
Trello Board: https://trello.com/invite/b/3FL9Lygk/ATTIec2bdd2ec22b925f50b6623c5451c06c56B04286/project-management

### Contributors
**Abida Mohammadi (s3811920)**  
**Mevlut Saluk (s3717696)**

## Admin WEB-API
The Admin WEB-API project consists of a RESTful API that is responsible for handling all the database operations and endpoints for the bank's admin portal. The API is built using ASP.NET Core and Entity Framework Core and has the following features:

- CRUD operations.
- Endpoints for blocking and unblocking customer accounts.
- Endpoints for blocking and unblocking bills.

## Admin WEB

*Note: Documentation can be found in the documentation folder.*

The Admin WEB project is a web application that connects to the Admin WEB-API endpoints and provides an easy-to-use interface for the bank's admin staff to manage customer accounts and transactions. The web application is built using ASP.NET Core and Razor Pages.

- Allows operation to edit a customer's general details and address.
- Can lock/unlock a login for a user.
- Can lock/unlock a bill.

## Customer WEB
The Customer WEB project is a web application that allows customers to log in and manage their banking details. The web application is built using ASP.NET Core and Razor Pages. It has the following features:

- A login page for customers to access their account.
- A page for customers to deposit, transfer or withdraw from their accounts.
- A page for customers to update their personal details
- A page for printing a transaction history for the customer's accounts.

### Pre-Requisites

- The current **init** migration must be used to update the database.
- Might need to have a pre added database table: dotnet.SessionCache for persisted sessions.

## Customer WEB - Tests

Currently onlt tests services for the project.

## Inspiration/Acknowledgment
This project was inspired by the work of Matthew Bolger and was used as a starting point for the assignment.

