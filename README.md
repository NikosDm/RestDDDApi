# RestDDDApi

Implementation of Web API in .NET 6

# Running the application

If you are using Visual studio then set up RestDDDApi.Api project as start project and run the application.
If you are using Visual studio Code then run the application with `dotnet run` command on folder RestDDDApi/RestDDDApi.Api.

Browse to page http://localhost:5021/swagger/index.html or https://localhost:7018/swagger/index.html
Where you can have a clear view of the Web Api and of its resources.

# Assumptions - Thoughts when designing the application

- When it comes to validation and business logic under DDD patern, I assumed and I am still assuming this refers to validation or any other
  modification that takes place inside Aggregated root or value objects. After my final fixes, changes that is what I tried to do, except for a few more validations I make on CQRS handlers. Personally I would create service classes which would handle the request, perform any necessary modification / validations before sending them to the repositories or any data layer that they project may have.
- As for the Api resource which would support iterating over the customer orders by order date, I assumed that this would be an Api call which would retieve all the orders on a specific date. At least that is what I assumed and developed. The api resource can be found Customers controllers with action name `GetOrdersByDate`.
- I wrote XML comments on every ocassion possible. However, I have left some files without it comments such as interfaces which I think are pretty straightforward what they are used for.
