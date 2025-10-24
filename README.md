# Product Review & Rating API

A simple ASP.NET Core MVC web application to manage products and reviews, with features like category-based filtering, CSV export, and automatic product code generation.

---

## Features

- **Product Management**
  - Add, edit, and delete products
  - Auto-generated product codes (`PRD-0001`, `PRD-0002`, …)
  - Display product categories based on review ratings (`Good`, `Bad`, `Worst`)
  
- **Review Management**
  - Add reviews for products
  - Filter reviews by category
  - Export reviews to CSV

- **User Integration**
  - Associate reviews with users (requires authentication)
  
- **UI**
  - Bootstrap-based responsive design
  - Radio-button selection for product actions (Details, Edit, Delete)
  - Badges for product categories and review ratings

---

## Technologies

- **Backend**: ASP.NET Core MVC, C#
- **Frontend**: Razor Pages, Bootstrap 5, HTML5, CSS3
- **Database**: Entity Framework Core, SQL Server / LocalDb
- **Authentication**: ASP.NET Core Identity
- **Other**:
  - CSV export helper
  - Category computation based on ratings

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/product-review-api.git
   cd product-review-api

2. **Open in Visual Studio 2022 or later**.

3. **Restore NuGet packages**:
   ```bash
   dotnet restore

4. **Update the connection string in appsettings.json**.
   ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductReviewDb;Trusted_Connection=True;"
    }

6. **Apply migrations and create the database:**:
   ```bash
    dotnet ef database update
   
7. **Run the application:**:
   ```bash
    dotnet run

   # Usage

## Products
- View all products on the index page.
- Click **Add Product** to create a new product (code is auto-generated).
- Select a product using the radio button to **Details**, **Edit**, or **Delete**.
- Product category is computed automatically based on reviews.

## Reviews
- Add reviews for products.
- Use the **Filter by Category** dropdown to view reviews by `Good`, `Bad`, or `Worst`.
- Export filtered reviews to CSV using the **Export** button.
- Ratings must be between 0 and 5.

# Folder Structure
- /Controllers - MVC Controllers (ProductController, ReviewsController)
- /Models - Product and Review models
- /Views - Razor views for Products & Reviews
- /Data - EF Core DbContext
- /Helpers - Helper classes (CSV export, category computation)

## Description
- **Controllers**: Handle HTTP requests, interact with models, and return views.
- **Models**: Define data structures for Products and Reviews.
- **Views**: Razor views for rendering HTML pages.
- **Data**: Contains `AppDbContext` for EF Core database operations.
- **Helpers**: Utility classes for tasks like CSV export and computing product categories.

# Sample Screenshots
## Product feautre
1. Product List page
<img width="1919" height="906" alt="image" src="https://github.com/user-attachments/assets/4b694e56-561f-401f-bdd2-4e3c20bfa2fe" />

2. Create page
<img width="1919" height="903" alt="image" src="https://github.com/user-attachments/assets/4c069e91-6a4a-48b6-9591-aa9c03f52c5a" />

3. Details page
<img width="1919" height="1065" alt="image" src="https://github.com/user-attachments/assets/d8ec5242-66e8-4250-a57f-bef758c78375" />

4. Edit Page
<img width="1919" height="894" alt="image" src="https://github.com/user-attachments/assets/82428823-dca0-4b29-9430-3bdd47532dff" />

## Reviews feature
1. All Reviews
<img width="1917" height="910" alt="image" src="https://github.com/user-attachments/assets/9547ffa0-4c57-4ccf-8096-3e3f283ab9a6" />

## Export File feature
1. Sample of Reviews exported as CSV file.
<img width="818" height="321" alt="image" src="https://github.com/user-attachments/assets/b1b10c9d-0289-4af4-9d1e-d6c45f37495f" />

