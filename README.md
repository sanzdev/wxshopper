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
- Comments are not added inside the code as they are already very much readable.
- No database involvement.

- Test coverage is currently 65%. More scenarios were not added in the interest of time.
- Fluent validations, more logging and structured logging are improvements that can be done.
- Better exception handling and a common api response could be added.
   
### Solution

There are three exercises here.



1. Get user details     
```
Operation: GET - api/user  
Parameters: None  
Response:   
{
  "name": "string",
  "token": "string"
}
```    

2. Get Products     
```
Operation: Get Products with a sort Parameter.  
Parameters:   
- QueryString parameter sortOption - Low, High, Ascending, Descending, Recommended
- Token.  
Response:  
[
    {
        "name": "Test Product A",
        "price": 99.99,
        "quantity": 0.0
    }
]  
```  

2. Get Trolley Totals    
```
Operation: Post  
Parameters:   
- Form Data - Json Trolley items.   
- Token.  
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
```   
  
  
