# Dotnet Case

This is a Web API demo made using .NET Core 3.1, to be combined with a front-end project for testing purposes. The front-end demo project can be found over here: https://github.com/mvmprojects/angular-case 

The project is themed around a music store and so the database entities are Artist, Album and Track. At the time of writing, only the Track object will see full CRUD functionality all the way into the front-end.

EF Core and Automapper will be used.

## Projects within Solution

Projects within the dotnet-case solution are API (default Web App template), BL, DOMAIN and DATA for more separation (empty class library templates) and finally TEST (NUnit template).

## Note on Architecture

The idea to add (several) layers between Entity Framework and the controller layer appears to be losing popularity, especially among users of EF Core. Extra layers are still present in this demo, however.