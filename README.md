# ProductStockApi

**Overview**  

This is a .NET Core Web API for managing products with CRUD operations. The API follows a three-tier architecture consisting of controllers, services, and repositories to ensure separation of concerns and maintainability. It also includes unit tests for the service layer using Moq to validate business logic.

**Features**  

- CRUD operations for products  
- Quantity management (increment/decrement quantity)  
- Follows three-tier architecture (Controller -> Service -> Repository)  
- Uses Entity Framework Core with In-Memory Database  
- Implements Dependency Injection for better maintainability  
- Unit tests using Moq for service layer validation


**Setup and Run Locally**

.Clone the Repository -> 
.Restore Dependencies -> dotnet restore

**Available API Endpoints**

| Method | Endpoint                                           | Description       |
|--------|---------------------------------------------------|-------------------|
| GET    | `/api/products`                                  | Get all products  |
| GET    | `/api/products/{id}`                             | Get product by ID |
| POST   | `/api/products`                                  | Add a new product |
| PUT    | `/api/products/{id}`                             | Update a product  |
| DELETE | `/api/products/{id}`                             | Delete a product  |
| PUT    | `/api/products/decrement-quantity/{id}/{quantity}` | Decrease quantity |
| PUT    | `/api/products/add-to-quantity/{id}/{quantity}`  | Increase quantity |


Run the API -> dotnet run

Running Unit Tests -> dotnet test

