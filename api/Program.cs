using api.Data;
using api.Interfaces;
using api.Models;
using api.Repository;
using api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


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
        // server validates the token - checks if it is valid
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],

        // ValidateLifetime = true,
        // LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,

        // Here, audience means intended recipient of the token, typically URL, identifier, unique string
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),

    };
});

// wire up dependency
builder.Services.AddScoped<StockRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<PortfolioRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

// add controllers that are annotated with [ApiController]
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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

// To make swagger work with controllersq
app.MapControllers();
app.Run();