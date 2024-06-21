using UseCases;
using Infrastructure;
using API.ExceptionMiddlewares;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.EnableAnnotations());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add services to the container.


// Dependancy Injection
builder.Services.AddUseCases();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
// var provider = app.Services.GetRequiredService<iapiversion>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
    });
}


// app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapWowToGoEndpoints();

app.Run();