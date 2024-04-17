DirectBookingOne.Api: Hotel Product Management with MongoDB
-----------------------------------------------------------
This document provides a solution overview for managing hotel product categories and their availability using ASP.NET Core, C#, and MongoDB.

1. Functionality Overview:
--------------------------

The API offers functionalities to manage hotel product categories and check their availability:

Product Management:
List all available product categories.
Retrieve a specific product category by its ID.
Create a new product category.
Update an existing product category.
Delete a product category.
Availability Check:
Check if a specific product category is available for a given date range.

-----------------------
2. Solution Highlights:
3. 
The provided code demonstrates how these functionalities are implemented:

Data Model:
A clear separation of concerns exists with dedicated models for Product, BaseProduct (inheritance for common properties), and Availability.
Services:
ProductService handles CRUD operations for product categories in MongoDB.
AvailabilityService checks availability and creates new availability entries.
Controllers:
ProductController exposes API endpoints for product management using ProductService.
AvailabilityController provides the availability check endpoint using AvailabilityService.

--------------------

3. Technology Stack:
Backend: ASP.NET Core MVC
Programming Language: C#
Database: MongoDB
--------------------
5. Conclusion:

This walkthrough showcases a solution for managing hotel product categories and their availability. The code leverages ASP.NET Core, C#, and MongoDB, demonstrating core functionalities and adherence to best practices.
