using ECommerce.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            //policy.WithOrigins("http://localhost:4200")
            //policy.WithOrigins("https://icy-wave-01c980310.2.azurestaticapps.net")
            policy.WithOrigins("https://icy-wave-01c980310.2.azurestaticapps.net", "http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

var connectionString = builder.Configuration["ECommerce:ConnectionString"];
//var connectionString = builder.Configuration.GetConnectionString("ECommerce:ConnectionString");
builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<IContext>(provider => provider.GetService<Context>());


//builder.Services.AddSingleton<IRepository>
//   (sp => new SQLRepository(connectionString, sp.GetRequiredService<ILogger<SQLRepository>>()));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
