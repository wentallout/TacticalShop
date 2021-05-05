# TacticalShop: Asp.Net Core 5 E-Commerce project by Nguyen Dang Khoa

![](https://github.com/wentallout/TacticalShop/workflows/.NET/badge.svg)

An E-commerce project to help me learn ASP.NET 5, EF Core, best practices, etc.

## Project Structure

Experiment with CQRS + Mediator in .NET Core.

Reference: <https://letienthanh0212.medium.com/cqrs-and-mediator-in-net-core-project-c0b477eab6e9>

1/ **Backend**: API Controllers + IdentityServer + NWebsec.AspNetCore.Middleware + use MediatR to send requests to a handler (which is in Application project).

2/ **Application**: contains logic for API Controllers to use, work in progress but categories and products are functional.

3/ **Persistence**: contains DatabaseContext and Migrations for EntityFrameworkCore.

4/ **Domain**: contains Models for DatabaseContext in Persistence.

5/ **ViewModels**: contains ViewModels. (might not be optimal in this pattern?)

6/ **Test**: UnitTest using XUnit.

7/ **Infrastructure**: contains services (ex: EmailSender, Cloudinary), right now I only have Cloudinary.

8/ **Frontend**: Customer website (ViewComponent, HttpClient, Controllers, Views)

9/ **React**: contains a basic admin page using React, Mobx (Manage Products, Categories, view Users info, Cloudinary image uploader)


## Authorization Code Grant flow (with PKCE)
![odkf14kzlb5gcbvrmuvx](https://user-images.githubusercontent.com/76118931/117102110-cfa57880-ada1-11eb-8368-172ea32f2cfb.gif)

The client application prepares an /authorize endpoint request with mandatory parameters such as client_id, grant_type, response_type and redirect_uri and makes a GET request to the IdentityServer (via a browser).

The server when received the request examines the client_id, grant_type and the redirect_uri, and validates for matching client records in its data store.

If a matching client is available, which means that this request is being made by a genuine client which is already registered to use the Token server with this grant and then redirects to its own Login page, where it requests the user to enter his account credentials if already exist or create a new account.

Once user credentials are validated, the Token Server redirects to the redirect_uri registered with the client and attaches a temporary code with a "code" query parameter.

The client extracts this code received from the Token Server and then prepares a POST request to the /token endpoint to exchange this token for an access (or identity) token that enables it to impersonate the user identity. It passes the client_id, code and the redirect_uri to the server along with a security header passing its clientSecret.

The Token Server receives this POST request, examines the header to validate the client and then validates the code sent in the request body to check if there was any code against this client which was generated previously. If validated, the Token Server returns the access (and or id) tokens to the client.

The client application prepares an /authorize endpoint request with mandatory parameters such as client_id, grant_type, response_type and redirect_uri and makes a GET request to the IdentityServer (via a browser).

The server when received the request examines the client_id, grant_type and the redirect_uri, and validates for matching client records in its data store.

If a matching client is available, which means that this request is being made by a genuine client which is already registered to use the Token server with this grant and then redirects to its own Login page, where it requests the user to enter his account credentials if already exist or create a new account.

Once user credentials are validated, the Token Server redirects to the redirect_uri registered with the client and attaches a temporary code with a "code" query parameter.

The client extracts this code received from the Token Server and then prepares a POST request to the /token endpoint to exchange this token for an access (or identity) token that enables it to impersonate the user identity. It passes the client_id, code and the redirect_uri to the server along with a security header passing its clientSecret.

The Token Server receives this POST request, examines the header to validate the client and then validates the code sent in the request body to check if there was any code against this client which was generated previously. If validated, the Token Server returns the access (and or id) tokens to the client.


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

## IMPORTANT:

You can quickly change important urls using appsettings.json. We use this to avoid hardcoding urls.

appsettings.json in Backend
```
  "ClientUrl": {
    "Mvc": "https://localhost:44367",  
    "Swagger": "https://localhost:44341"
   
  }
```

appsettings.json in Frontend
```
"Service": {
    "Backend": {
      "Host": "localhost",
      "Port": "44341",
      "Protocol": "https"
    }
  }
```



Checklist:

⭐ Customer

- ✅ Home: category menu, features products
- ✅ View products by category
- ✅ View products details
- ✅ Product rating
- ✅ Login/Logout ✅ IdentityServer4
- ❌ Shopping Cart, Ordering

⭐ Admin:

- ✅ Login/Logout ✅ IdentityServer4
- ✅ Manage product categories (Name, Description)
- ✅ Manage products (Name, Category, Description, Price, Images, CreatedDate, UpdatedDate)
- ✅ View Customers

