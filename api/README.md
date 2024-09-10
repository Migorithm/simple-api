## Database

```shell
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef 

# initialize schema 
dotnet ef migrations add init  

# actually apply changes to database
dotnet ef database update
```



## Dependencies

### For Auth
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.Core

### ORM
- Microsoft.EntityFrameWorkCore
- Microsoft.EntityFrameWorkCore.Design (schema migration)
- Npgsql.EntityFrameWorkCore.PostgreSQL


