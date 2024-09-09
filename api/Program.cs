using api.Data;
using api.Interfaces;
using api.Models;
using api.ParamObjects.Stock;
using api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// hook db up to service
builder.Services.AddDbContext<ApplicationDbContext>(optionBuilder =>
{
    optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Adds the default identity system to the application with specified options for user management.
builder.Services.AddIdentity<AppUser, AppRole>((IdentityOptions options) =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
})
// `AddEntityFrameworkStores` is used to store user and role data in a database.
.AddEntityFrameworkStores<ApplicationDbContext>();

// Sets up JWT-based authentication using the JwtBearer scheme.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Issuer is basically server
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),

    };
});

// wire up dependency
builder.Services.AddScoped<StockRepository>();
builder.Services.AddScoped<CommentRepository>();

// add controllers that are annotated with [ApiController]
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// To prevent infinite loop when serializing entities with relationships
builder.Services.AddControllers().AddJsonOptions(
    options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Adds auth middlewares to the request pipeline. 
// This middleware checks the Authorization header of incoming requests to validate the JWT token.
app.UseAuthentication();
app.UseAuthorization();

// To make swagger work with controllers
app.MapControllers();
app.Run();