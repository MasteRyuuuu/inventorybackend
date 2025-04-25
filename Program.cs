using Microsoft.EntityFrameworkCore;
using InventorySystem.Data;
using InventorySystem.Controllers;
using InventorySystem.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL; 
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<BarcodeService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<AdjustmentService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddHostedService<InventoryEmailJob>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();


app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();