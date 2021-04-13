# Dotnet Case

This is a Web API demo made using .NET Core 3.1, to be combined with a front-end project for testing purposes. The front-end demo project can be found over here: https://github.com/mvmprojects/angular-case 

The project is themed around a music store and so the database entities are Artist, Album and Track. At the time of writing, only the Track object will see full CRUD functionality all the way into the front-end.

EF Core and Automapper will be used.

A "User Story" is the smallest unit of work in the agile framework, and is intended to represent an end goal from the point of view of a user. The following user stories can be briefly laid out but obviously cannot be tested without running both the backend and frontend simultaneously:

As a user, I want a search bar so that I can look up music artists by name.

As a user, I want to see an overview of albums belonging to the artist so that I can choose an album.

As a user, I want to see an overview of tracks after choosing an album so that I can inspect these tracks.

As a user, I want to have an "add" functionality so that I can add additional tracks to the album.

As a user, I want to have an "edit" functionality so that I can modify an existing track.

As a user, I want to have a "delete" functionality so that I can remove an unneeded track.

## Projects within Solution

Projects within the dotnet-case solution are API (default Web App template), BL, DOMAIN and DATA for more separation (empty class library templates) and finally TEST (NUnit template).

## Note on Architecture

Although it is unlikely that EF Core will ever be switched out for something else, this demo still has a repository layer which is supposed to make a potential switch easier to perform. The layers are as follows:

Controller layer (TracksController.cs etc. in dotnet-case.API)

Business logic layer (TrackService.cs etc. in dotnet-case.BL)

Repository layer (CaseRepository.cs in dotnet-case.DATA)

Case context (CaseContext.cs in dotnet-case.DATA)
