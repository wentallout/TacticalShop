# TacticalShop: Asp.Net Core 5 E-Commerce project by Nguyen Dang Khoa

![](https://github.com/wentallout/TacticalShop/workflows/.NET/badge.svg)

An E-commerce project to help me learn ASP.NET 5, EF Core, best practices, etc.

## STEP 1: Install SDK

You need to install .NET 5.0 here: https://dotnet.microsoft.com/download/dotnet/5.0

## STEP 2: Using Visual Studio to run:

1. Open TacticalShop.sln, build the solution. Set up TacticalShop.Backend and TacticalShop.Frontend as multiple startup by right click solution in Solution Explorer and choose Properties.
2. Right click the TacticalShop.Backend, choose "Manage User Secrets"
   . In secrets.json add your connection string, this setting will override the ConnectionString in the appsettings.json

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TacticalShop;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

3. Open the Package Manager Console, type `Update-Database` and press Enter
4. Ctrl+F5 or F5 to start.

## STEP 3: Authentication

All the content: brands, categories, products can be added via swagger ui. The insert/update/delete APIs are required authentication. So you will need to register an account. Then, in the swagger ui click "Authorize", a popup dialog will be showed and in the **Scope** you need to check on the 'api.TacticalShop'. After authorized you can invoke methods to create brands, categories, projects including upload product thumbnails.

Checklist:

⭐ Customer

- ✅ Home: category menu, features products
- ✅ View products by category
- ✅ View products details
- Product rating
- ✅ Login/Logout ✅ IdentityServer4
- Shopping Cart, Ordering

⭐ Admin:

- ✅ Login/Logout ✅ IdentityServer4
- Manage product categories (Name, Description)
- Manage products (Name, Category, Description, Price, Images, CreatedDate, UpdatedDate)
- View Customers

❌Current Problems:

- Nvarchar(max) is super bad --> fix by using MaxLength and varchar if possible.
- Not sure if I should put DataAnnotation in ViewModels ???