using API;
using UseCases;
using Infrastructure;
using API.ExceptionMiddlewares;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddServices(builder.Configuration);
builder.Services.AddUseCases();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.AddHostConfigurations(builder.Configuration);

var app = builder.Build();

var requiredVars = new[]
{
    "PORT",
    "CLIENT_ORIGIN_URL",
    "AUTH0_DOMAIN",
    "AUTH0_AUDIENCE",
};

foreach (var key in requiredVars)
{
    var value = app.Configuration.GetValue<string>(key);

    if (value is "" or null)
    {
        throw new Exception($"Config variable missing: {key}.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapWowToGoEndpoints();

app.Run();