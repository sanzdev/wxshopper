## WX Shopper APIs
Retail Problem Solving.


## Summary
These are a suite of Opem API standard services for day to day retail and e-commerce problem solving exercise.  
Documentation - https://wxshopper.azurewebsites.net/swagger/index.html

   
## Technology Stack
- Azure API Management Service, App Service.
- Asp.Net Core 3.1
- Visual Studio 2019
- Moq, xUnit, Moq, FluentAssertions, Serilog.
   
## Assumptions and Conditions
- Authentication, API security currently considered out of scope.
- No automation, integration tests, CI/CD implemented.
- Versioning, retry mechanisms, fault tolerance are not considered in scope.
- No database involvement.

- Test coverage is currently 65%. More scenarios were not added in the interest of time.
- Fluent validations, more logging and structured logging are improvements that can be done.
   
### Solution

There are three exercises here.

1. Get user details  
Operation: GET - api/user  
Parameters: None  
Response:   
{
  "name": "string",
  "token": "string"
}


2. Get Products  
Operation: Get Products. Optional Sort Parameter.  
Parameters:   
QueryString parameter sortOption.  
Token.  
Response:  
[
    {
        "name": "Test Product A",
        "price": 99.99,
        "quantity": 0.0
    }
]  


2. Get Products  
Operation: Get  
Parameters:   
Token.  
List of Items    
{
  "products": [
    {
      "name": "string",
      "price": 0,
      "quantity": 0
    }
  ]
}  
Response:  
Lowest Total Amount  
  
  
