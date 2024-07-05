using API;
using API.AuthMiddleware;
using UseCases;
using Infrastructure;
using API.ExceptionMiddlewares;
using API.Middlewares;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using dotenv.net;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddHostConfigurations(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.EnableAnnotations());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddServices(builder.Configuration);
builder.Services.AddUseCases();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
// builder.Services.AddAntiforgery();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(option =>
    {
        option.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
builder.Services.AddScoped(x => new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL")));

var app = builder.Build();
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<WowToGoDBContext>();
dbContext.Database.Migrate();
// var provider = app.Services.GetRequiredService<iapiversion>();

var requiredVars = new[]
{
    "PORT",
    "CLIENT_ORIGIN_URL",
    "AUTH0_DOMAIN",
    "AUTH0_AUDIENCE",
    "POSTGRES_CONSTR",
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
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
    });
}


// app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();
app.UseCors();
// app.UseAntiforgery();

app.MapWowToGoEndpoints();

app.Run();