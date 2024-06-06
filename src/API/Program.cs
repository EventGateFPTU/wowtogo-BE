using UseCases;
using Infrastructure;
using API.ExceptionMiddlewares;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using API.Endpoints;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add services to the container.


// Dependancy Injection
builder.Services.AddUseCases();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapWowToGoEndpoints();

app.Run();