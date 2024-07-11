using API;
using API.AuthMiddleware;
using API.Common;
using API.ExceptionMiddlewares;
using API.Extensions;
using API.Middlewares;
using CloudinaryDotNet;
using dotenv.net;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.OpenApi.Models;
using UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddHostConfigurations(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddServices(builder.Configuration);
builder.Services.AddUseCases();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
// builder.Services.AddAntiforgery();

builder.Services.AddSwaggerGen(x =>
{
	x.EnableAnnotations();
	x.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});

	x.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
	  {
		new OpenApiSecurityScheme
		{
		  Reference = new OpenApiReference
		  {
			Type = ReferenceType.SecurityScheme,
			Id = "Bearer"
		  },
		},
		new string[] {}
	  }
	});
});

DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
builder.Services.AddScoped(x => new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL")));

var app = builder.Build();
var requiredVars = new[]
{
	"PORT",
	"CLIENT_ORIGIN_URL",
	"AUTH0_DOMAIN",
	"AUTH0_AUDIENCE",
	"POSTGRES_CONSTR",
	"FGA_API_URL",
    // "FGA_STORE_ID",
    // "FGA_MODEL_ID",
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
	app.MigrateDatabase<WowToGoDBContext>((dbContext, _) =>
	{
		return dbContext.Seed();
	});
	app.UseCors(CorsPolicy.Development);
}
else
{
	app.UseCors(CorsPolicy.Production);
}


// app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();

app.MapWowToGoEndpoints();

app.Run();