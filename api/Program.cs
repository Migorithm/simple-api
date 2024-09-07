using api.Data;
using api.Interfaces;
using api.Models;
using api.ParamObjects.Stock;
using api.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// hook db up to service
builder.Services.AddDbContext<ApplicationDbContext>(optionBuilder =>
{
    optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// wire up dependency
builder.Services.AddScoped<StockRepository>();
builder.Services.AddScoped<CommentRepository>();

// add controllers that are annotated with [ApiController]
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// To make swagger work with controllers
app.MapControllers();
app.Run();