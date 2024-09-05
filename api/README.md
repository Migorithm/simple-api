## Database

```shell
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef 

# initialize schema 
dotnet ef migrations add init  

# actually apply changes to database
dotnet ef database update
```