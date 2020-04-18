# Football news

This is simple website that is gathering information about football. It allows to add articles, tags for articles, manage leagues, teams and matches.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites


**Install MSSQL or Postgres.**  
**Install dotnet ef core** from [Microsoft website](https://dotnet.microsoft.com/download).  
**Run command dotnet tool install --global dotnet-ef** to install Entity Framework Core CLI tool.

### Configuration
 

After installation process go to project and find **appsettings.json** file and edit **DefaultConnection** value by placing your connection string to be able to access your database.

After specyfing connection string go to 
Infrastructure project (it is in place where file ECMP.Infrastructure.csproj is located) then using cmd or PowerShell run this command 
```dotnet ef database update --startup-project ..\FootballNews.WebApp\FootballNews.WebApp.csproj```

This command will update your database using all migrations that has been created before.

Now you can go and launch application using your IDE or CLI.

## Deployment

To deploy this application on external server you have to use IDE publish option or by using CLI. If you prefer CLI then it can be done by going to **FootballNews.WebApp** project with cmd or powershell and run command ``` dotnet publish```. More information about this commnand can be found [here](https://docs.microsoft.com/pl-pl/dotnet/core/tools/dotnet-publish)

## Built With

* [ASP .NET Core](https://docs.microsoft.com/en-US/aspnet/core/?view=aspnetcore-3.1) - The web framework used
* [EF Core](https://docs.microsoft.com/en-US/ef/core/) - Object-Relational Mapper to simplify operations on the database,
* [Bootstrap](https://getbootstrap.com/) - CSS framework
* [JQuery](https://jquery.com/) - js library that helps operate on DOM.

