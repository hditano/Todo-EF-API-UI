# Todo-EF-API-UI

This is a simple Todo web application built using .NET Core 3.1.32 and Entity Framework, with a web API backend, with SQLite as DataLayer and a Console Application front-end.

Using Repository Pattern with Dependency Injection Implemented.

## Technologies Used

-   .NET Core 3.1.32
-   Entity Framework Core
-   SQLite
-   HttpClient
-   ASP.Net Web UI
-   Newtonsoft JSON
-   Swagger


## Features

-   Create, read, update, and delete tasks in a todo list
-   Search tasks by title

## Getting Started

To run this application locally, you will need to have NET Core 3.1 installed on your machine.

1.  Clone the repository:
    
    `git clone https://github.com/hditano/Todo-EF-API-UI.git`
    
2.  Open the `Todo-EF-API-UI.sln` solution file in Visual Studio and build the solution.
    
3.  Update the connection string in `appsettings.json` to point to a local instance of SQLite.
    
4.  In Visual Studio, open the Package Manager Console and run the following command to create the database:
    
    `Update-Database`
    
5.  Start the backend API by running the project in Visual Studio.
    
6.  Open [http://localhost:3000](http://localhost:3000/) in your browser to view the application.
    

## License

This project is licensed under the MIT License - see the [LICENSE](https://chat.openai.com/c/LICENSE) file for details.
